using System;
using BlackJack.Enums;
using BlackJack.View;

namespace BlackJack.Input
{
    public static class InputRequester
    {
        public static EGameMode AskForGameMode()
        {
            while (true)
            {
                ConsoleView.Instance.Clear();
                ConsoleView.Instance.AskForGameMode();
                var choice = Input.Instance.ReadLine().ToUpper();

                if (!(choice.Equals("S") || choice.Equals("M")))
                    ConsoleView.Instance.PromptInvalidGameMode();

                return choice.Equals("S") ? EGameMode.SinglePlayer : EGameMode.MultiPlayer;
            }
        }

        public static int AskPlayerCount()
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

        public static bool AskPlayAgain()
        {
            ConsoleView.Instance.AskPlayAgainOrQuit();
            if (Console.ReadKey().KeyChar == 'q')
                return false;
            return true;
        }
    }
}