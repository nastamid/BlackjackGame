using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Enums;
using BlackJack.Models;
using BlackJack.Players;
using BlackJack.View;

namespace BlackJack
{
    public class BlackjackGame
    {
        private readonly Deck _deck = new Deck();
        private readonly Evaluator _evaluator = new Evaluator();
        private readonly PlayerFactory _playerFactory = new PlayerFactory();

        private List<APlayer> Players { get; set; }

        public void Initialize(EGameMode gameMode, int playerCount)
        {
            Players = new List<APlayer>();
            
            switch (gameMode)
            {
                case EGameMode.SinglePlayer:
                    InitializeSinglePlayer(playerCount);
                    break;
                case EGameMode.MultiPlayer:
                    InitializeMultiplayer(playerCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null);
            }
        }

        private void InitializeSinglePlayer(int playerCount)
        {
            Players.Add(_playerFactory.CreateDealer());
            Players.Add(_playerFactory.CreateHumanPlayer());
            Players.AddRange(_playerFactory.CreateBotPlayers(--playerCount));
        }

        private void InitializeMultiplayer(int playerCount)
        {
            Players.Add(_playerFactory.CreateDealer());
            Players.AddRange(_playerFactory.CreateHumanPlayers(playerCount));
        }

        public void StartGame()
        {
            PlayGame();
            EndGame();
        }

        private void PlayGame()
        {
            _deck.Shuffle();
            var isHold = false;
            while (!isHold)
            {
                ConsoleView.Instance.Clear();
                AddCardToPlayers();
                KickOutBustedPlayers();

                if (Players.Count <= 1)
                    break;

                foreach (var player in Players)
                {
                    // if (player.IsDealer)
                    //     continue;

                    isHold = AsksForHold(player);
                    if (isHold)
                        break;
                }
            }
        }

        private void EndGame()
        {
            ConsoleView.Instance.DisplayEndGame();
            var winner = _evaluator.DetermineWinner(Players);
            ConsoleView.Instance.DisplayWinner(winner);
            Players.Remove(winner);
            ConsoleView.Instance.DisplayLeftPlayerCardsAndValue(Players);
        }

        private void KickOutBustedPlayers()
        {
            var bustedPlayers = Players.Where(player => player.IsBusted()).ToList();
            foreach (var bustedPlayer in bustedPlayers)
            {
                ConsoleView.Instance.DisplayBustedPlayer(bustedPlayer);
                Players.Remove(bustedPlayer);
            }
        }

        private void AddCardToPlayers()
        {
            foreach (var player in Players)
                player.AddCardToHand(_deck.DrawCard());
        }

        private bool AsksForHold(APlayer aPlayer)
        {
            while (true)
            {
                ConsoleView.Instance.DisplayPlayerHand(aPlayer);
                ConsoleView.Instance.DisplayQuestionForHitOrHold(aPlayer);
                var choice = Console.ReadLine()?.ToUpper();
                if (choice != "H" && choice != "X")
                    continue;
                return choice == "X";
            }
        }

        public void Dispose()
        {
            Players = null;
        }
    }
}