namespace MetricsTelegramBot.Models.Enums;

public enum BotCommand
{ 
    Help,
    GetAllCpuMetrics,
    GetAllGpuMetrics,
    GetAllRamMetrics,
    GetMetricsByPeriod,
    GetCpuMetricsByPeriod,
    GetGpuMetricsByPeriod,
    GetRamMetricsByPeriod,
    UnavailableCommand
}