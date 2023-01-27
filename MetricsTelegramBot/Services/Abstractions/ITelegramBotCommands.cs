using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services.Abstractions;
 
 public interface ITelegramBotCommand
 {
     /// <summary>
     /// This method shows an available commands to interact with telegram's bot
     /// </summary>
     /// <param name="botClient">The telegram bot's client</param>
     /// <param name="message">The message from client</param>
     /// <param name="cancellationToken">The cancellation token</param>
     /// <returns>The representation sent message's object</returns>
     Task<Message> UsageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
     
     /// <summary>
     /// This method sends all of the specified PC's metrics to the client into file
     /// </summary>
     /// <param name="botClient">The telegram bot's client</param>
     /// <param name="message">The message from client</param>
     /// <param name="cancellationToken">The cancellation token</param>
     /// <returns>The representation sent message's object</returns>
    Task<Message> GetAllMetricsAsync(ITelegramBotClient botClient, BotCommand botCommand, Message message, CancellationToken cancellationToken);
     
     /// <summary>
     /// This method sends the concrete metrics to the client into file by using DateTime's expression.
     /// </summary>
     /// <param name="botClient">The telegram bot's client</param>
     /// <param name="message">The message from client</param>
     /// <param name="cancellationToken">The cancellation token</param>
     /// <returns>The representation sent message's object</returns>
    Task<Message> GetMetricsByPeriodAsync(ITelegramBotClient botClient, BotCommand typeCommand, Message message, CancellationToken cancellationToken);
     
     /// <summary>
     /// This method shows the available examples for using GetMetricsByPeriodAsync method
     /// </summary>
     /// <param name="botClient">The telegram bot's client</param>
     /// <param name="message">The message from client</param>
     /// <param name="cancellationToken">The cancellation token</param>
     /// <returns>The representation sent message's object</returns>
    Task<Message> ShowExampleInputDateTimeDataAsync(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken);
     
     /// <summary>
     /// This method notifies a client, who has typed an incorrect command 
     /// </summary>
     /// <param name="botClient">The telegram bot's client</param>
     /// <param name="message">The message from client</param>
     /// <param name="cancellationToken">The cancellation token</param>
     /// <returns>The representation sent message's object</returns>
    Task<Message> UnavailableCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
  
}