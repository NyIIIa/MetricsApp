using MetricsTelegramBot.Models;
using MetricsTelegramBot.Models.Dto.Response;
using MetricsTelegramBot.Models.Enums;
using MetricsTelegramBot.Models.Request;
using MetricsTelegramBot.Services.Abstractions;
using Newtonsoft.Json;

namespace MetricsTelegramBot.Services;

public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public string ResponseContent { get; private set; }

    public HttpClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> SendRequestAsync(RequestModel requestModel)
    {
        try
        {
            if (requestModel.ToString() != String.Empty)
            {
                var httpClient = _httpClientFactory.CreateClient("Metrics");
                var response = await httpClient.GetAsync(requestModel.ToString());

                ResponseContent = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<MetricDto> DeserializeResponseContent(BotCommand botCommand)
    {
        return botCommand switch
        {
            BotCommand.GetAllCpuMetrics => JsonConvert.DeserializeObject<IEnumerable<CpuMetricDto>>(ResponseContent),
            BotCommand.GetAllGpuMetrics => JsonConvert.DeserializeObject<IEnumerable<GpuMetricDto>>(ResponseContent),
            BotCommand.GetAllRamMetrics => JsonConvert.DeserializeObject<IEnumerable<RamMetricDto>>(ResponseContent),
            BotCommand.GetCpuMetricsByPeriod => JsonConvert.DeserializeObject<IEnumerable<CpuMetricDto>>(ResponseContent),
            BotCommand.GetGpuMetricsByPeriod => JsonConvert.DeserializeObject<IEnumerable<GpuMetricDto>>(ResponseContent),
            BotCommand.GetRamMetricsByPeriod => JsonConvert.DeserializeObject<IEnumerable<RamMetricDto>>(ResponseContent),
            _ => new List<MetricDto>()
        };
    }
}