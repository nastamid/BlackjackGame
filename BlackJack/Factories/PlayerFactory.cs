using System.Collections.Generic;
using BlackJack.AppSettings;
using BlackJack.Players;
using BlackJack.Utils;
using BlackJack.View;

namespace BlackJack.Factories
{
    public class PlayerFactory
    {
        public APlayer CreateDealer()
        {
            return new Dealer();
        }

        public APlayer CreateHumanPlayer()
        {
            ConsoleView.Instance.EnterPlayerName();
            var name = Input.Instance.ReadLine();
            return new Human(name);
        }
        
        public List<APlayer> CreateHumanPlayers(int count)
        {
            var players = new List<APlayer>();
            for (var i = 0; i < count; i++)
            {
                ConsoleView.Instance.EnterPlayerName();
                var name = Input.Instance.ReadLine();
                if (string.IsNullOrEmpty(name))
                    name = Configurations.DefaultPlayerName + "_" + i;
                players.Add(new Human(name));
            }
            return players;
        }

        public List<APlayer> CreateBotPlayers(int count)
        {
            var players = new List<APlayer>();
            for (var i = 0; i < count; i++)
            {
                var botName = Configurations.DefaultBotName + "_" + (i + 1);
                players.Add(new Bot(botName));
            }
            return players;
        }
    }
}