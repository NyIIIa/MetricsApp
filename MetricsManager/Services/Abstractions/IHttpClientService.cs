namespace MetricsManager.Services.Abstractions;

public interface IHttpClientService
{
    /// <summary>
    /// Send request to other microservice specified in the requestUri's field.
    /// This method supports only 'GET' method, it doesn't support other HTTP methods.
    /// </summary>
    /// <param name="requestUri">Request uri's remote API</param>
    /// <returns>Operation status</returns>
    public Task<bool> SendRequestAsync(string requestUri);
    /// <summary>
    /// The deserialize content, which it was fetching after 'SendRequestAsync' method was called.
    /// </summary>
    /// <typeparam name="T">Type deserialization objects</typeparam>
    /// <returns>Type deserialization objects</returns>
    public IEnumerable<T> DeserializeResponseContent<T>();
}