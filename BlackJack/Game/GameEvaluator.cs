using System.Collections.Generic;
using BlackJack.Strategies.GameOutcomeStrategies;

namespace BlackJack.Game
{
    public class GameEvaluator
    {
        private readonly List<IGameOutcomeStrategy> _strategies;

        public GameEvaluator()
        {
            _strategies = new List<IGameOutcomeStrategy>()
            {
                new DrawStrategy(),
                new EveryoneBustedStrategy(),
                new DealerBustedStrategy()
            };
        }

        public void Evaluate(BlackjackGame game)
        {
            foreach (var strategy in _strategies)
                strategy.Execute(game);
        }
    }
}