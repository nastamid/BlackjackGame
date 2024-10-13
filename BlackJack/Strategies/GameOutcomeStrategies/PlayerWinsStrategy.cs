﻿using System.Linq;
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
            if (game.Players.AreAllBusted())
                return null;
            
            var winners = game.Players.Where(p => p.HandValue > game.Dealer.HandValue).ToList();

            if (winners.Count == 0)
                return null;
            
            return new OutcomeData()
            {
                OutcomeType = EOutcomeType.PlayerWins,
                Players = winners
            };
        }
    }
}