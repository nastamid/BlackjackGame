using System;
using System.Collections.Generic;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.Models.Players;

namespace BlackJack.View
{
    // As we have only one view in this case it can be Singleton for easy access
    public class ConsoleView
    {
        private static ConsoleView _instance;
        public static ConsoleView Instance => _instance ?? (_instance = new ConsoleView());

        public void Clear()
        {
            //Console.Clear();
        }

        public void DisplayQuestionForHitOrHold(IPlayer player)
        {
            Console.WriteLine($"{player.Name} - Do you want to (H)it or (X)HOLD?");
        }

        public void EnterPlayerName()
        {
            Console.WriteLine("Enter player name: ");
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

        public void DisplayGameEnded()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("===== GAME ENDED =====");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void PromptDealerWins()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Dealer - WON");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void PromptDealerLost()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("Dealer - LOST");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void PromptDealerBusted()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Dealer - BUSTED");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayWinners(List<IPlayer> winnerPlayers)
        {
            foreach (var winner in winnerPlayers)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("{0} - WON!", winner.Name);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void DisplayLosers(List<IPlayer> losers)
        {
            foreach (var loser in losers)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("{0} - LOST", loser.Name);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void DisplayBustedPlayers(List<IPlayer> bustedPlayers)
        {
            foreach (var bustedPlayer in bustedPlayers)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("{0} - BUSTED!", bustedPlayer.Name);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void DisplayPlayersInDraw(List<IPlayer> drawPlayers)
        {
            foreach (var drawPlayer in drawPlayers)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("{0} - DRAW!", drawPlayer.Name);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void DisplayPlayerCardsAndValues(List<IPlayer> players)
        {
            foreach (var player in players)
                DisplayPlayerHand(player);
        }

        public void DisplayPlayerHand(IPlayer player)
        {
            Console.Write($"{player.Name}'s Hand Value = {player.HandValue.ToString()}, Hand: ");
            foreach (var card in player.Hand) Console.Write($"|{card.Face}_{card.Suit}_{card.Value}|");
            Console.WriteLine();
        }

        public void DisplayOutcomes(List<OutcomeData> outcomes)
        {
            Console.WriteLine("*** Outcome Report Start ***");
            foreach (var outcome in outcomes)
                DisplayOutcome(outcome);

            Console.WriteLine("--- Outcome Report End ---");
        }

        private void DisplayOutcome(OutcomeData outcome)
        {
            Console.WriteLine($"Outcome: {Enum.GetName(typeof(EOutcomeType), outcome.OutcomeType)}");
            Console.WriteLine("Players:");
            DisplayPlayerNames(outcome.Players);
            Console.WriteLine("---------");
        }

        public void DisplayPlayerNames(List<IPlayer> players)
        {
            foreach (var player in players)
                Console.WriteLine(player.Name);
        }

        public void EmptyLine()
        {
            Console.WriteLine();
        }
    }
}