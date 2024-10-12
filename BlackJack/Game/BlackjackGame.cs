using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.AppSettings;
using BlackJack.Enums;
using BlackJack.Factories;
using BlackJack.Models;
using BlackJack.Players;
using BlackJack.Utils;
using BlackJack.View;

namespace BlackJack
{
    public class BlackjackGame
    {
        private readonly Evaluator _evaluator = new Evaluator();
        private readonly PlayerFactory _playerFactory = new PlayerFactory();
        private readonly IJsonReader jsonReader = new JsonReader();
        
        private Deck _deck;
        private List<APlayer> _players;

        public void Initialize(EGameMode gameMode, int playerCount)
        {
            _players = new List<APlayer>();
            _deck = new Deck(jsonReader.LoadCardsFromJson(Configurations.CardsJsonPath));

            switch (gameMode)
            {
                case EGameMode.SinglePlayer:
                    _players.Add(_playerFactory.CreateHumanPlayer());
                    _players.AddRange(_playerFactory.CreateBotPlayers(--playerCount));
                    break;
                case EGameMode.MultiPlayer:
                    _players.AddRange(_playerFactory.CreateHumanPlayers(playerCount));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null);
            }

            _players.Add(_playerFactory.CreateDealer());
        }

        public void StartGame()
        {
            PlayGame();
            EndGame();
        }

        private void PlayGame()
        {
            _deck.Shuffle();
            var isHit = true;
            while (isHit)
            {
                AddCardToPlayers();
                KickOutBustedPlayers();

                if (_players.Count <= 1)
                    break;

                foreach (var player in _players)
                {
                    isHit = player.TakeTurn(_deck);
                    if (!isHit)
                        break;
                }
            }
        }

        private void EndGame()
        {
            ConsoleView.Instance.DisplayEndGame();
            var winner = _evaluator.DetermineWinner(_players);
            ConsoleView.Instance.DisplayWinner(winner);
            _players.Remove(winner);
            ConsoleView.Instance.DisplayLeftPlayerCardsAndValue(_players);
        }

        private void KickOutBustedPlayers()
        {
            var bustedPlayers = _players.Where(player => player.IsBusted()).ToList();
            foreach (var bustedPlayer in bustedPlayers)
            {
                ConsoleView.Instance.DisplayBustedPlayer(bustedPlayer);
                _players.Remove(bustedPlayer);
            }
        }

        private void AddCardToPlayers()
        {
            foreach (var player in _players)
                player.AddCardToHand(_deck.DrawCard());
        }

        public void Dispose()
        {
            _players = null;
            _deck = null;
        }
    }
}