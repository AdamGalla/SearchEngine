using DataFormatter.FormatterLogic.Strategies;

namespace DataFormatter.StrategyFactory
{
    public interface IStrategyFactory
    {
        public IFormatterStrategy StrategyType (StrategyType strategyType);

        
    }
}
