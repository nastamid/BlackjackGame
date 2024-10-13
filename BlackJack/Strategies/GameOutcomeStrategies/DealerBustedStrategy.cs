using System.Linq;
using BlackJack.Enums;
using BlackJack.Game;
using BlackJack.View;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerBustedStrategy : IGameOutcomeStrategy
    {
        public void Execute(BlackjackGame game)
        {
            if (game.Dealer.IsBusted())
            {
                var winningPlayers = game.Players.Where(p => !p.IsBusted()).ToList();
                ConsoleView.Instance.DisplayEndGame();
                ConsoleView.Instance.DisplayWinner(winningPlayers);
            }
        }
    }
}