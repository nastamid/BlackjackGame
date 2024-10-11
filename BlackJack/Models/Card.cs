using Newtonsoft.Json;

namespace BlackJack.Models
{
    public class Card
    {
        [JsonProperty ("suit")]
        public string Suit { get; private set; }
        
        [JsonProperty ("face")]
        public string Face { get; private set; }
        
        [JsonProperty ("value")]
        public int Value { get; private set; }

        public Card(string suit, string face, int value)
        {
            Suit = suit;
            Face = face;
            Value = value;
        }
    }
}