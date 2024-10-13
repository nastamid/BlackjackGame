using System;
using System.Collections.Generic;
using BlackJack.AppSettings;
using BlackJack.Enums;
using BlackJack.Models.Players;
using BlackJack.View;

namespace BlackJack.Factories
{
    public class PlayerFactory
    {

        public List<IPlayer> CratePlayersByMode(EGameMode mode, int playerCount)
        {
            var players = new List<IPlayer>();

            switch (mode)
            {
                case EGameMode.SinglePlayer:
                    players.Add(CreateHumanPlayer());
                    players.AddRange(CreateBotPlayers(--playerCount));
                    break;
                case EGameMode.MultiPlayer:
                    players.AddRange(CreateHumanPlayers(playerCount));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            return players;
        }
        
        public Dealer CreateDealer()
        {
            return new Dealer();
        }

        public Human CreateHumanPlayer()
        {
            ConsoleView.Instance.EnterPlayerName();
            var name = Input.Input.Instance.ReadLine();
            return new Human(name);
        }

        public List<Human> CreateHumanPlayers(int count)
        {
            var players = new List<Human>();
            for (var i = 0; i < count; i++)
            {
                ConsoleView.Instance.EnterPlayerName();
                var name = Input.Input.Instance.ReadLine();
                if (string.IsNullOrEmpty(name))
                    name = Configurations.DefaultPlayerName + "_" + i;
                players.Add(new Human(name));
            }

            return players;
        }

        public List<Bot> CreateBotPlayers(int count)
        {
            var players = new List<Bot>();
            for (var i = 0; i < count; i++)
            {
                var botName = Configurations.DefaultBotName + "_" + (i + 1);
                players.Add(new Bot(botName));
            }

            return players;
        }
    }
}