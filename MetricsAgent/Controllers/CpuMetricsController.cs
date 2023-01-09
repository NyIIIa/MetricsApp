using AutoMapper;
using MetricsAgent.Models.Dto.Response;
using MetricsAgent.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[ApiController]
[Route("api/cpu")]
public class CpuMetricsController : Controller
{
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly IMapper _mapper;

    public CpuMetricsController(ICpuMetricsRepository cpuMetricsRepository, IMapper mapper)
    {
        _cpuMetricsRepository = cpuMetricsRepository;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CpuMetricDto>>> GetAllCpuMetrics()
    {
        try
        {
            var cpuMetricsFromDb = await _cpuMetricsRepository.GetAllMetricsAsync();
            return Ok(_mapper.Map<IEnumerable<CpuMetricDto>>(cpuMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<CpuMetricDto>>> GetCpuMetricsByPeriod(long from, long to)
    {
        try
        {
            var cpuMetricsFromDb = await _cpuMetricsRepository.GetMetricsByPeriodAsync(from, to);
            return Ok(_mapper.Map<IEnumerable<CpuMetricDto>>(cpuMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}