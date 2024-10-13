using BlackJack.Models;
using BlackJack.Players;
using BlackJack.View;

namespace BlackJack.Strategies
{
    public class AIStrategy : IPlayerStrategy
    {
        public bool ShouldHit(APlayer player, Deck deck)
        {
            var isHit = player.HandValue < 17;

            if (isHit)
                ConsoleView.Instance.PromptHit(player.Name);
            else
                ConsoleView.Instance.PromptHold(player.Name);

            return isHit;
        }
    }
}