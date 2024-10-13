using BlackJack.Models.Deck;
using BlackJack.Models.Players;

namespace BlackJack.Strategies
{
    public interface IPlayerStrategy
    {
        bool ShouldHit(IPlayer player, IDeck deck);
    }
}