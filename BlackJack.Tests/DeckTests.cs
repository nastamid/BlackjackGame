using System;
using System.Collections.Generic;
using BlackJack.Models;
using BlackJack.Models.Deck;
using NUnit.Framework;

namespace BlackJack.Tests
{
    public class DeckTests
    {
        private List<Card> _cards;

        [SetUp]
        public void Setup()
        {
            _cards = new List<Card>
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
        }

        [Test]
        public void Shuffle_ShouldRandomizeCardOrder()
        {
            // Arrange
            var deck = new Deck(_cards);

            // Act
            deck.Shuffle();

            // Assert
            Assert.AreNotEqual(_cards, deck.Cards);
        }

        [Test]
        public void DrawCard_ShouldReturnFirstCard()
        {
            // Arrange
            var deck = new Deck(_cards);

            // Act
            var drawnCard = deck.DrawCard();

            // Assert
            Assert.AreEqual("Hearts", drawnCard.Suit);
            Assert.AreEqual("Ace", drawnCard.Face);
            Assert.AreEqual(11, drawnCard.Value);
        }

        [Test]
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