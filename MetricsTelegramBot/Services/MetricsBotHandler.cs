using MetricsTelegramBot.Services.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services;

public class MetricsBotHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<MetricsBotHandler> _logger;
    private readonly ITelegramBotCommand _telegramBotCommand;
    

    public MetricsBotHandler(
        ITelegramBotClient botClient,
        ILogger<MetricsBotHandler> logger,
        ITelegramBotCommand telegramBotCommand
        )
    {
        _botClient = botClient;
        _logger = logger;
        _telegramBotCommand = telegramBotCommand;
        
    }
    
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message }                       => BotOnMessageReceived(message, cancellationToken), 
            _                                              => UnknownUpdateHandlerAsync(update, cancellationToken)
        };
        
        await handler;
    }
    
    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);
        
        if (message.Text is not { } messageText)
            return;
        
        var action = messageText.CheckInputCommand() switch
        {
            BotCommand.Help => _telegramBotCommand.UsageAsync(_botClient, message, cancellationToken),
            BotCommand.GetAllCpuMetrics => _telegramBotCommand.GetAllMetricsAsync(_botClient,BotCommand.GetAllCpuMetrics , message, cancellationToken),
            BotCommand.GetAllGpuMetrics => _telegramBotCommand.GetAllMetricsAsync(_botClient, BotCommand.GetAllGpuMetrics, message, cancellationToken),
            BotCommand.GetAllRamMetrics => _telegramBotCommand.GetAllMetricsAsync(_botClient, BotCommand.GetAllRamMetrics, message, cancellationToken),
            BotCommand.GetMetricsByPeriod => _telegramBotCommand.ShowExampleInputDateTimeDataAsync(_botClient, message, cancellationToken),
            BotCommand.GetCpuMetricsByPeriod => _telegramBotCommand.GetMetricsByPeriodAsync(_botClient, BotCommand.GetCpuMetricsByPeriod, message, cancellationToken),
            BotCommand.GetGpuMetricsByPeriod => _telegramBotCommand.GetMetricsByPeriodAsync(_botClient, BotCommand.GetGpuMetricsByPeriod, message, cancellationToken),
            BotCommand.GetRamMetricsByPeriod => _telegramBotCommand.GetMetricsByPeriodAsync(_botClient, BotCommand.GetRamMetricsByPeriod, message, cancellationToken),
            _ => _telegramBotCommand.UnavailableCommandAsync(_botClient, message, cancellationToken)
        };
        
        Message sentMessage = await action;
        
        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);
    }
    
    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

}