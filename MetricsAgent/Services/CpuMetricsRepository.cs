using MetricsAgent.Models;
using MetricsAgent.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Services;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private readonly MetricsDbContext _dbContext;

    public CpuMetricsRepository(MetricsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddMetricAsync(CpuMetric model)
    {
        await _dbContext.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<CpuMetric>> GetAllMetricsAsync()
    {
        return await _dbContext.CpuMetrics.FromSql($"select * from CpuMetrics").ToListAsync();
    }

    public async Task<IEnumerable<CpuMetric>> GetMetricsByPeriodAsync(long from, long to)
    {
        return await _dbContext.CpuMetrics.FromSql($"select * from CpuMetric where CurrentDate >= {from} and CurrentDate <= {to}").ToListAsync();
    }
}