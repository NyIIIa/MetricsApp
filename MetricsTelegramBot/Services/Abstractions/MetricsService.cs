using MetricsTelegramBot.Services.Abstractions;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MetricsTelegramBot.Services;

public abstract class MetricsService
{
    protected readonly ResponseWriter responseWriter;
    protected readonly RequestBuilder requestBuilder;
    protected readonly IHttpClientService httpClientService;

    protected MetricsService(
        ResponseWriter responseWriter,
        RequestBuilder requestBuilder,
        IHttpClientService httpClientService)
    {
        this.responseWriter = responseWriter;
        this.requestBuilder = requestBuilder;
        this.httpClientService = httpClientService;
    }
    
    /// <summary>
    /// The method conducts the necessary manipulations with DI's services for sending file of the metrics to the client
    /// </summary>
    /// <param name="telegramBotClient">The telegram bot's client</param>
    /// <param name="message">The message from client</param>
    /// <param name="botCommand">The type bot's command</param>
    /// <returns>The result string of the ending point's operation</returns>
    public virtual async Task<string> NotifyAsync(ITelegramBotClient telegramBotClient, Message message,
        BotCommand botCommand)
    {
        var requestModel = requestBuilder.BuildRequest(message.Text, botCommand);
        var statusRequest = await httpClientService.SendRequestAsync(requestModel);
        if (statusRequest)
        {
            var responseData = httpClientService.DeserializeResponseContent(botCommand);

            await responseWriter.SendResponseToClientAsync(telegramBotClient, message, responseData);

            return await Task.FromResult("Take your response data : )");
        }

        return await Task.FromResult<string>("The remote microservice isn't responding");
    }
}