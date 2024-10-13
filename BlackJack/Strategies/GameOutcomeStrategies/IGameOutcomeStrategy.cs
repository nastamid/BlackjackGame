using BlackJack.Enums;
using BlackJack.Game;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public interface IGameOutcomeStrategy
    {
        void Execute(BlackjackGame game);
    }
}