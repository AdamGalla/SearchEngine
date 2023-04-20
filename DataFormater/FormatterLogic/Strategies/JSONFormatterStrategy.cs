using DataFormatter.FormatterLogic.Model;
using Newtonsoft.Json;

namespace DataFormatter.FormatterLogic.Strategies;

public class JSONFormatterStrategy : IFormatterStrategy
{
    public async Task<string> FormatTextAsync(FileData data)
    {
        if(data == null)
        {
            return String.Empty;
        }
        return await Task.Run(() => JsonConvert.SerializeObject(data));
    }
}
