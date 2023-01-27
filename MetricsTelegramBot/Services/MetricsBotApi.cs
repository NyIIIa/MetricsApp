using MetricsTelegramBot.Services.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services;
public class MetricsBotApi : ITelegramBotCommand
{
    private readonly ICommonMetricsFactory _commonMetricsFactory;
    private readonly IDateTimeMetricsFactory _metricsDateTimeFactory;

    public MetricsBotApi(
        ICommonMetricsFactory commonMetricsFactory,
        IDateTimeMetricsFactory metricsDateTimeFactory)
    {
        _commonMetricsFactory = commonMetricsFactory;
        _metricsDateTimeFactory = metricsDateTimeFactory;
    }
    
    private readonly Dictionary<string, string> _usageCommands = new()
    {
        {
            "help",
            "Welcome to the MetricsApp's application. This bot will help you to get own metrics from your computer.\nAs you can see there is the 'Menu' in the left-bottom corner the current chat. It will help you to see bot's commands. They serve to interact with your computer.\nSo, just click on this menu and choose a command, which you need :)"
        },
        {
            "showExampleInputData",
            "So, you can fetch metrics to use the following commands as in the example:\n \nExamples: \n /cpu from {29-12-2022 12:30:00} to {30-12-2022 12:30:00}\n/gpu from {29-12-2023 12:30:00} to {30-12-2023 12:30:00}\n/gpu from {29-12-2023 12:30:00} to {30-12-2024 12:30:00}\n \nPay attention on the DateTime's format: dd’-‘MM’-‘yyyy’T’HH’:’mm’:’ss. Also you need to cover DateTime's expression in the symbols '{' and '}' as in the example above. Thank you : )"
        },
        {
            "unavailableCommand",
            "You've just typed the wrong command. \nThere's the navigational menu in the left-bottom corner the current chat :)"
        }
    };

    public async Task<Message> UsageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _usageCommands["help"],
            cancellationToken: cancellationToken);
    }

    public async Task<Message> GetAllMetricsAsync(ITelegramBotClient botClient, BotCommand botCommand ,Message message, CancellationToken cancellationToken)
    {
        var commonMetricsService = _commonMetricsFactory.CreateService();
        var result =  await commonMetricsService.NotifyAsync(botClient, message, botCommand);
        
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: result,
            cancellationToken: cancellationToken);
    }
    
    public async Task<Message> GetMetricsByPeriodAsync(ITelegramBotClient botClient, BotCommand botCommand, Message message, CancellationToken cancellationToken)
    {
        var metricsDateTimeService = _metricsDateTimeFactory.CreateService();
        var result = await metricsDateTimeService.NotifyAsync(botClient, message, botCommand);
        
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: result,
            cancellationToken: cancellationToken);
    }

    public async Task<Message> ShowExampleInputDateTimeDataAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _usageCommands["showExampleInputData"],
            cancellationToken: cancellationToken);
    }

    public async Task<Message> UnavailableCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _usageCommands["unavailableCommand"],
            cancellationToken: cancellationToken);
    }
}