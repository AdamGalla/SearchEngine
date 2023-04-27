using Common;
using DataFormatter.FormatterLogic.Model;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;

namespace DataFormatter.FormatterLogic.Strategies;

public class JSONFormatterStrategy : EventSource, IFormatterStrategy
{
    public async Task<string> FormatTextAsync(FileData data)
    {
        using var activity = Monitoring.ActivitySource.StartActivity();

        if (data == null)
        {
            Monitoring.Log.Warning("File data is null");
            return String.Empty;
        }
        Monitoring.Log.Information("Formating text data from {Data}", data);
        return await Task.Run(() => JsonConvert.SerializeObject(data));
    }
}
