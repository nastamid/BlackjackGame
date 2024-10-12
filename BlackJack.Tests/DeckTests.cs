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
        private readonly Mock<IJsonReader> _mockJsonReader;

        public DeckTests()
        {
            _mockJsonReader = new Mock<IJsonReader>();
        }

        [Fact]
        public void Deck_Constructor_ShouldShuffleCards()
        {
            // Arrange

            var expectedCards = new List<Card>
            {
                new Card("Hearts", "Ace", 11),
                new Card("Spades", "King", 10),
                new Card("Diamonds", "Queen", 10)
            };
            _mockJsonReader.Setup(reader => reader.LoadCardsFromJson(It.IsAny<string>())).Returns(expectedCards);

            // Act
            var deck = new Deck();

            // Assert
            Assert.NotEqual(expectedCards, deck.Cards);
        }

        [Fact]
        public void Shuffle_ShouldRandomizeCardOrder()
        {
            // Arrange
            var cards = new List<Card> { new Card(), new Card(), new Card() };
            _mockJsonReader.Setup(reader => reader.LoadCardsFromJson(It.IsAny<string>())).Returns(cards);

            var deck = new Deck();

            // Act
            var shuffledCards = deck.Cards;

            // Assert
            Assert.NotEqual(cards, shuffledCards);
        }

        [Fact]
        public void DrawCard_ShouldReturnFirstCard()
        {
            // Arrange
            var cards = new List<Card> { new Card { Value = "Ace" }, new Card { Value = "King" } };
            _mockJsonReader.Setup(reader => reader.LoadCardsFromJson(It.IsAny<string>())).Returns(cards);

            var deck = new Deck();

            // Act
            var drawnCard = deck.DrawCard();

            // Assert
            Assert.Equal("Ace", drawnCard.Value);
        }

        [Fact]
        public void DrawCard_ShouldThrowExceptionWhenDeckIsEmpty()
        {
            // Arrange
            var deck = new Deck();
            while (deck.Cards.Count > 0) deck.DrawCard();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
        }
    }
}