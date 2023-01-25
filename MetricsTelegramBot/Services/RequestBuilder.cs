using System.Text.RegularExpressions;
using MetricsTelegramBot.Models.Request;
using MetricsTelegramBot.Models.Enums;

namespace MetricsTelegramBot.Services;

public class RequestBuilder
{
    /// <summary>
    /// This method builds the request string to an other service to receive data.
    /// </summary>
    /// <param name="message">The message from client</param>
    /// <param name="botCommand">The bot's command for concrete request to other service</param>
    /// <returns>The built request string</returns>
    public RequestModel BuildRequest(string message, BotCommand botCommand)
    {
        var dateTime = ParseDateTimeExpressions(message);

        return botCommand switch
        {
            BotCommand.GetAllCpuMetrics => new RequestModel {Uri = "http://localhost:7151/api/metrics/cpu/all"},
            BotCommand.GetAllGpuMetrics => new RequestModel {Uri = "http://localhost:7151/api/metrics/gpu/all"},
            BotCommand.GetAllRamMetrics => new RequestModel {Uri = "http://localhost:7151/api/metrics/ram/all"},
            BotCommand.GetCpuMetricsByPeriod => new RequestModel
            {
                Uri = "http://localhost:7151/api/metrics/cpu/from//to/",
                From = dateTime["from"],
                To = dateTime["to"]
            },
            BotCommand.GetGpuMetricsByPeriod => new RequestModel
            {
                Uri = "http://localhost:7151/api/metrics/gpu/from//to/",
                From = dateTime["from"],
                To = dateTime["to"]
            },
            BotCommand.GetRamMetricsByPeriod => new RequestModel
            {
                Uri = "http://localhost:7151/api/metrics/ram/from//to/",
                From = dateTime["from"],
                To = dateTime["to"]
            },
            _ => new RequestModel()
        };
    }

    /// <summary>
    /// Retrieve DateTime's expressions from the input message
    /// </summary>
    /// <param name="message">Input message from client</param>
    /// <returns>The method returns the dictionary, which contains the fallowing data:
    /// Key - The strings of the "from" and "to" strings</returns>
    /// Value - The DateTime from input message is converted to ToUnixTimeMilliseconds
    private Dictionary<string, long> ParseDateTimeExpressions(string message)
    {
        var correspondsOfDateTime = Regex.Matches(message, @"\{.*?\}");
        
        if (correspondsOfDateTime.Count > 0)
        {
            var firstDate = Regex.Replace(correspondsOfDateTime[0].Value, "[{}]", "");
            var secondDate = Regex.Replace(correspondsOfDateTime[1].Value, "[{}]", "");

            if ((DateTimeOffset.TryParse(firstDate, out DateTimeOffset from)) &
                (DateTimeOffset.TryParse(secondDate, out DateTimeOffset to)))
            {
                return new Dictionary<string, long>()
                {
                    {"from", from.ToUnixTimeMilliseconds()},
                    {"to", to.ToUnixTimeMilliseconds()}
                };
            }
        }
        return new Dictionary<string, long>()
        {
            {"from", 0},
            {"to", 0}
        };
    }
}