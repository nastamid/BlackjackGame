using System.Linq;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class EveryoneBustedStrategy : IGameOutcomeStrategy
    {
        public EOutcomeType? Execute(Game game)
        {
            if (game.Dealer.IsBusted() && game.Players.All(p => p.IsBusted()))
            {
                //return "Dealer wins as everyone else is busted.";
                return EOutcomeType.DealerWins;
            }

            return null;
        }
    }
}