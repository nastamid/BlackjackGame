using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.GameCore;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class EveryoneBustedStrategy : IGameOutcomeStrategy
    {
        //Dealer wins as everyone is busted.;
        
        public OutcomeData GetOutcome(Game game)
        {
            if (game.Dealer.IsBusted() && game.Players.All(p => p.IsBusted()))
            {
                return new OutcomeData()
                {
                    OutcomeType = EOutcomeType.DealerWins,
                    Players = new List<IPlayer>() { game.Dealer }
                };
            }

            return null;
        }
    }
}