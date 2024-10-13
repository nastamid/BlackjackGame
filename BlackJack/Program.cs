using System;
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
            var presenter = new GameOutcomePresenter();

            while (true)
            {
                if (!RunGameCycle(gameEvaluator, playerFactory, jsonReader, presenter))
                    break;
            }
        }

        private static bool RunGameCycle(GameEvaluator gameEvaluator, PlayerFactory playerFactory,
            JsonReader jsonReader, GameOutcomePresenter presenter)
        {
            var gameMode = InputRequester.AskForGameMode();
            var playerCount = InputRequester.AskPlayerCount();

            Deck deck;
            try
            {
                deck = new Deck(jsonReader.LoadCardsFromJson(Configurations.CardsJsonPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading deck: {ex.Message}");
                return true;
            }

            var gameData = new GameData
            {
                Deck = deck,
                Dealer = playerFactory.CreateDealer(),
                Players = playerFactory.CratePlayersByMode(gameMode, playerCount)
            };

            using (var game = new Game(gameData))
            {
                game.Run();
                var outcomes = gameEvaluator.Evaluate(game);
                presenter.PresentOutcomes(outcomes);
            }

            return InputRequester.AskPlayAgain();
        }
    }
}