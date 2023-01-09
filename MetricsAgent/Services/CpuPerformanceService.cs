using MetricsAgent.Services.Abstractions;
using System.Diagnostics;
using MetricsAgent.Models;


namespace MetricsAgent.Services;

public class CpuPerformanceService : ICpuPerformanceService
{
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly ILogger<string> _logger;

    public CpuPerformanceService(ICpuMetricsRepository cpuMetricsRepository, ILogger<string> logger)
    {
        _cpuMetricsRepository = cpuMetricsRepository;
        _logger = logger;
    }

    public async Task StartParseAsync()
    {
        try
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            if (cpuCounter.NextValue() == 0)
            {
                Thread.Sleep(2000);
                var utilization = Math.Ceiling(cpuCounter.NextValue());
                var currentTimeInMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                await _cpuMetricsRepository.AddMetricAsync(new CpuMetric
                {
                    Utilization = utilization,
                    CurrentTimeInMilliseconds = currentTimeInMilliseconds
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }
    }
}