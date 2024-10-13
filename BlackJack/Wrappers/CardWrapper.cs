using System.Collections.Generic;
using BlackJack.Models;
using Newtonsoft.Json;

namespace BlackJack.Wrappers
{
    public class CardWrapper
    {
        [JsonProperty("Card")]    
        public List<Card> Cards { get; set; }
    }
}