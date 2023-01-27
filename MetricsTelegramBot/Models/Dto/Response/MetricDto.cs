namespace MetricsTelegramBot.Models.Dto.Response;

public abstract class MetricDto
{
    public double Utilization { get; set; }
    public long CurrentTimeInMilliseconds { get; set; }

    public override string ToString()
    {
        var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(CurrentTimeInMilliseconds.ToString()));
        
        return $"Utilization = {Utilization}\nCurrentTimeInMilliseconds = {date}";
    } 
}