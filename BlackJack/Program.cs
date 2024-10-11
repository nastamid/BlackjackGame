using System;
using BlackJack.Enums;
using BlackJack.Utils;
using BlackJack.View;

namespace BlackJack
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StartNewGame();
        }

        private static void StartNewGame()
        {
            var game = new BlackjackGame();

            while (true)
            {
                var gameMode = AskForGameMode();
                var playerCount = AskPlayerCount();
                
                game.Initialize(gameMode, playerCount);
                game.StartGame();
                game.Dispose();

                ConsoleView.Instance.AskPlayAgainOrQuit();
                if (Console.ReadKey().KeyChar == 'q')
                    break;
            }
        }

        private static EGameMode AskForGameMode()
        {
            while (true)
            {
                ConsoleView.Instance.Clear();
                ConsoleView.Instance.AskForGameMode();
                var choice = Input.Instance.ReadLine().ToUpper();

                if (!choice.Equals("S") || !choice.Equals("M"))
                    ConsoleView.Instance.PromptInvalidGameMode();

                return choice.Equals("S") ? EGameMode.SinglePlayer : EGameMode.MultiPlayer;
            }
        }

        private static int AskPlayerCount()
        {
            while (true)
            {
                ConsoleView.Instance.Clear();
                ConsoleView.Instance.AskHowManyPlayers();
                if (int.TryParse(Input.Instance.ReadLine(), out var playerCount) && playerCount > 0 && playerCount < 7)
                    return playerCount;
                ConsoleView.Instance.PromptInvalidInputPlayerNumber();
            }
        }
    }
}