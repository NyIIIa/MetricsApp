using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MetricsTelegramBot;

public static class Extensions
{
    public static T GetConfiguration<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        var o = serviceProvider.GetService<IOptions<T>>();
        if (o is null)
            throw new ArgumentNullException(nameof(T));

        return o.Value;
    }
    
    public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<T>(
        this IEndpointRouteBuilder endpoints,
        string route)
    {
        var controllerName = typeof(T).Name.Replace("Controller", "");
        var actionName = typeof(T).GetMethods()[0].Name;
        
        return endpoints.MapControllerRoute(
            name: "metricsBot_webhook",
            pattern: route,
            defaults: new { controller = controllerName, action = actionName });
    }

    public static async Task SetMyCommandsToMetricsBotAsync(this IServiceProvider serviceProvider)
    
    {
        #region Metrics bot's commands

        var commands = new BotCommand[]
        {
            new BotCommand()
            {
                Command = "/get_all_cpu_metrics",
                Description = "retrieve all of cpu metrics from personal computer"
            },
            new BotCommand()
            {
                Command = "/get_all_gpu_metrics",
                Description = "retrieve all of gpu metrics from personal computer"
            },
            new BotCommand()
            {
                Command = "/get_all_ram_metrics",
                Description = "retrieve all of ram metrics from personal computer"
            },
            new BotCommand()
            {
                Command = "/get_metrics_by_period",
                Description = "retrieve certain metrics by DateTime period"
            },
            new BotCommand()
            {
                Command = "/help",
                Description = "get metrics bot's usage documentation"
            },
        };

        #endregion
        
        var telegramBotClient = serviceProvider.GetService<ITelegramBotClient>();
        if (telegramBotClient != null)
        {
            var botCommands =  await telegramBotClient.GetMyCommandsAsync();
            if (botCommands.Length <= 0)
            {
                await telegramBotClient.SetMyCommandsAsync(commands);
            }
        }
    }

    public static MetricsTelegramBot.Models.Enums.BotCommand CheckInputCommand(this string message)
    {
        return message.Split(' ')[0] switch
        {
            "/help" => Models.Enums.BotCommand.Help,
            "/get_all_cpu_metrics" => Models.Enums.BotCommand.GetAllCpuMetrics,
            "/get_all_gpu_metrics" => Models.Enums.BotCommand.GetAllGpuMetrics,
            "/get_all_ram_metrics" => Models.Enums.BotCommand.GetAllRamMetrics,
            "/get_metrics_by_period" => Models.Enums.BotCommand.GetMetricsByPeriod,
            "/cpu" => Models.Enums.BotCommand.GetCpuMetricsByPeriod,
            "/gpu" => Models.Enums.BotCommand.GetGpuMetricsByPeriod,
            "/ram" => Models.Enums.BotCommand.GetRamMetricsByPeriod,
            _ => Models.Enums.BotCommand.UnavailableCommand
        };
        
    }
    
    

}