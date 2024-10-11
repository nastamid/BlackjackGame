namespace BlackJack
{
    public class Card
    {
        public string Suit { get; }
        public string Face { get; }
        public int Value { get; }

        public Card(string suit, string face, int value)
        {
            Suit = suit;
            Face = face;
            Value = value;
        }
    }
}