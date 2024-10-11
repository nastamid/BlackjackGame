using System.Collections.Generic;
using System.Linq;
using BlackJack.Players;

namespace BlackJack.Utils
{
    public class Evaluator
    {
        public APlayer DetermineWinner(List<APlayer> players)
        {
            if (players.Count == 1)
                return players[0];

            return players.OrderByDescending(p=>p.HandValue).First();
            //Todo: What happens when players have same value? How to Determine Winner in that case?
        }
    }
}