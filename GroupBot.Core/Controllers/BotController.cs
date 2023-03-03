using GroupBot.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroupBot.Core.Controllers;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
    private BotService botService;

    public BotController(BotService botService)
    {
        this.botService = botService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        long groupId,
        string message)
    {

        await botService
            .SendMessageToGroupAsync(groupId, message);

        return Ok();
    }
}
