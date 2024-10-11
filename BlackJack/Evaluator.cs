using System.Collections.Generic;
using System.Linq;
using BlackJack.Models;
using BlackJack.Players;

namespace BlackJack
{
    public class Evaluator
    {
        public APlayer DetermineWinner(List<APlayer> players)
        {
            if (players.Count == 1)
                return players[0];

            return players.OrderByDescending(p=>p.CalculateHandValue()).First();
            //Todo: What happens when players have same value? How to Determine Winner in that case?
        }
    }
}