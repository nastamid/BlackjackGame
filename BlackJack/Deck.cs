using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BlackJack
{
    public class Deck
    {
        private List<Card> _cards;
        
        public Deck(string jsonFilePath)
        {
            _cards = LoadCardsFromJson(jsonFilePath);
            Shuffle();
        }
        
        private List<Card> LoadCardsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CardWrapper>(json).Cards;
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