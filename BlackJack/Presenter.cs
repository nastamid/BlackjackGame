using System;

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
    }
}