using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class Presenter
    {
        public void DisplayPlayerHand(Player player)
        {
            Console.Write($"{player.Name}'s Value = {player.CalculateHandValue().ToString()} hand: ");
            foreach (var card in player.Hand)
            {
                Console.Write($"|{card.Face}_{card.Suit}_{card.Value}|");
            }
            Console.WriteLine();
        }

        public void DisplayBustedPlayer(Player player)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{player.Name} is busted");
            DisplayPlayerHand(player);
            Console.ResetColor();
        }

        public void DisplayQuestionForHitOrHold(Player player)
        {
            Console.WriteLine($"{player.Name} - Do you want to (H)it or (X)HOLD?");
        }

        public void DisplayWinner(Player player)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("{0} is a Winner", player.Name);
            DisplayPlayerHand(player);
            Console.ResetColor();
        }

        public void DisplayLeftPlayerCardsAndValue(List<Player> players)
        {
                Console.WriteLine("Other Players:");
                foreach (var player in players)
                    DisplayPlayerHand(player);
        }
        public void DisplayEndGame()
        {
            Clear();
            Console.WriteLine("===== GAME OVER =====");
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}