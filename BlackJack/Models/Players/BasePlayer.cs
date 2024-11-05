using System.Collections.Generic;
using System.Linq;
using BlackJack.Models.Deck;
using BlackJack.Strategies;
using BlackJack.Strategies.PlayerStrategies;

namespace BlackJack.Models.Players
{
    public abstract class BasePlayer : IPlayer
    {
        private readonly IPlayerStrategy _strategy;

        public BasePlayer(string name, IPlayerStrategy strategy)
        {
            Name = name;
            _strategy = strategy;
            Hand = new List<Card>();
        }

        public string Name { get; }
        public List<Card> Hand { get; }
        public int HandValue => Hand.Sum(c => c.Value);

        public bool IsBusted()
        {
            return HandValue > 21;
        }

        public bool TakeTurn(IDeck deck)
        {
            return _strategy.ShouldHit(this);
        }

        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }
    }
}