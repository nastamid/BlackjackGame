using System.Collections.Generic;
using System.Linq;
using BlackJack.Models.Players;

namespace BlackJack.Extensions
{
    public static class PlayersExtensions
    {
        public static List<IPlayer> GetBustedPlayers(this List<IPlayer> players)
        {
            return players.Where(p => p.IsBusted()).ToList();
        }

        public static List<IPlayer> GetNonBustedPlayers(this List<IPlayer> players)
        {
            return players.Where(p => !p.IsBusted()).ToList();
        }

        public static bool AreAllBusted(this List<IPlayer> players)
        {
            return players.All(p => p.IsBusted());
        }

        public static bool IsAnyBusted(this List<IPlayer> players)
        {
            return players.Any(p => p.IsBusted());
        }
    }
}