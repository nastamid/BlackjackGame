using System;

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
            int playerCount = AskPlayerCount();
            BlackjackGame game = new BlackjackGame();
            game.Initialize(playerCount);
            game.StartGame();
        }

        private static int AskPlayerCount()
        {
            Console.WriteLine("How many players want to join Table?");
            if (int.TryParse(Console.ReadLine(), out var playerCount) && playerCount > 0 && playerCount < 7)
            {
                Console.Clear();
                return playerCount;
            }
            
            Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            return  AskPlayerCount();
        }
    }
}