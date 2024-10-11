using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    public class BlackjackGame
    {
        private Deck _deck;
        private Evaluator _evaluator;
        private Presenter _presenter;
        
        public List<Player> Players { get; set; }
        
        public BlackjackGame(int playerCount)
        {
            _deck = new Deck(AssetPaths.CardsJsonPath);
            _evaluator = new Evaluator();
            _presenter = new Presenter();
            Players = CreatePlayers (playerCount);
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

        private static List<Player> CreatePlayers(int playerCount)
        {
            var players = new List<Player>();

            for (var i = 0; i < playerCount; i++)
                players.Add(new Player($"Player_{(i + 1).ToString()}"));
            
            players.Add(new Player("Dealer", true));
            return players;
        }
    }
}