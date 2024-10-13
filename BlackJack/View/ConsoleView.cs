using System;
using System.Collections.Generic;
using BlackJack.Players;

namespace BlackJack.View
{
    // As we have only one view in this case it can be Singleton for easy access
    public class ConsoleView
    {
        private static ConsoleView _instance;
        public static ConsoleView Instance => _instance ?? (_instance = new ConsoleView());

        public void DisplayPlayerHand(APlayer aPlayer)
        {
            Console.Write($"{aPlayer.Name}'s Hand Value = {aPlayer.HandValue.ToString()}, Hand: ");
            foreach (var card in aPlayer.Hand)
            {
                Console.Write($"|{card.Face}_{card.Suit}_{card.Value}|");
            }

            Console.WriteLine();
        }

        public void DisplayBustedPlayer(APlayer aPlayer)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{aPlayer.Name} is busted");
            DisplayPlayerHand(aPlayer);
            Console.ResetColor();
        }

        public void DisplayQuestionForHitOrHold(APlayer aPlayer)
        {
            Console.WriteLine($"{aPlayer.Name} - Do you want to (H)it or (X)HOLD?");
        }

        public void DisplayWinner(List<APlayer> winnerPlayers)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            foreach (var winner in winnerPlayers)
            {
                Console.WriteLine("{0} is a Winner", winner.Name);
                DisplayPlayerHand(winner);
            }
            
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayLeftPlayerCardsAndValue(List<APlayer> players)
        {
            Console.WriteLine("Other Players:");
            foreach (var player in players)
                DisplayPlayerHand(player);
        }

        public void DisplayEndGame()
        {
            Console.WriteLine("===== GAME OVER =====");
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void EnterPlayerName()
        {
            Console.WriteLine($"Enter player name: ");
        }

        public void AskForGameMode()
        {
            Console.WriteLine("Select Game Mode: (S)inglePlayer or (M)ultiPlayer)");
        }

        public void AskHowManyPlayers()
        {
            Console.WriteLine("How many players want to join Table?");
        }

        public void PromptInvalidInputPlayerNumber()
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
        }

        public void PromptInvalidGameMode()
        {
            Console.WriteLine("Invalid input. Try S or M for selecting Game Mode");
        }

        public void AskPlayAgainOrQuit()
        {
            Console.WriteLine("Press 'q' to quit or any other key to play again");
        }

        public void PromptHit(string playerName)
        {
            Console.WriteLine($"{playerName}: HIT!");
        }
        
        public void PromptHold(string playerName)
        {
            Console.WriteLine($"{playerName}: HOLD!");
        }
    }
}