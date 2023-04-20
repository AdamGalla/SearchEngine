using DataFormatter.FormatterLogic.Model;

namespace DataFormatter.FormatterLogic.Strategies;

public interface IFormatterStrategy
{
    Task<string> FormatTextAsync(FileData data);
}
