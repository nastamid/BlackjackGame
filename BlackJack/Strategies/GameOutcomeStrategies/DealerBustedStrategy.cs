using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DealerBustedStrategy : IGameOutcomeStrategy
    {
        
        //If the dealer goes bust, then all players who have not busted (i.e., have a hand value of 21 or less)
        //are considered winners, regardless of their exact hand value.
        
        public OutcomeData GetOutcome(Game game)
        {
            if (!game.Dealer.IsBusted()) 
                return null;
            
            var winners = game.Players.Where(p => !p.IsBusted()).ToList();
            
            // ConsoleView.Instance.DisplayEndGame();
            // ConsoleView.Instance.DisplayWinner(winningPlayers);

            if (winners.Count == 0)
                return null;
                
            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.DealerBusted,
                Players = winners
            };
        }
    }
}