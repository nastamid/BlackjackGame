using System.Collections.Generic;
using BlackJack.Strategies.GameOutcomeStrategies;

namespace BlackJack.GameCore
{
    public class GameEvaluator
    {
        private readonly List<IGameOutcomeStrategy> _strategies;

        public GameEvaluator()
        {
            _strategies = new List<IGameOutcomeStrategy>
            {
                new DealerBustedStrategy(),
                new DealerLosesStrategy(),
                new DealerWinsStrategy(),
                
                new PlayerBustedStrategy(),
                new PlayerLosesStrategy(),
                new PlayerWinsStrategy(),
                
                new DrawStrategy(),
                new EveryoneBustedStrategy()
            };
        }

        public List<OutcomeData> Evaluate(Game game)
        {
            var outcomes = new List<OutcomeData>();
            
            foreach (var strategy in _strategies)
            {
                var outcome = strategy.GetOutcome(game);
                
                if (outcome != null)
                    outcomes.Add(outcome);
            }
            return outcomes;
        }
    }
}