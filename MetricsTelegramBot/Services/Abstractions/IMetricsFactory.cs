namespace MetricsTelegramBot.Services.Abstractions;

public interface IMetricsFactory
{
    public MetricsService CreateService();
}