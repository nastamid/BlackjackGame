using System.Collections.Generic;
using BlackJack.Enums;
using BlackJack.Models.Players;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class OutcomeData
    {
        public EOutcomeType OutcomeType { get; set; }
        public List<IPlayer> Players { get; set; }
    }
}