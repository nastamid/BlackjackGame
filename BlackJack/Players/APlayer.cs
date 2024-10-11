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
        public int HandValue => Hand.Sum(c=>c.Value);
        public bool IsBusted() => HandValue > 21;

        private readonly IPlayerStrategy _strategy;

        public APlayer(string name, IPlayerStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
            Hand = new List<Card>();
        }

        public bool TakeTurn(Deck deck)
        {
            return _strategy.ShouldHit(this, deck);
        }

        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }
    }
}