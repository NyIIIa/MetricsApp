using MetricsTelegramBot.Services.Abstractions;

namespace MetricsTelegramBot.Services;

public class DateTimeMetricsFactory : IDateTimeMetricsFactory
{
    private readonly ResponseWriter _responseWriter;
    private readonly RequestBuilder _requestBuilder;
    private readonly IHttpClientService _httpClientService;
    private readonly ValidatorService _validatorService;

    public DateTimeMetricsFactory(
        ResponseWriter responseWriter,
        RequestBuilder requestBuilder,
        IHttpClientService httpClientService,
        ValidatorService validatorService)
    {
        _responseWriter = responseWriter;
        _requestBuilder = requestBuilder;
        _httpClientService = httpClientService;
        _validatorService = validatorService;
    }
    
    public MetricsService CreateService()
    {
        return new DateTimeMetricsService(
            _responseWriter, _requestBuilder, _httpClientService, _validatorService);
    }
}