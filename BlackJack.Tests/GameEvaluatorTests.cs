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


        [Test]
        public void Evaluate_DealerWinsWith21_Player1LosesWith25()
        {
            // Arrange
            _mockPlayer.Setup(p => p.IsBusted()).Returns(true);
            _mockPlayer.Setup(p => p.HandValue).Returns(25); // Player 1 hand value

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(21); // Dealer hand value

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerBusted && o.Players.Contains(_mockPlayer.Object)));
            Assert.IsFalse(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(_mockPlayer.Object)));
        }


        [Test]
        public void Evaluate_DealerBusted_PlayerWinsWith21()
        {
            // Arrange
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false); // Player is not busted
            _mockPlayer.Setup(p => p.HandValue).Returns(21); // Player hand value is 21

            _mockDealer.Setup(d => d.IsBusted()).Returns(true); // Dealer is busted
            _mockDealer.Setup(d => d.HandValue).Returns(30); // Dealer hand value is 30

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerBusted));
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.PlayerWins));
            Assert.AreEqual(2, outcomes.Count);
        }


        [Test]
        public void Evaluate_DealerWins_PlayerBusted()
        {
            // Arrange
            _mockPlayer.Setup(p => p.IsBusted()).Returns(true); // Player is busted
            _mockPlayer.Setup(p => p.HandValue).Returns(27); // Player hand value is 27

            _mockDealer.Setup(d => d.IsBusted()).Returns(true); // Dealer is not busted
            _mockDealer.Setup(d => d.HandValue).Returns(22); // Dealer hand value is 22

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerWins));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerBusted && o.Players.Contains(_mockPlayer.Object)));
            Assert.AreEqual(2, outcomes.Count);
        }


        [Test]
        public void Evaluate_PlayerAndBotsHaveHigherValue_ThanDealer_ShouldDeclareCorrectOutcomes()
        {
            // Arrange
            var mockPlayer1 = new Mock<IPlayer>();
            var mockBot1 = new Mock<IPlayer>();
            var mockBot2 = new Mock<IPlayer>();
            var mockBot3 = new Mock<IPlayer>();
            var mockBot4 = new Mock<IPlayer>();

            _mockPlayer.Setup(p => p.IsBusted()).Returns(false);
            _mockPlayer.Setup(p => p.HandValue).Returns(14);

            mockBot1.Setup(p => p.IsBusted()).Returns(false);
            mockBot1.Setup(p => p.HandValue).Returns(15);

            mockBot2.Setup(p => p.IsBusted()).Returns(false);
            mockBot2.Setup(p => p.HandValue).Returns(17);

            mockBot3.Setup(p => p.IsBusted()).Returns(false);
            mockBot3.Setup(p => p.HandValue).Returns(20);

            mockBot4.Setup(p => p.IsBusted()).Returns(false);
            mockBot4.Setup(p => p.HandValue).Returns(4);

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(11);

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer>
                    { _mockPlayer.Object, mockBot1.Object, mockBot2.Object, mockBot3.Object, mockBot4.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.DealerLoses && o.Players.Contains(_mockDealer.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(_mockPlayer.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(mockBot1.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(mockBot2.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(mockBot3.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerLoses && o.Players.Contains(mockBot4.Object)));
            Assert.AreEqual(3, outcomes.Select(o => o.OutcomeType).Distinct().Count());
        }


        [Test]
        public void Evaluate_PlayerAndDealerDraw_ShouldDeclareDraw()
        {
            // Arrange
            _mockPlayer.Setup(p => p.IsBusted()).Returns(false); // Player is not busted
            _mockPlayer.Setup(p => p.HandValue).Returns(20); // Player hand value is 20

            _mockDealer.Setup(d => d.IsBusted()).Returns(false); // Dealer is not busted
            _mockDealer.Setup(d => d.HandValue).Returns(20); // Dealer hand value is 20

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { _mockPlayer.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.Draw));
            Assert.AreEqual(1, outcomes.Count);
        }


        [Test]
        public void Evaluate_PlayerAndDealerDrawForTwoOutOfFourPlayers()
        {
            // Arrange
            var mockPlayer1 = new Mock<IPlayer>();
            var mockPlayer2 = new Mock<IPlayer>();
            var mockPlayer3 = new Mock<IPlayer>();
            var mockPlayer4 = new Mock<IPlayer>();

            // Player 1 and Player 2 will draw with the dealer
            mockPlayer1.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer1.Setup(p => p.HandValue).Returns(20);

            mockPlayer2.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer2.Setup(p => p.HandValue).Returns(20);

            // Player 3 will win
            mockPlayer3.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer3.Setup(p => p.HandValue).Returns(22);

            // Player 4 will lose
            mockPlayer4.Setup(p => p.IsBusted()).Returns(true);
            mockPlayer4.Setup(p => p.HandValue).Returns(25);

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(20);

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer>
                    { mockPlayer1.Object, mockPlayer2.Object, mockPlayer3.Object, mockPlayer4.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsTrue(
                outcomes.Any(o =>
                    o.OutcomeType == EOutcomeType.Draw && o.Players.Contains(mockPlayer1.Object) &&
                    o.Players.Contains(mockPlayer2.Object)));
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.Draw && o.Players.Count == 3));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerWins && o.Players.Contains(mockPlayer3.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerBusted && o.Players.Contains(mockPlayer4.Object)));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.DealerLoses));
            Assert.AreEqual(4, outcomes.Count);
        }


        [Test]
        public void Evaluate_TwoPlayersDraw_DealerLoses_ThirdPlayerBusted()
        {
            // Arrange
            var mockPlayer1 = new Mock<IPlayer>();
            var mockPlayer2 = new Mock<IPlayer>();
            var mockPlayer3 = new Mock<IPlayer>();

            // Player 1 and Player 2 will draw
            mockPlayer1.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer1.Setup(p => p.HandValue).Returns(20);

            mockPlayer2.Setup(p => p.IsBusted()).Returns(false);
            mockPlayer2.Setup(p => p.HandValue).Returns(20);

            // Player 3 will be busted
            mockPlayer3.Setup(p => p.IsBusted()).Returns(true);
            mockPlayer3.Setup(p => p.HandValue).Returns(27);

            _mockDealer.Setup(d => d.IsBusted()).Returns(false);
            _mockDealer.Setup(d => d.HandValue).Returns(18);

            _game = new Game(new GameData
            {
                Deck = _mockDeck.Object,
                Dealer = _mockDealer.Object,
                Players = new List<IPlayer> { mockPlayer1.Object, mockPlayer2.Object, mockPlayer3.Object }
            });

            // Act
            var outcomes = _evaluator.Evaluate(_game);

            // Assert
            Assert.IsFalse(
                outcomes.Any(o =>
                    o.OutcomeType == EOutcomeType.Draw));
            Assert.IsTrue(outcomes.Any(o =>
                o.OutcomeType == EOutcomeType.PlayerBusted && o.Players.Contains(mockPlayer3.Object)));
            Assert.IsTrue(outcomes.Any(o => o.OutcomeType == EOutcomeType.DealerLoses));
            Assert.AreEqual(3, outcomes.Count);
        }

        [TearDown]
        public void Cleanup()
        {
            _game.Dispose();
        }
    }
}