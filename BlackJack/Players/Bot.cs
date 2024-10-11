using BlackJack.Strategies;

namespace BlackJack.Players
{
    public class Bot : APlayer
    {
        public Bot(string name) : base(name, new AIStrategy()) { }
    }
}