using BlackJack.Models.Players;
using BlackJack.View;

namespace BlackJack.Strategies.PlayerStrategies
{
    public class AIStrategy : IPlayerStrategy
    {
        public bool ShouldHit(IPlayer player)
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