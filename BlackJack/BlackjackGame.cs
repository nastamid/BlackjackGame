using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    public class BlackjackGame
    {
        private readonly Deck _deck = new Deck(AssetPaths.CardsJsonPath);
        private readonly Evaluator _evaluator = new Evaluator();
        private readonly Presenter _presenter = new Presenter();
        private readonly PlayerFactory _playerFactory = new PlayerFactory();
        
        private List<Player> Players { get; set; }

        public void Initialize(int playerCount)
        {
            Players = _playerFactory.CreatePlayers (playerCount);
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
                _presenter.Clear();
                AddCardToPlayers();
                KickOutBustedPlayers();
                
                if (Players.Count <= 1)
                    break;
                
                foreach (var player in Players)
                {
                    if (player.IsDealer)
                        continue;
                    
                    isHold = AsksForHold(player);
                    if (isHold)
                        break;
                }
            }
        }
        
        private void EndGame()
        {
            _presenter.DisplayEndGame();
            var winner = _evaluator.DetermineWinner(Players);
            _presenter.DisplayWinner(winner);
            Players.Remove(winner);
            _presenter.DisplayLeftPlayerCardsAndValue(Players);
        }

        private void KickOutBustedPlayers()
        {
            for (var index = 0; index < Players.ToList().Count; index++)
            {
                var player = Players.ToList()[index];
                if (player.IsBusted())
                {
                    _presenter.DisplayBustedPlayer(player);
                    Players.Remove(player);
                }
            }
        }

        private void AddCardToPlayers()
        {
            foreach (var player in Players)
                player.AddCard(_deck.DrawCard());
        }

        private bool AsksForHold(Player player)
        {
            while (true)
            {
                _presenter.DisplayPlayerHand(player);
                _presenter.DisplayQuestionForHitOrHold(player);
                var choice = Console.ReadLine()?.ToUpper();

                if (choice == null || (choice != "H" && choice != "X")) 
                    continue;
                return choice.Equals("X");
            }
        }
    }
}