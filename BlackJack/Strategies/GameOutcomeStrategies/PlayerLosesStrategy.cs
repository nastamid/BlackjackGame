using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerLosesStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Players.AreAllBusted())
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