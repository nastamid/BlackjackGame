using BlackJack.AppSettings;
using BlackJack.Factories;
using BlackJack.Game;
using BlackJack.Input;
using BlackJack.Models;
using BlackJack.Utils;

namespace BlackJack
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var gameEvaluator = new GameEvaluator();
            var playerFactory = new PlayerFactory();
            var jsonReader = new JsonReader();
            var deck = new Deck(jsonReader.LoadCardsFromJson(Configurations.CardsJsonPath));
            
            while (true)
            {
                var gameMode = InputRequester.AskForGameMode();
                var playerCount = InputRequester.AskPlayerCount();

                var gameData = new GameData()
                {
                    Deck = deck,
                    Dealer = playerFactory.CreateDealer(),
                    Players = playerFactory.GetPlayersByMode(gameMode, playerCount)
                };
                
                var game = new BlackjackGame(gameData);

                game.Run();
                gameEvaluator.Evaluate(game);
                game.Dispose();

                if(!InputRequester.AskPlayAgain())
                    break;
            }
        }
    }
}