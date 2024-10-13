using System.Collections.Generic;
using BlackJack.Models;
using BlackJack.Players;

namespace BlackJack.Game
{
    public class GameData
    {
        public List<APlayer> Players { get; set; }
        public Dealer Dealer { get; set; }
        public Deck Deck { get; set; }
    }
}