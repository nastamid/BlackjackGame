using System.Collections.Generic;
using BlackJack.Models.Deck;

namespace BlackJack.Models.Players
{
    public interface IPlayer
    {
        string Name { get; }
        List<Card> Hand { get; }
        int HandValue { get; }
        bool IsBusted();
        bool TakeTurn(IDeck deck);
        void AddCardToHand(Card card);
    }
}