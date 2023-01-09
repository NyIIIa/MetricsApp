namespace MetricsAgent.Models;

public class GpuMetric
{
    public int Id { get; set; }
    public double Utilization { get; set; }
    public long CurrentTimeInMilliseconds { get; set; }
}