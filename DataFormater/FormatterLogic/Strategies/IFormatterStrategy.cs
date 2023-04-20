using DataFormatter.FormatterLogic.Model;

namespace DataFormatter.FormatterLogic.Strategies;

public interface IFormatterStrategy
{
    public string FormatText(FileData data);
}
