using BlackJack.Models;
using BlackJack.Players;
using BlackJack.Utils;
using BlackJack.View;

namespace BlackJack.Strategies
{
    public class HumanStrategy : IPlayerStrategy
    {
        public bool ShouldHit(APlayer player, Deck deck)
        {
            while (true)
            {
                ConsoleView.Instance.DisplayPlayerHand(player);
                ConsoleView.Instance.DisplayQuestionForHitOrHold(player);
                var choice = Input.Input.Instance.ReadLine()?.ToUpper();
                if (choice != "H" && choice != "X")
                    continue;
                return choice == "H";
            }
        }
    }
}