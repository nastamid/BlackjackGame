using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DrawStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Players.Count == 0)
                return null;
            
            var drawPlayers = game.Players.Where(p => p.HandValue == game.Dealer.HandValue).ToList();

            if (drawPlayers.Count == 0)
                return null;
            
            return new OutcomeData
            {
                OutcomeType = EOutcomeType.Draw,
                Players = drawPlayers
            };
        }
    }
}