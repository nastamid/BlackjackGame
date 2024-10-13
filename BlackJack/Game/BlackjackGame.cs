using System.Collections.Generic;
using System.Linq;
using BlackJack.Models;
using BlackJack.Players;
using BlackJack.View;

namespace BlackJack.Game
{
    public class BlackjackGame
    {
        public Deck Deck { get; private set; }
        public Dealer Dealer { get; private set; }
        public List<APlayer> Players { get; private set; }

        public BlackjackGame(GameData gameData)
        {
            Deck = gameData.Deck;
            Dealer = gameData.Dealer;
            Players = gameData.Players;
        }

        public void Run()
        {
            Deck.Shuffle();
            var isHit = true;
            while (isHit)
            {
                AddCardToPlayers();
                Dealer.AddCardToHand(Deck.DrawCard());
                
                KickOutBustedPlayers();
                
                if (Players.Count == 0)
                    break;
                
                if (Dealer.IsBusted())
                    break;

                isHit = PlayersTakeTurn();
                
                if (!isHit)
                    break;
                
                Dealer.TakeTurn(Deck);
            }
        }

        private bool PlayersTakeTurn()
        {
            foreach (var player in Players)
            {
                if (!player.TakeTurn(Deck)) 
                    return false;
            }
            return true;
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
                player.AddCardToHand(Deck.DrawCard());
        }

        public void Dispose()
        {
            Dealer = null;
            Players = null;
            Deck = null;
        }
    }
}