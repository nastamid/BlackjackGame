using System.Collections.Generic;
using System.IO;
using BlackJack.Models;
using BlackJack.Wrappers;
using Newtonsoft.Json;

namespace BlackJack.Utils
{
    public interface IJsonReader
    {
        List<Card> LoadCardsFromJson(string filePath);
    }

    public class JsonReader : IJsonReader
    {
        public List<Card> LoadCardsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CardWrapper>(json).Cards;
        }
    }
}