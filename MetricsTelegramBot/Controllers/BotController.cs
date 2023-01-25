using MetricsTelegramBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace MetricsTelegramBot.Controllers;

public class BotController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        [FromServices] MetricsBotHandler metricsBotHandleService,
        CancellationToken cancellationToken)
    {
        await metricsBotHandleService.HandleUpdateAsync(update, cancellationToken);
        return Ok();
    }
}