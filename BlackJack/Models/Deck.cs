using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.AppSettings;
using BlackJack.Utils;

namespace BlackJack.Models
{
    public class Deck
    {
        private List<Card> _cards;
        
        public Deck()
        {
            _cards = JsonReader.LoadCardsFromJson(Configurations.CardsJsonPath);
            Shuffle();
        }
        
        public void Shuffle()
        {
            var rand = new Random();
            _cards = _cards.OrderBy(c => rand.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0) 
                throw new InvalidOperationException("No more cards in the deck.");
            
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}