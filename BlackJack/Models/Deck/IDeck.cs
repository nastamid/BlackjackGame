using System.Collections.Generic;

namespace BlackJack.Models.Deck
{
    public interface IDeck
    {
        List<Card> Cards { get; }
        void Shuffle();
        Card DrawCard();
    }
}