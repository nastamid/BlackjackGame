using System.Collections.Generic;
using BlackJack.Enums;
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
            
            if (game.Players.Count == 0)
                return new OutcomeData()
                {
                    OutcomeType = EOutcomeType.DealerWins,
                    Players = new List<IPlayer>() { game.Dealer }
                };
            
            return null;
        }
    }
}