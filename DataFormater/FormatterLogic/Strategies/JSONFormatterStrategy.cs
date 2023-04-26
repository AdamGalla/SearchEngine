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
            return String.Empty;
        }
        return await Task.Run(() => JsonConvert.SerializeObject(data));
    }
}
