using System.Collections.Generic;

namespace BlackJack
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Hand { get; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public bool IsBusted() => Hand.Count > 21;
    }
}