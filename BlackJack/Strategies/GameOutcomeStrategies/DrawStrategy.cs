using BlackJack.Game;

namespace BlackJack.Strategies.GameOutcomeStrategies
{
    public class DrawStrategy : IGameOutcomeStrategy
    {
        public void Execute(BlackjackGame game)
        {
            string result = "Outcome: ";
            bool isDraw = false;

            foreach (var player in game.Players)
            {
                if (player.HandValue == game.Dealer.HandValue)
                {
                    result += $"{player.Name} draws with the Dealer. ";
                    isDraw = true;
                }
                else if (player.HandValue > game.Dealer.HandValue && !player.IsBusted())
                {
                    result += $"{player.Name} wins. ";
                }
                else if (player.HandValue < game.Dealer.HandValue && !player.IsBusted())
                {
                    result += $"{player.Name} loses. ";
                }
            }

            //return isDraw ? result : null;
        }
    }
}