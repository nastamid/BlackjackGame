using BlackJack.Models.Deck;
using BlackJack.Models.Players;
using BlackJack.View;

namespace BlackJack.Strategies.PlayerStrategies
{
    public class HumanStrategy : IPlayerStrategy
    {
        public bool ShouldHit(IPlayer player, IDeck deck)
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