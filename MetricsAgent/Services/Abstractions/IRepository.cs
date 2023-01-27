namespace MetricsAgent.Services.Abstractions;

public interface IRepository<T> where T : class
{
    public Task AddMetricAsync(T model);
    public Task<IEnumerable<T>> GetAllMetricsAsync();
    public Task<IEnumerable<T>> GetMetricsByPeriodAsync(long from, long to);
}