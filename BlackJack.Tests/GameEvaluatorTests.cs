using System.Collections.Generic;
using System.Linq;
using BlackJack.Enums;
using BlackJack.GameCore;
using BlackJack.Models.Deck;
using BlackJack.Models.Players;
using Moq;
using NUnit.Framework;

namespace BlackJack.Tests
{
    public class GameEvaluatorTests
    {
        private GameEvaluator _evaluator;
        private Game _game;
        private Mock<IDeck> _mockDeck;
        private Mock<IPlayer> _mockDealer;
        private Mock<IPlayer> _mockPlayer;

        [SetUp]
        public void Setup()
        {
            _mockDeck = new Mock<IDeck>();
            _mockDealer = new Mock<IPlayer>();
            _mockPlayer = new Mock<IPlayer>();
            var players = new List<IPlayer> { _mockPlayer.Object };

            var gameData = new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = players
            };

            _game = new Game(gameData);
            _evaluator = new GameEvaluator();
        }


        [Test]
        public void Evaluate_DealerBusted_ShouldDeclarePlayerWinner()
        {
            // Arrange
            _mockDealer.Setup(d => d.IsBusted()).Returns(true);
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false);
            _mockPlayer.Setup(p => p.HandValue).Returns(18);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.PlayerWins));
        }

        [Test]
        public void Evaluate_PlayerBusted_ShouldDeclareDealerWinner()
        {
            // Arrange
            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockPlayer.Setup(p => p.IsBusted()).Returns(true);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
        }

        [Test]
        public void Evaluate_PlayerHasHigherValue_ShouldDeclarePlayerWinner()
        {
            // Arrange
            _mockDealer.Setup(d => d.HandValue).Returns(17);
            _mockPlayer.Setup(p => p.HandValue).Returns(19);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.PlayerWins));
        }

        [Test]
        public void Evaluate_DealerHasHigherValue_ShouldDeclareDealerWinner()
        {
            // Arrange
            _mockDealer.Setup(d => d.HandValue).Returns(20);
            _mockPlayer.Setup(p => p.HandValue).Returns(18);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
        }

        [Test]
        public void Evaluate_PlayerAndDealerDraw_ShouldDeclareDraw()
        {
            // Arrange
            _mockDealer.Setup(d => d.HandValue).Returns(18);
            _mockPlayer.Setup(p => p.HandValue).Returns(18);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.Draw));
        }

        [Test]
        public void Evaluate_EveryoneBusted_ShouldDeclareEveryoneBusted()
        {
            // Arrange
            _mockDealer.Setup(d => d.IsBusted()).Returns(true);
            _mockPlayer.Setup(p => p.IsBusted()).Returns(true);

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
        }

        public void Cleanup()
        {
            _game.Dispose();
        }
    }
}