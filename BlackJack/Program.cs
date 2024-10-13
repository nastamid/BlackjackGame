using BlackJack.AppSettings;
using BlackJack.Data;
using BlackJack.Factories;
using BlackJack.GameCore;
using BlackJack.Input;
using BlackJack.Models.Deck;
using BlackJack.Presenters;
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
            var deck = new Deck(jsonReader.LoadCardsFromJson(Configurations.CardsJsonPath)); // Skipping Error handling
            var presenter = new GameOutcomePresenter();

            while (true)
            {
                var gameMode = InputRequester.AskForGameMode();
                var playerCount = InputRequester.AskPlayerCount();

                var gameData = new GameData
                {
                    Deck = deck,
                    Dealer = playerFactory.CreateDealer(),
                    Players = playerFactory.CratePlayersByMode(gameMode, playerCount)
                };

                var game = new Game(gameData);

                game.Run();
                var outcomes = gameEvaluator.Evaluate(game);
                presenter.PresentOutcomes(outcomes);
                game.Dispose();

                if (!InputRequester.AskPlayAgain())
                    break;
            }
        }
    }
}