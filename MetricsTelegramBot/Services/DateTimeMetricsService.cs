using MetricsTelegramBot.Services.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services;

public class DateTimeMetricsService : MetricsService
{
    private readonly ValidatorService _validatorService;

    public DateTimeMetricsService(
        ResponseWriter responseWriter,
        RequestBuilder requestBuilder,
        IHttpClientService httpClientService,
        ValidatorService validatorService) : base(responseWriter, requestBuilder, httpClientService)
    {
        _validatorService = validatorService;
    }
    public override async Task<string> NotifyAsync(ITelegramBotClient telegramBotClient, Message message, BotCommand botCommand)
    {
        try
        {
            var isCorrectDateTimeExpression = await _validatorService.IsCommandValid(message.Text, botCommand);

            if (isCorrectDateTimeExpression)
            {
                return await base.NotifyAsync(telegramBotClient, message, botCommand);
            }

            return await Task.FromResult("Incorrect DateTime's expression");
        }
        catch (Exception e)
        {
            return await Task.FromResult(e.Message);
        }
    }
}