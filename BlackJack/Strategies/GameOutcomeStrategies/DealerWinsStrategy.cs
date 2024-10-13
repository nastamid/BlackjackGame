using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerWinsStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Dealer.IsBusted())
                return null;
            
            if (game.Players.AreAllBusted() || !game.Players.Any(p=>p.HandValue >= game.Dealer.HandValue))
                return new OutcomeData()
                {
                    OutcomeType = EOutcomeType.DealerWins,
                    Players = new List<IPlayer>() { game.Dealer }
                };
            
            return null;
        }
    }
}