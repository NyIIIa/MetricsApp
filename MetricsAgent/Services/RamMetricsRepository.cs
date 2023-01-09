using MetricsAgent.Models;
using MetricsAgent.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.Services;

public class RamMetricsRepository : IRamMetricsRepository
{
    private readonly MetricsDbContext _dbContext;

    public RamMetricsRepository(MetricsDbContext dbContext)
    {
        _dbContext = dbContext;
        
    }
    
    public async Task AddMetricAsync(RamMetric model)
    {
        await _dbContext.RamMetrics.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<RamMetric>> GetAllMetricsAsync()
    {
        return await _dbContext.RamMetrics.FromSql($"select * from RamMetrics").ToListAsync();
    }

    public async Task<IEnumerable<RamMetric>> GetMetricsByPeriodAsync(long from, long to)
    {
        return await _dbContext.RamMetrics.FromSql($"select * from RamMetrics where CurrentDate >= {from} and CurrentDate <= {to}").ToListAsync();
    }
}