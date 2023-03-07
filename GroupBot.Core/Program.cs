using GroupBot.Core.Extensions;
using Telegram.Bot;

namespace GroupBot.Core;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services
            .AddTelegramBot(builder.Configuration)
            .AddTelegramBotService()
            .AddControllerMappers();
            
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        SetWebHook(app, builder.Configuration);

        app.Run();
    }

    public static void SetWebHook(
        IApplicationBuilder builder,
        IConfiguration configuration)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var baseUrl = configuration.GetSection("TelegramBot:BaseAddress").Value;
            var webHookUrl = $"{baseUrl}/bot";

            var webhookInfo = botClient.GetWebhookInfoAsync().Result;

            if (webhookInfo is null || webhookInfo.Url != webHookUrl)
            {
                botClient.SetWebhookAsync(webHookUrl).Wait();
            }
        }
    }
}