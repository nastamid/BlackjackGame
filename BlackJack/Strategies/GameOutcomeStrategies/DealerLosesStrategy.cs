using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerLosesStrategy: IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Players.AreAllBusted())
                return null;

            if (game.Dealer.IsBusted())
                return null;
            
            var winners = game.Players.Where(p => p.HandValue > game.Dealer.HandValue).ToList();

            if (winners.Count == 0)
                return null;

            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.DealerLoses,
                Players = new List<IPlayer>() { game.Dealer }
            };
        }
    }
}