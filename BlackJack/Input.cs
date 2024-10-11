using System;

namespace BlackJack
{
    public class Input
    {
        private static Input _instance;
        public static Input Instance => _instance ?? (_instance = new Input());

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}