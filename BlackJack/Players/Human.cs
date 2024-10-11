using BlackJack.Strategies;

namespace BlackJack.Players
{
    public class Human : APlayer
    {
        public Human(string name) : base(name, new HumanStrategy())
        {
        }
    }
}