using BlackJack.Models.Deck;
using BlackJack.Models.Players;

namespace BlackJack.Strategies
{
    public class DealerStrategy : IPlayerStrategy
    {
        public bool ShouldHit(IPlayer player, IDeck deck)
        {
            return true;
        }
    }
}