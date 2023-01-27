namespace MetricsAgent.Models.Dto.Response;

public class CpuMetricDto
{
    public double Utilization { get; set; }
    public long CurrentTimeInMilliseconds { get; set; }
}