using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.AppSettings;
using BlackJack.Utils;

namespace BlackJack.Models
{
    public class Deck
    {
        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        public List<Card> Cards { get; private set; }

        public void Shuffle()
        {
            var rand = new Random();
            Cards = Cards.OrderBy(c => rand.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (Cards.Count == 0)
                throw new InvalidOperationException("No more cards in the deck.");

            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}