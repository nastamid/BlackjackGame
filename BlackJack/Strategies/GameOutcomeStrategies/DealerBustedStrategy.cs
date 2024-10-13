using System.Linq;
using BlackJack.Enums;
using BlackJack.GameCore;
using BlackJack.View;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerBustedStrategy : IGameOutcomeStrategy
    {
        public EOutcomeType? Execute(Game game)
        {
            if (game.Dealer.IsBusted())
            {
                var winningPlayers = game.Players.Where(p => !p.IsBusted()).ToList();
                ConsoleView.Instance.DisplayEndGame();
                ConsoleView.Instance.DisplayWinner(winningPlayers);
                return EOutcomeType.PlayerWins;
            }

            return null;
        }
    }
}