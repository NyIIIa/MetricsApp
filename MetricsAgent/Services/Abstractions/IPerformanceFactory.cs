namespace MetricsAgent.Services.Abstractions;

public interface IPerformanceFactory
{
    public IPerformanceService CreateService();
}