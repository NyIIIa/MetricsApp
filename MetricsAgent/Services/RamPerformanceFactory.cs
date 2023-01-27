using MetricsAgent.Services.Abstractions;

namespace MetricsAgent.Services;

public class RamPerformanceFactory : IRamPerformanceFactory
{
    private readonly IRamMetricsRepository _ramMetricsRepository;
    private readonly ILogger<string> _logger;

    public RamPerformanceFactory(IRamMetricsRepository ramMetricsRepository, ILogger<string> logger)
    {
        _ramMetricsRepository = ramMetricsRepository;
        _logger = logger;
    }
    public IPerformanceService CreateService()
    {
        return new RamPerformanceService(_ramMetricsRepository, _logger);
    }
}