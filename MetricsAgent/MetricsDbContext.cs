using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent;

public class MetricsDbContext : DbContext
{
    public DbSet<CpuMetric>? CpuMetrics { get; set; }
    public DbSet<GpuMetric>? GpuMetrics { get; set; }
    public DbSet<RamMetric>? RamMetrics { get; set; }

    public MetricsDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure CpuMetrics' columns
        modelBuilder.Entity<CpuMetric>()
            .Property(c => c.Utilization)
            .HasMaxLength(100);
        modelBuilder.Entity<CpuMetric>()
            .Property(c => c.CurrentTimeInMilliseconds)
            .HasColumnName("CurrentDate");
        
        //Configure GpuMetrics' columns
        modelBuilder.Entity<GpuMetric>()
            .Property(g => g.Utilization)
            .HasMaxLength(100);
        modelBuilder.Entity<GpuMetric>()
            .Property(g => g.CurrentTimeInMilliseconds)
            .HasColumnName("CurrentDate");
        
        //Configure RamMetrics' columns
        modelBuilder.Entity<RamMetric>()
            .Property(r => r.Utilization)
            .HasMaxLength(100);
        modelBuilder.Entity<RamMetric>()
            .Property(r => r.CurrentTimeInMilliseconds)
            .HasColumnName("CurrentDate");
    }
}