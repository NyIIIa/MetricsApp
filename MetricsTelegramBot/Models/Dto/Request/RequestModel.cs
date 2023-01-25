namespace MetricsTelegramBot.Models.Request;

public class RequestModel
{
    public string Uri { get; set; } = "";
    public long From { get; set; }
    public long To { get; set; }

    public override string ToString()
    {
        if (From != 0 & To != 0)
        {
            var fromIndex = Uri.IndexOf("from");
            //Inserting 'From' property in the Uri's string
            var firstInsert = Uri.Insert(fromIndex + 5, From.ToString());

            var toIndex = firstInsert.IndexOf("to");
            //Inserting 'To' property in the firstInsert's string(Uri's string)
            var result = firstInsert.Insert(toIndex + 3, To.ToString());

            return result;
        }
        
        else if(Uri != string.Empty)
        {
            return Uri;
        }

        return string.Empty;
    }
}