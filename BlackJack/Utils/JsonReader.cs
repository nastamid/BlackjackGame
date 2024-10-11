using System.Collections.Generic;
using System.IO;
using BlackJack.Models;
using Newtonsoft.Json;

namespace BlackJack.Utils
{
    public static class JsonReader
    {
        public static List<Card> LoadCardsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CardWrapper>(json).Cards;
        }
    }
}