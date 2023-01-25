using MetricsTelegramBot.Models.Dto.Response;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MetricsTelegramBot.Services;

public class ResponseWriter
{
    private readonly string _responseDataFile;

    public ResponseWriter()
    {
        _responseDataFile = $"{Directory.GetCurrentDirectory()}/responseData.txt";
    }
    public async Task SendResponseToClientAsync(
        ITelegramBotClient botClient,
        Message message,
        IEnumerable<MetricDto> metrics)
    {
        var streamWriter = new StreamWriter(_responseDataFile);
        await streamWriter.WriteLineAsync("The response data of the selected PC metrics: \n");
        
        foreach (var metric in metrics)
        {
            await streamWriter.WriteLineAsync("------------------------------------");
            await streamWriter.WriteLineAsync(metric.ToString());
            await streamWriter.WriteLineAsync("------------------------------------\n");
            
        }
        streamWriter.Close();
        using var streamReader = new StreamReader(_responseDataFile);
        
        await botClient.SendDocumentAsync(
            chatId: message.Chat.Id,
            document: new InputFile(streamReader.BaseStream, fileName: "metrics.txt"));
        
    }
}