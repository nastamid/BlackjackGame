namespace BlackJack
{
    public class BlackjackGame
    {
        private Deck deck;
        
        public BlackjackGame()
        {
            deck = new Deck(AssetPaths.CardsJsonPath);
            
        }

        public void StartGame()
        {
            
        }
    }
}