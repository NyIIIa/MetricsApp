namespace MetricsAgent.Models;

public class RamMetric
{
    public int Id { get; set; }
    public double Utilization { get; set; }
    public long CurrentTimeInMilliseconds { get; set; }
}