using System.Collections.Generic;
using System.Linq;
using BlackJack.Data;
using BlackJack.Models.Deck;
using BlackJack.Models.Players;
using BlackJack.View;

namespace BlackJack.GameCore
{
    public class Game
    {
        public IDeck Deck { get; private set; }
        public IPlayer Dealer { get; private set; }
        public List<IPlayer> Players { get; private set; }

        public Game(GameData gameData)
        {
            Deck = gameData.Deck;
            Dealer = gameData.Dealer;
            Players = gameData.Players;
        }
        
        /// <summary>
        /// Game Stops In following Conditions
        /// - When All players are Busted
        /// - When Dealer is Busted
        /// - When Player Asks for Hold
        /// </summary>
        public void Run()
        {
            Deck.Shuffle();
            var isHit = true;
            while (isHit)
            {
                AddCardToPlayers();
                Dealer.AddCardToHand(Deck.DrawCard());

                if (Players.All(p=>p.IsBusted()))
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
                if (player.IsBusted())
                    continue;
                if (!player.TakeTurn(Deck))
                    return false;
            }
            return true;
        }

        private void AddCardToPlayers()
        {
            foreach (var player in Players)
                player.AddCardToHand(Deck.DrawCard());
        }

        public void Dispose()
        {
            Deck = null;
            Dealer = null;
            Players = null;
        }
    }
}