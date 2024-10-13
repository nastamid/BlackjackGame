using System.Linq;
using BlackJack.Enums;
using BlackJack.Game;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class EveryoneBustedStrategy : IGameOutcomeStrategy
    {
        public void Execute(BlackjackGame game)
        {
            if (game.Dealer.IsBusted() && game.Players.All(p => p.IsBusted()))
            {
                //return "Dealer wins as everyone else is busted.";
            }
        }
    }
}