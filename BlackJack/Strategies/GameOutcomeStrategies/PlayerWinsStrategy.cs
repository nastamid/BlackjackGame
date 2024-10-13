using System.Linq;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerWinsStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Players.Count == 0)
                return null;
            
            var winners = game.Players.Where(p => p.HandValue > game.Dealer.HandValue).ToList();

            if (winners.Count == 0)
                return null;
            
            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.PlayerWins,
                Players = winners
            };
        }
    }
}