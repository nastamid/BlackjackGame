using System.Collections.Generic;
using BlackJack.Models.Deck;
using BlackJack.Models.Players;

namespace BlackJack.Data
{
    public class GameData
    {
        public List<IPlayer> Players { get; set; }
        public IPlayer Dealer { get; set; }
        public IDeck Deck { get; set; }
    }
}