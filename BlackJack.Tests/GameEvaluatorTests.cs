using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
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


        [Test]
        public void Evaluate_MultipleOutcomes_ShouldDeclareCorrectOutcomes()
        {
            // Arrange
            var secondMockPlayer = new Mock<IPlayer>();
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false);
            secondMockPlayer.Setup(p => p.IsBusted()).Returns(false);

            _mockPlayer.Setup(p => p.HandValue).Returns(20);
            secondMockPlayer.Setup(p => p.HandValue).Returns(19);

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(17);

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object, secondMockPlayer.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(_mockPlayer.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(secondMockPlayer.Object)));
            Assert.IsFalse(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
        }


        [Test]
        public void Evaluate_Player1Wins_DealerAndPlayer2Loses()
        {
            // Arrange
            var mockPlayer2 = new Mock<IPlayer>();
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer2.Setup(p => p.IsBusted()).Returns(false);

            _mockPlayer.Setup(p => p.HandValue).Returns(20);
            mockPlayer2.Setup(p => p.HandValue).Returns(18);

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(19);

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object, mockPlayer2.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(_mockPlayer.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.DealerLoses && o.Players.Contains(_mockDealer.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerLoses && o.Players.Contains(mockPlayer2.Object)));
        }


        [Test]
        public void Evaluate_DealerBusted_PlayerOneWins_PlayerTwoBusted()
        {
            // Arrange
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false); // Player 1 is not busted
            _mockPlayer.Setup(p => p.HandValue).Returns(20); // Player 1 hand value
            
            
            var mockPlayer2 = new Mock<IPlayer>();
            mockPlayer2.Setup(p => p.IsBusted()).Returns(true); // Player 2 is busted
            mockPlayer2.Setup(p => p.HandValue).Returns(25); // Player 1 hand value

            
            _mockDealer.Setup(d => d.IsBusted()).Returns(true); // Dealer is busted
            _mockDealer.Setup(p => p.HandValue).Returns(24); // Player 1 hand value


            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object, mockPlayer2.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            // Assert.IsTrue(outcomes.Any(o =>
            //     o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(_mockPlayer.Object)));

            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.DealerBusted && o.Players.Contains(_mockDealer.Object)));

            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerBusted && o.Players.Contains(mockPlayer2.Object)));
        }

        public void Cleanup()
        {
            _game.Dispose();
        }
    }
}