using AutoMapper;
using MetricsAgent.Models.Dto.Response;
using MetricsAgent.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[ApiController]
[Route("api/ram")]
public class RamMetricsController : Controller
{
    private readonly IRamMetricsRepository _ramMetricsRepository;
    private readonly IMapper _mapper;

    public RamMetricsController(IRamMetricsRepository ramMetricsRepository, IMapper mapper)
    {
        _ramMetricsRepository = ramMetricsRepository;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<RamMetricDto>>> GetAllRamMetrics()
    {
        try
        {
            var ramMetricsFromDb = await _ramMetricsRepository.GetAllMetricsAsync();
            return Ok(_mapper.Map<IEnumerable<RamMetricDto>>(ramMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<RamMetricDto>>> GetRamMetricsByPeriod(long from, long to)
    {
        try
        {
            var ramMetricsFromDb = await _ramMetricsRepository.GetMetricsByPeriodAsync(from, to);
            return Ok(_mapper.Map<IEnumerable<RamMetricDto>>(ramMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}