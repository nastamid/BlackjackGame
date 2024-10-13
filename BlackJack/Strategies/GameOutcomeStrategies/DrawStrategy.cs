using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DrawStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            var drawPlayers = game.Players.GetNonBustedPlayers()
                .Where(p => p.HandValue == game.Dealer.HandValue).ToList();

            if (drawPlayers.Count != 0)
            {
                drawPlayers.Add(game.Dealer);

                return new OutcomeData
                {
                    OutcomeType = EOutcomeType.Draw,
                    Players = drawPlayers
                };
            }

            return null;
        }
    }
}