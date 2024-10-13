using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerBustedStrategy : IGameOutcomeStrategy
    {
        //If the dealer goes bust, then all players who have not busted (i.e., have a hand value of 21 or less)
        //are considered winners, regardless of their exact hand value.

        public OutcomeData GetOutcome(Game game)
        {
            var winners = game.Players.GetNonBustedPlayers().ToList();

            if (game.Dealer.IsBusted() && winners.Count != 0)
                return new OutcomeData
                {
                    OutcomeType = EOutcomeType.DealerBusted,
                    Players = new List<IPlayer> { game.Dealer }
                };
            return null;
        }
    }
}