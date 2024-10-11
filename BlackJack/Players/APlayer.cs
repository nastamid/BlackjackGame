using System.Collections.Generic;
using System.Linq;
using BlackJack.Models;
using BlackJack.Strategies;

namespace BlackJack.Players
{
    public abstract class APlayer
    {
        public string Name { get; }
        public List<Card> Hand { get; }

        private IPlayerStrategy _strategy;

        public APlayer(string name, IPlayerStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
            Hand = new List<Card>();
        }

        public void TakeTurn(Deck deck)
        {
            _strategy.Execute(this, deck);
        }

        public void AddCardToHand(Card card)
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