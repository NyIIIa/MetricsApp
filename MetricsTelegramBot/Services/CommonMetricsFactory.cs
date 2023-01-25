using MetricsTelegramBot.Services.Abstractions;

namespace MetricsTelegramBot.Services;

public class CommonMetricsFactory : ICommonMetricsFactory
{
    private readonly ResponseWriter _responseWriter;
    private readonly RequestBuilder _requestBuilder;
    private readonly IHttpClientService _httpClientService;

    public CommonMetricsFactory(
        ResponseWriter responseWriter,
        RequestBuilder requestBuilder,
        IHttpClientService httpClientService)
    {
        _responseWriter = responseWriter;
        _requestBuilder = requestBuilder;
        _httpClientService = httpClientService;
    }

    public MetricsService CreateService()
    {
        return new CommonMetricsService
            (_responseWriter, _requestBuilder, _httpClientService);
    }
}