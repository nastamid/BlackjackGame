using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public interface IGameOutcomeStrategy
    {
        OutcomeData GetOutcome(Game game);
    }
}