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
        
        public void DisplayGameEnded()
        {
            Console.WriteLine("===== GAME ENDED =====");
        }

        public void PromptDealerWins()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Dealer - WON");
            Console.ResetColor();
        }

        public void PromptDealerLost()
        {
            Console.WriteLine($"Dealer - LOST");
        }

        public void PromptDealerBusted()
        {
            Console.WriteLine($"Dealer - BUSTED");
        }
        
        public void DisplayWinners(List<IPlayer> winnerPlayers)
        {

            foreach (var winner in winnerPlayers)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("{0} - WON!", winner.Name);
                // DisplayPlayerHand(winner);
                Console.ResetColor();

            }
        }

        public void DisplayLosers(List<IPlayer> losers)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            foreach (var loser in losers)
            {
                Console.WriteLine("{0} - LOST", loser.Name);
                // DisplayPlayerHand(loser);
            }
            
            Console.ResetColor();
        }

        public void DisplayBustedPlayers(List<IPlayer> bustedPlayers)
        {
            Console.BackgroundColor = ConsoleColor.Red;

            foreach (var bustedPlayer in bustedPlayers)
            {
                Console.WriteLine("{0} - BUSTED!", bustedPlayer.Name);
                // DisplayPlayerHand(bustedPlayer);
            }
            
            Console.ResetColor();
        }

        public void DisplayPlayersInDraw(List<IPlayer> drawPlayers)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;

            foreach (var drawPlayer in drawPlayers)
            {
                Console.WriteLine("{0} - DRAW!", drawPlayer.Name);
                // DisplayPlayerHand(drawPlayer);
                Console.WriteLine();
            }
            
            Console.ResetColor();
            Console.WriteLine();
        }
        
        public void DisplayBustedPlayer(IPlayer player)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{player.Name} is busted");
            DisplayPlayerHand(player);
            Console.ResetColor();
        }
        
        public void DisplayPlayerCardsAndValues(List<IPlayer> players)
        {
            foreach (var player in players)
                DisplayPlayerHand(player);
        }
        
        public void DisplayPlayerHand(IPlayer player)
        {
            Console.Write($"{player.Name}'s Hand Value = {player.HandValue.ToString()}, Hand: ");
            foreach (var card in player.Hand)
            {
                Console.Write($"|{card.Face}_{card.Suit}_{card.Value}|");
            }
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