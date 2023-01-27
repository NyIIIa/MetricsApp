using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Abstractions;


namespace MetricsAgent.Services;

public class GpuPerformanceService : IGpuPerformanceService
{
    private readonly IGpuMetricsRepository _gpuMetricsRepository;
    private readonly ILogger<string> _logger;

    public GpuPerformanceService(IGpuMetricsRepository gpuMetricsRepository, ILogger<string> logger)
    {
        _gpuMetricsRepository = gpuMetricsRepository;
        _logger = logger;
    }

    public async Task StartParseAsync()
    {
        try
        {
            var gpuCounters = GetGPUCounters();
            var utilization = Math.Ceiling(GetGPUUsage(gpuCounters));
            var currentTimeInMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            await _gpuMetricsRepository.AddMetricAsync(new GpuMetric
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

    private List<PerformanceCounter> GetGPUCounters()
    {
        var category = new PerformanceCounterCategory("GPU Engine");
        var counterNames = category.GetInstanceNames();

        var gpuCounters = counterNames
            .Where(counterName => counterName.EndsWith("engtype_3D"))
            .SelectMany(counterName => category.GetCounters(counterName))
            .Where(counter => counter.CounterName.Equals("Utilization Percentage"))
            .ToList();

        return gpuCounters;
    }

    private float GetGPUUsage(List<PerformanceCounter> gpuCounters)
    {
        gpuCounters.ForEach(x => x.NextValue());
        Thread.Sleep(1000);
        var result = gpuCounters.Sum(x => x.NextValue());

        return result;
    }
}