using System.Linq;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerLosesStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Players.Count == 0)
                return null;
            
            var losers = game.Players.Where(p => p.HandValue < game.Dealer.HandValue).ToList();

            if (losers.Count == 0)
                return null;
            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.PlayerLoses,
                Players = losers
            };
        }
    }
}