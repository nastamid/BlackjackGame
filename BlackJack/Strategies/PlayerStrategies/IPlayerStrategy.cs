using BlackJack.Models.Players;

namespace BlackJack.Strategies.PlayerStrategies
{
    public interface IPlayerStrategy
    {
        bool ShouldHit(IPlayer player);
    }
}