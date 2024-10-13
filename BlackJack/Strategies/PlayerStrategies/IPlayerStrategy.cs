using BlackJack.Models.Deck;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.PlayerStrategies
{
    public interface IPlayerStrategy
    {
        bool ShouldHit(IPlayer player, IDeck deck);
    }
}