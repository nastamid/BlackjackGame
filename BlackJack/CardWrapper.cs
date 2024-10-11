using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlackJack
{
    public class CardWrapper
    {
        [JsonProperty("Card")]    
        public List<Card> Cards { get; set; }
    }
}