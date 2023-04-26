using Common;
using DataFormatter.FormatterLogic.Strategies;
using System.Diagnostics.Tracing;

namespace DataFormatter.StrategyFactory;

public class StrategyFactory : EventSource, IStrategyFactory
{
    public IFormatterStrategy GetStrategyType(StrategyType strategyType)
    {
        using var activity = Monitoring.ActivitySource.StartActivity();
        return strategyType switch
        {
            StrategyType.JSONFormatter => new JSONFormatterStrategy(),
            StrategyType.XMLFormatter => new XMLFormatterStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(strategyType), strategyType, null),
        };
    }
}

