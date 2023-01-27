using MetricsTelegramBot.Services.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services;

public class CommonMetricsService : MetricsService
{
    public CommonMetricsService(
        ResponseWriter responseWriter,
        RequestBuilder requestBuilder,
        IHttpClientService httpClientService) : base(responseWriter, requestBuilder, httpClientService)
    { }

    public override async Task<string> NotifyAsync(ITelegramBotClient telegramBotClient, Message message, BotCommand botCommand)
    {
        try
        { 
            return await base.NotifyAsync(telegramBotClient, message, botCommand);
        }
        catch (Exception e)
        {
            return await Task.FromResult(e.Message);
        }
    }
}