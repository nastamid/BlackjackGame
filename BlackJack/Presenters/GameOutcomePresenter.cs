using System;
using System.Collections.Generic;
using BlackJack.Data;
using BlackJack.Enums;
using BlackJack.View;

namespace BlackJack.Presenters
{
    public class GameOutcomePresenter
    {
        public void PresentOutcomes(List<OutcomeData> outcomes)
        {
            ConsoleView.Instance.DisplayGameEnded();
            
            foreach (OutcomeData outcome in outcomes)
            {
                switch (outcome.OutcomeType)
                {
                    case EOutcomeType.DealerWins:
                        ConsoleView.Instance.PromptDealerWins();
                        break;
                    case EOutcomeType.DealerLoses:
                        ConsoleView.Instance.PromptDealerLost();
                        break;
                    case EOutcomeType.DealerBusted:
                        ConsoleView.Instance.PromptDealerBusted();
                        break;
                    case EOutcomeType.PlayerWins:
                        ConsoleView.Instance.DisplayWinners(outcome.Players);
                        break;
                    case EOutcomeType.PlayerLoses:
                        ConsoleView.Instance.DisplayLosers(outcome.Players);
                        break;
                    case EOutcomeType.PlayerBusted:
                        ConsoleView.Instance.DisplayBustedPlayers(outcome.Players);
                        break;
                    case EOutcomeType.Draw:
                        ConsoleView.Instance.DisplayPlayersInDraw(outcome.Players);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ConsoleView.Instance.DisplayPlayerCardsAndValues(outcome.Players);
            }
        }
    }
}