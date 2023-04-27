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
            Monitoring.Log.Fatal("file data for is null");
            return String.Empty;
        }
        Monitoring.Log.Information("format text data");
        return await Task.Run(() => JsonConvert.SerializeObject(data));
       // Monitoring.Log.Debug("format text async");
    }
}
