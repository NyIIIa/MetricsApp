using System.Diagnostics;
using MetricsAgent.Services.Abstractions;

namespace MetricsAgent.Services;

public class GpuPerformanceFactory : IGpuPerformanceFactory
{
    private readonly IGpuMetricsRepository _gpuMetricsRepository;
    private readonly ILogger<string> _logger;

    public GpuPerformanceFactory(IGpuMetricsRepository gpuMetricsRepository, ILogger<string> logger)
    {
        _gpuMetricsRepository = gpuMetricsRepository;
        _logger = logger;
    }
    public IPerformanceService CreateService()
    {
        return new GpuPerformanceService(_gpuMetricsRepository, _logger);
    }
}