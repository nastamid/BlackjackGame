﻿using BlackJack.Strategies.PlayerStrategies;

namespace BlackJack.Models.Players
{
    public class Human : BasePlayer
    {
        public Human(string name) : base(name, new HumanStrategy())
        {
        }
    }
}