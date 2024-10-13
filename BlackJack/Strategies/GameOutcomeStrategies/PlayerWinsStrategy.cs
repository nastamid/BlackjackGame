using System.Linq;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Extensions;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class PlayerWinsStrategy : IGameOutcomeStrategy
    {
        public OutcomeData GetOutcome(Game game)
        {
            // If Dealer is busted return all Non-busted Players, regardless their score

            if (game.Dealer.IsBusted())
                return new OutcomeData
                {
                    OutcomeType = EOutcomeType.PlayerWins,
                    Players = game.Players.GetNonBustedPlayers()
                };

            // If Dealer is not busted, players who have more than dealer is the winner
            var winners = game.Players.Where(p => p.HandValue > game.Dealer.HandValue).ToList();

            if (winners.Count != 0)
                return new OutcomeData
                {
                    OutcomeType = EOutcomeType.PlayerWins,
                    Players = winners
                };
            
            return null;
        }
    }
}