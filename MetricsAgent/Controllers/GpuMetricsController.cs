using AutoMapper;
using MetricsAgent.Models.Dto.Response;
using MetricsAgent.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[ApiController]
[Route("api/gpu")]
public class GpuMetricsController : Controller
{
    private readonly IGpuMetricsRepository _gpuMetricsRepository;
    private readonly IMapper _mapper;

    public GpuMetricsController(IGpuMetricsRepository gpuMetricsRepository, IMapper mapper)
    {
        _gpuMetricsRepository = gpuMetricsRepository;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<GpuMetricDto>>> GetAllGpuMetrics()
    {
        try
        {
            var gpuMetricsFromDb = await _gpuMetricsRepository.GetAllMetricsAsync();
            return Ok(_mapper.Map<IEnumerable<GpuMetricDto>>(gpuMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("from/{from:long}/to/{to:long}")]
    public async Task<ActionResult<IEnumerable<GpuMetricDto>>> GetGpuMetricsByPeriod(long from, long to)
    {
        try
        {
            var gpuMetricsFromDb = await _gpuMetricsRepository.GetMetricsByPeriodAsync(from, to);
            return Ok(_mapper.Map<IEnumerable<GpuMetricDto>>(gpuMetricsFromDb));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}