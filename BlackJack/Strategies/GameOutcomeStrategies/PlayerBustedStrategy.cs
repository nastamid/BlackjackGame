using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerBustedStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            if (!game.Players.IsAnyBusted())
                return null;

            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.PlayerBusted,
                Players = game.Players.GetBustedPlayers()
            };
        }
    }
}