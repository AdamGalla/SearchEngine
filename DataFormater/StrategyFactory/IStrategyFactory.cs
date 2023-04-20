using DataFormatter.FormatterLogic.Strategies;

namespace DataFormatter.StrategyFactory;
public interface IStrategyFactory
{
    IFormatterStrategy GetStrategyType(StrategyType strategyType);
}
