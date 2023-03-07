using Telegram.Bot.Types.Enums;

namespace GroupBot.Core.Models;

public class GroupMessageModel
{
    public long GroupId { get; set; }
    public string MessageText { get; set; }
    public string MarkupType { get; set; }
}

