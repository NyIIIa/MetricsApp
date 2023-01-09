using System.Diagnostics;
using MetricsAgent.Services.Abstractions;

namespace MetricsAgent.Services;

public class CpuPerformanceFactory : ICpuPerformanceFactory
{
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly ILogger<string> _logger;

    public CpuPerformanceFactory(ICpuMetricsRepository cpuMetricsRepository, ILogger<string> logger)
    {
        _cpuMetricsRepository = cpuMetricsRepository;
        _logger = logger;
    }
    public IPerformanceService CreateService()
    {
        return new CpuPerformanceService(_cpuMetricsRepository, _logger);
    }
}