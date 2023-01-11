using MetricsManager.Services.Abstractions;
using Newtonsoft.Json;

namespace MetricsManager.Services;

public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public string ResponseContent { get; private set; }

    public HttpClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> SendRequestAsync(string requestUri)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("Metrics");
            var response = await httpClient.GetAsync(requestUri);

            ResponseContent = await response.Content.ReadAsStringAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<T> DeserializeResponseContent<T>()
    {
        return JsonConvert.DeserializeObject<IEnumerable<T>>(ResponseContent);
    }
}