using System.Text.RegularExpressions;
using BotCommand = MetricsTelegramBot.Models.Enums.BotCommand;

namespace MetricsTelegramBot.Services;

public class ValidatorService
{
    private readonly Dictionary<string, string> _regexExpressions = new()
    {
        {"/cpu from", @"/cpu from {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}} to {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}}\z"},
        
        {"/gpu from", @"/gpu from {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}} to {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}}\z"},
        
        {"/ram from", @"/ram from {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}} to {([0-9]{2})+-([0-9]{2})+-([0-9]{4}) ([0-9]{2})+:([0-9]{2})+:[0-9]{2}}\z"},
        
    };
    
    /// <summary>
    /// This method validates the input DateTime's message(if the input DateTime's message is correct) 
    /// </summary>
    /// <param name="message">The message from client</param>
    /// <param name="botCommand">The bot's command for request to other service</param>
    /// <returns></returns>
    public async Task<bool> IsCommandValid(string message, BotCommand botCommand)
    {
        var result = botCommand switch
        {
            BotCommand.GetCpuMetricsByPeriod => new Regex(_regexExpressions["/cpu from"]).IsMatch(message),
            BotCommand.GetGpuMetricsByPeriod => new Regex(_regexExpressions["/gpu from"]).IsMatch(message),
            BotCommand.GetRamMetricsByPeriod => new Regex(_regexExpressions["/ram from"]).IsMatch(message),
            _ => false
        };

        
        return await Task.FromResult(result);
    }
    
}