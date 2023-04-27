using Common;
using DataFormatter.FormatterLogic.Strategies;
using System.Diagnostics.Tracing;

namespace DataFormatter.StrategyFactory;

public class StrategyFactory : EventSource, IStrategyFactory
{
    public IFormatterStrategy GetStrategyType(StrategyType strategyType)
    {
        Monitoring.Log.Debug($"get stragety type");
        using var activity = Monitoring.ActivitySource.StartActivity();
        return strategyType switch
        {
            StrategyType.JSONFormatter => new JSONFormatterStrategy(),
            StrategyType.XMLFormatter => new XMLFormatterStrategy(),
           // Monitoring.Log.Debug(ArgumentOutOfRangeException);
            _ => throw new ArgumentOutOfRangeException(nameof(strategyType), strategyType, null),
          
        };

    }
}

