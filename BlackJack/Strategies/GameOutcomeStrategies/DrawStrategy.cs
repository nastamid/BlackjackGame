using BlackJack.Enums;
using BlackJack.GameCore;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DrawStrategy : IGameOutcomeStrategy
    {
        public EOutcomeType? Execute(Game game)
        {
            var isDraw = false;

            foreach (var player in game.Players)
                if (player.HandValue == game.Dealer.HandValue)
                {
                    //result += $"{player.Name} draws with the Dealer. ";
                    isDraw = true;
                }
                else if (player.HandValue > game.Dealer.HandValue && !player.IsBusted())
                {
                    //result += $"{player.Name} wins. ";
                }
                else if (player.HandValue < game.Dealer.HandValue && !player.IsBusted())
                {
                    //result += $"{player.Name} loses. ";
                }

            if (isDraw)
                return EOutcomeType.Draw;

            return null;
        }
    }
}