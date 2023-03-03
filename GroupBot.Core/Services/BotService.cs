using Telegram.Bot;

namespace GroupBot.Core.Services;

public class BotService
{
    private readonly ITelegramBotClient botClient;
    private readonly ILogger<BotService> logger;

    public BotService(ITelegramBotClient botClient, ILogger<BotService> logger)
    {
        this.botClient = botClient;
        this.logger = logger;
    }

    public async Task SendMessageToGroupAsync(
        long groupId,
        string messageText)
    {
        try
        {
           await botClient.SendTextMessageAsync(
                chatId: groupId,
                text: messageText);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }
}
