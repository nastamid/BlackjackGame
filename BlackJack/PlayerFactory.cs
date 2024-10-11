using System.Collections.Generic;

namespace BlackJack
{
    public class PlayerFactory
    {
        private  const string DefaultPlayerName = "Player";
        
        public List<Player> CreatePlayers(int playerCount)
        {
            var players = new List<Player>();

            for (var i = 0; i < playerCount; i++)
                players.Add(new Player($"{DefaultPlayerName}_{(i + 1).ToString()}"));
            
            players.Add(new Player("Dealer", true));
            return players;
        }
    }
}