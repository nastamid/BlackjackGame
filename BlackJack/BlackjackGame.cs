﻿using System;
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
            _deck.Shuffle();

            bool isHold = false;
            while (Players.Count > 1 && !isHold)
            {
                AddCardToPlayers();
                foreach (var player in Players)
                {
                    if (!player.IsDealer)
                        _presenter.DisplayPlayerHand(player);

                    if (player.IsBusted())
                    {
                        Console.WriteLine($"{player.Name} is busted");
                        _presenter.DisplayPlayerHand(player);
                        Players.Remove(player);
                    }
                    
                    if (!player.IsDealer)
                    {
                        isHold = AsksForHold(player);
                        if (isHold)
                            break;
                    }
                }
            }
            
            DisplayLeftPlayerCardsAndValue();
            Player winner = _evaluator.DetermineWinner(Players);
            DisplayWinner(winner);
        }

        private void AddCardToPlayers()
        {
            foreach (var player in Players)
            {
                player.AddCard(_deck.DrawCard());
            }
        }


        private void DisplayWinner(Player winner)
        {
            Console.WriteLine("{0} is a Winner", winner.Name);
            _presenter.DisplayPlayerHand(winner);
        }

        private void DisplayLeftPlayerCardsAndValue()
        {
            Console.WriteLine("===== GAME OVER =====");
            foreach (var player in Players)
                _presenter.DisplayPlayerHand(player);
        }

        private bool AsksForHold(Player player)
        {
            Console.WriteLine($"{player.Name} - Do you want to (H)it or (X)HOLD?");
            var choice = Console.ReadLine()?.ToUpper();
            
            if (choice == null || (choice != "H" && choice != "X"))
            {
                AsksForHold(player);
                return false;
            }
            
            return choice.Equals("X");
        }
        

        private List<Player> CreatePlayers(int playerCount)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < playerCount; i++)
                players.Add(new Player($"Player_{(i + 1).ToString()}"));
            
            players.Add(new Player("Dealer", true));
            return players;
        }
    }
}