using BlackJack.Models;
using BlackJack.Players;

namespace BlackJack.Strategies
{
    public class DealerStrategy : IPlayerStrategy
    {
        public bool ShouldHit(APlayer player, Deck deck)
        {
            return true;
        }
    }
}