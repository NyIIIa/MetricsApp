using MetricsManager.Models.Dto.Response;
using MetricsManager.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[ApiController]
[Route("api/metrics/gpu")]
public class GpuMetricsController : Controller
{
    private readonly IHttpClientService _httpClientService;

    public GpuMetricsController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<GpuMetricDto>>> GetAllGpuMetrics()
    {
        var status = await _httpClientService
            .SendRequestAsync("http://localhost:7128/api/gpu/all");
        
        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<GpuMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }
    
    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<GpuMetricDto>>> GetGpuMetricsByPeriod(long from, long to)
    {
        var status = await _httpClientService
            .SendRequestAsync($"http://localhost:7128/api/gpu/from/{from}/to/{to}");
        
        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<GpuMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }
}