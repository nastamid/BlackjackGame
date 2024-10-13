using System.Collections.Generic;
using BlackJack.Factories;
using BlackJack.GameCore;
using BlackJack.Models;
using BlackJack.Presenters;

namespace BlackJack.Data
{
    public class GameCycleData
    {
        public List<Card> Cards { get; set; }
        public GameEvaluator GameEvaluator { get; set; }
        public PlayerFactory PlayerFactory { get; set; }
        public GameOutcomePresenter OutcomePresenter { get; set; }
    }
}