using DataFormatter.FormatterLogic.Strategies;

namespace DataFormatter.StrategyFactory
{
    public class StrategyFactory : IStrategyFactory
    {
        IFormatterStrategy IStrategyFactory.StrategyType(StrategyType strategyType)
        {

            switch (strategyType)
            {
                case StrategyType.JSONFormatter:
                    return new JSONFormatterStrategy();

                case StrategyType.ExcelFormatter:
                    return new ExcelFormatterStrategy();

                default:
                    throw new ArgumentOutOfRangeException(nameof(strategyType), strategyType, null);
            }

        }

    }
}

