using MetricsManager.Models.Dto.Response;
using MetricsManager.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers;

[ApiController]
[Route("api/metrics/ram")]
public class RamMetricsController : Controller
{
    private readonly IHttpClientService _httpClientService;

    public RamMetricsController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<RamMetricDto>>> GetAllRamMetrics()
    {
        var status = await _httpClientService
            .SendRequestAsync("http://localhost:7128/api/ram/all");
        
        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<RamMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }
    
    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<RamMetricDto>>> GetRamMetricsByPeriod(long from, long to)
    {
        var status = await _httpClientService
            .SendRequestAsync($"http://localhost:7128/api/ram/from/{from}/to/{to}");
        
        if (status)
        {
            try
            {
                return Ok(_httpClientService.DeserializeResponseContent<RamMetricDto>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Remote server(MetricsAgent) isn't responding");
    }
}