using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public interface IGameOutcomeStrategy
    {
        EOutcomeType? Execute(Game game);
    }
}