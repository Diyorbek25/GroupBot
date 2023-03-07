using GroupBot.Core.Services;
using Telegram.Bot;

namespace GroupBot.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string botToken = configuration
            .GetSection("TelegramBot:Token").Value;

        services.AddSingleton<ITelegramBotClient, TelegramBotClient>(
            botClient => new TelegramBotClient(botToken));

        return services;
    }

    public static IServiceCollection AddTelegramBotService(
        this IServiceCollection services)
    {
        services.AddScoped<UpdateHandler>();
        return services;
    }

    public static IServiceCollection AddControllerMappers(
       this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
