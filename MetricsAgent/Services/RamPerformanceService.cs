using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Abstractions;

namespace MetricsAgent.Services;

public class RamPerformanceService : IRamPerformanceService
{
    private readonly IRamMetricsRepository _ramMetricsRepository;
    private readonly ILogger<string> _logger;

    public RamPerformanceService(IRamMetricsRepository ramMetricsRepository, ILogger<string> logger)
    {
        _ramMetricsRepository = ramMetricsRepository;
        _logger = logger;
    }

    public async Task StartParseAsync()
    {
        try
        {
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes", null);

            var utilization = Math.Ceiling(16000 - ramCounter.NextValue()); // 16000 Mb or 16Gb - is my RAM of computer. 16000 - available Mb memory = utilization
            var currentTimeInMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            await _ramMetricsRepository.AddMetricAsync(new RamMetric
            {
                Utilization = utilization,
                CurrentTimeInMilliseconds = currentTimeInMilliseconds
            });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }
    }
}