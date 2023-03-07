using GroupBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GroupBot.Core.Services;

public class UpdateHandler
{
    private readonly ITelegramBotClient botClient;
    private readonly ILogger<UpdateHandler> logger;

    public UpdateHandler(
        ITelegramBotClient botClient,
        ILogger<UpdateHandler> logger)
    {
        this.botClient = botClient;
        this.logger = logger;
    }

    public async Task SendMessageToGroupAsync(GroupMessageModel model)
    {

        ParseMode parseMode = model.MarkupType switch
        {
            "markdown" => ParseMode.Markdown,
            "html" => ParseMode.Html,
            _ => ParseMode.Markdown
        };

        try
        {
           await botClient.SendTextMessageAsync(
                chatId: model.GroupId,
                text: model.MessageText,
                parseMode: parseMode);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }

    public async Task UpdateHandlerAsync(Update update)
    {
        if (update.Message is null)
        {
            return;
        }
        Message message = update.Message;

        if (message.Text is null)
        {
            return;
        }

        if (message.Text.ToLower() == "/id")
        {
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Group id 👇\n<pre>{message.Chat.Id}</pre>",
                parseMode: ParseMode.Html);
        }
    }
}
