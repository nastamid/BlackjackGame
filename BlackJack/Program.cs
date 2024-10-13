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
            var jsonReader = new JsonReader();
            var gameCycleData = new GameCycleData()
            {
                Cards = jsonReader.LoadCardsFromJson(Configurations.CardsJsonPath),
                GameEvaluator = new GameEvaluator(),
                PlayerFactory = new PlayerFactory(),
                OutcomePresenter = new GameOutcomePresenter()
            };
            
            while (true)
            {
                if (!RunGameCycle(gameCycleData))
                    break;
            }
        }

        private static bool RunGameCycle(GameCycleData gameCycleData)
        {
            var gameMode = InputRequester.AskForGameMode();
            var playerCount = InputRequester.AskPlayerCount();

            var gameData = new GameData
            {
                Deck = new Deck(gameCycleData.Cards),
                Dealer = gameCycleData.PlayerFactory.CreateDealer(),
                Players = gameCycleData.PlayerFactory.CreatePlayersByMode(gameMode, playerCount)
            };

            using (var game = new Game(gameData))
            {
                game.Run();
                var outcomes = gameCycleData.GameEvaluator.Evaluate(game);
                gameCycleData.OutcomePresenter.PresentOutcomes(outcomes);
            }

            return InputRequester.AskPlayAgain();
        }
    }
}