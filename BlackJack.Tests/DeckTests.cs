using System;
using System.Collections.Generic;
using BlackJack.Models;
using BlackJack.Utils;
using Moq;
using Xunit;

namespace BlackJack.Tests
{
    public class DeckTests
    {
        private readonly List<Card> _cards = new List<Card>
        {
            new Card("Hearts", "Ace", 11),
            new Card("Spades", "King", 10),
            new Card("Diamonds", "Queen", 10),
            new Card("Clubs", "Jack", 10),
            new Card("Hearts", "10", 10),
            new Card("Spades", "9", 9),
            new Card("Diamonds", "8", 8),
            new Card("Clubs", "7", 7),
            new Card("Hearts", "6", 6),
            new Card("Spades", "5", 5)
        }; 
        
        [Fact]
        public void Shuffle_ShouldRandomizeCardOrder()
        {
            // Arrange
            var deck = new Deck(_cards);

            // Act
            deck.Shuffle();

            // Assert
            Assert.NotEqual(_cards, deck.Cards);
        }

        [Fact]
        public void DrawCard_ShouldReturnFirstCard()
        {
            // Arrange
            var deck = new Deck(_cards);
            
            // Act
            var drawnCard = deck.DrawCard();
            
            // Assert
            Assert.Equal("Hearts", drawnCard.Suit);
            Assert.Equal("Ace", drawnCard.Face);
            Assert.Equal(11, drawnCard.Value);
        }

        [Fact]
        public void DrawCard_ShouldThrowExceptionWhenDeckIsEmpty()
        {
            // Arrange
            var deck = new Deck(_cards);
            
            while (deck.Cards.Count > 0) 
                deck.DrawCard();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
        }
    }
}