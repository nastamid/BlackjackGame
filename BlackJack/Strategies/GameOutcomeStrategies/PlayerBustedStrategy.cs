using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerBustedStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (game.BustedPlayers.Count == 0)
                return null;

            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.PlayerBusted,
                Players = game.BustedPlayers
            };
        }
    }
}