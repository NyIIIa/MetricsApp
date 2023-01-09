namespace MetricsAgent.Models.Dto.Response;

public class GpuMetricDto
{
    public double Utilization { get; set; }
    public long CurrentTimeInMilliseconds { get; set; }
}