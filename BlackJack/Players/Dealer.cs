using BlackJack.AppSettings;
using BlackJack.Strategies;

namespace BlackJack.Players
{
    public class Dealer : APlayer
    {
        public Dealer() : base(Configurations.DefaultDealerName, new DealerStrategy()) { }
    }
}