using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    public class Player
    {
        public string Name { get; }
        public bool IsDealer { get; }
        public List<Card> Hand { get; }

        public Player(string name, bool isDealer = false)
        {
            Name = name;
            IsDealer = isDealer;
            Hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public int CalculateHandValue()
        {
            return Hand.Sum(c=>c.Value);
        }

        public bool IsBusted() => CalculateHandValue() > 21;
    }
}