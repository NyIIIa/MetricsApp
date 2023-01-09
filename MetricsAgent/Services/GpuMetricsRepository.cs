using MetricsAgent.Models;
using MetricsAgent.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Services;

public class GpuMetricsRepository : IGpuMetricsRepository
{
    private readonly MetricsDbContext _dbContext;

    public GpuMetricsRepository(MetricsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddMetricAsync(GpuMetric model)
    {
       await _dbContext.GpuMetrics.AddAsync(model);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<GpuMetric>> GetAllMetricsAsync()
    {
        return await _dbContext.GpuMetrics.FromSql($"select * from GpuMetrics").ToListAsync();
    }

    public async Task<IEnumerable<GpuMetric>> GetMetricsByPeriodAsync(long from, long to)
    {
        return await _dbContext.GpuMetrics.FromSql($"select * from GpuMetrics where CurrentDate >= {from} and CurrentDate <= {to}").ToListAsync();
    }
}