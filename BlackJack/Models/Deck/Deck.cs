using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Models.Deck
{
    public class Deck : IDeck
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