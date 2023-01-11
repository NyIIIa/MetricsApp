using MetricsManager.Models.Dto.Response;
using MetricsManager.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[ApiController]
[Route("api/metrics/cpu")]
public class CpuMetricsController : Controller
{
    private readonly IHttpClientService _httpClientService;

    public CpuMetricsController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CpuMetricDto>>> GetAllCpuMetrics()
    {
        var status = await _httpClientService
            .SendRequestAsync("http://localhost:7128/api/cpu/all");

        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<CpuMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }

    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<CpuMetricDto>>> GetCpuMetricByPeriod(long from, long to)
    {
        var status = await _httpClientService
            .SendRequestAsync($"http://localhost:7128/api/cpu/from/{from}/to/{to}");

        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<CpuMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }
}