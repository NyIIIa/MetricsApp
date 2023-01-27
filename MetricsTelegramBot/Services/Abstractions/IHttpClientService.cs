using MetricsTelegramBot.Models.Dto.Response;
using MetricsTelegramBot.Models.Enums;
using MetricsTelegramBot.Models.Request;

namespace MetricsTelegramBot.Services.Abstractions;

public interface IHttpClientService
{
    /// <summary>
    /// Send request to other microservice specified in the requestUri's field.
    /// This method supports only 'GET' method, it doesn't support other HTTP methods.
    /// </summary>
    /// <param name="requestModel">RequestModel for sending request to remote API</param>
    /// <returns>Operation status</returns>
    public Task<bool> SendRequestAsync(RequestModel requestModel);
    /// <summary>
    /// The deserialize content, which it was fetching after 'SendRequestAsync' method was called.
    /// </summary>
    /// <typeparam name="T">Type deserialization objects</typeparam>
    /// <returns>Type deserialization objects</returns>
    public IEnumerable<MetricDto> DeserializeResponseContent(BotCommand botCommand);
}