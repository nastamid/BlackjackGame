using System.Collections.Generic;
using BlackJack.Enums;
using BlackJack.GameCore;
using BlackJack.Models;
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
            _mockDeck = new Mock<IDeck>(null);
            _mockDealer = new Mock<IPlayer>();
            _mockPlayer = new Mock<IPlayer>(null);
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
        public void RunGame_WithOneHumanPlayer_HandValue20_DealerHandValue12_ShouldTerminateGame()
        {
            // Setup player with hand value 20
            var playerHand = new List<Card>
            {
                new Card("Spades", "10", 10),
                new Card("Hearts", "10", 10)
            };

            _mockPlayer.Setup(p => p.Hand).Returns(playerHand);
            _mockPlayer.Setup(p => p.TakeTurn(It.IsAny<Deck>())).Returns(false); // Player Holds

            // Setup dealer with hand value 12
            var dealerHand = new List<Card>
            {
                new Card("Diamonds", "10", 10),
                new Card("Clubs", "2", 2)
            };
            _mockDealer.Setup(p => p.Hand).Returns(dealerHand);

            var cardQueue = new Queue<Card>();
            cardQueue.Enqueue(new Card("Spades", "5", 5));
            cardQueue.Enqueue(new Card("Hearts", "10", 10));
            cardQueue.Enqueue(new Card("Diamonds", "A", 11));

            _mockDeck.Setup(d => d.DrawCard()).Returns(cardQueue.Dequeue);

            // Run the game
            _game.Run();
            var outcome = _evaluator.Evaluate(_game);
            Assert.IsTrue(outcome == EOutcomeType.PlayerWins);
        }

        public void Cleanup()
        {
            _game.Dispose();
        }
    }
}