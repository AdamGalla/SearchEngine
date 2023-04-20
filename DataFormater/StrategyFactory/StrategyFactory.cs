using Common;
using DataFormatter.FormatterLogic.Strategies;

namespace DataFormatter.StrategyFactory;

public class StrategyFactory : IStrategyFactory
{
    public IFormatterStrategy GetStrategyType(StrategyType strategyType)
    {
        using var activity = Monitoring.ActivitySource.StartActivity();
        return strategyType switch
        {
            StrategyType.JSONFormatter => new JSONFormatterStrategy(),
            StrategyType.ExcelFormatter => new ExcelFormatterStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(strategyType), strategyType, null),
        };
    }
}

