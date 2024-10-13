using BlackJack.Strategies;
using BlackJack.Strategies.PlayerStrategies;

namespace BlackJack.Models.Players
{
    public class Bot : BasePlayer
    {
        public Bot(string name) : base(name, new AIStrategy())
        {
        }
    }
}