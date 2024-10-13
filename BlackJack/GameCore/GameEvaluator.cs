using System.Collections.Generic;
using BlackJack.Enums;
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
                new DrawStrategy(),
                new EveryoneBustedStrategy(),
                new DealerBustedStrategy()
            };
        }

        public EOutcomeType? Evaluate(Game game)
        {
            foreach (var strategy in _strategies)
            {
                var outcome = strategy.Execute(game);
                if (outcome != null)
                    return outcome;
            }

            return null;
        }
    }
}