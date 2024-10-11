using BlackJack.Models;
using BlackJack.Players;

namespace BlackJack.Strategies
{
    public interface IPlayerStrategy
    {
        bool ShouldHit(APlayer player, Deck deck);
    }
}