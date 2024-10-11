using BlackJack.Models;
using BlackJack.Players;

namespace BlackJack.Strategies
{
    public interface IPlayerStrategy
    {
        void Execute(APlayer aPlayer, Deck deck);
    }
}