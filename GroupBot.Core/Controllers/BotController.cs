using GroupBot.Core.Models;
using GroupBot.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace GroupBot.Core.Controllers;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
    private UpdateHandler botService;

    public BotController(UpdateHandler botService)
    {
        this.botService = botService;
    }

    [HttpPut]
    public async Task<IActionResult> Get(GroupMessageModel model)
    {

        await botService
            .SendMessageToGroupAsync(model);

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] UpdateHandler updateHandler)
    {
        await updateHandler
            .UpdateHandlerAsync(update);

        return Ok();
    }
}
