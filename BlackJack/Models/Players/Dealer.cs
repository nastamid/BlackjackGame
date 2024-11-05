using BlackJack.AppSettings;
using BlackJack.Strategies.PlayerStrategies;

namespace BlackJack.Models.Players
{
    public class Dealer : BasePlayer
    {
        public Dealer() : base(Configurations.DefaultDealerName, new DealerStrategy())
        {
        }
    }
}