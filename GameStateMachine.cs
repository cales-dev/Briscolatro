using CardClass;
using PlayerClass;
using DeckClass;

namespace GameStateClass
{    public class Game
    {
        private Deck deck;
        private Player player1;
        private Player player2;
        private Player firstPlayer;
        private Player secondPlayer;
        private Card? firstCard;
        private Card? secondCard;
        private Card briscola;
        private Dictionary<Player, int> score;
        private Random random = new Random();
        public GameState State { get; private set; } = GameState.Start;
        public PlayState PlayState { get; private set; }
        public void StartGame()
        {
            //Deck setup
            this.deck = new Deck();

            //Players setup
            this.player1 = new Player("Player1", PlayerType.Human);
            this.player2 = new Player("Player2", PlayerType.Bot);

            score = new()
            {
                {player1, 0},
                {player2, 0}
            };
            
            //This might be another method{
            for (int i = 0; i < 3; i++)
            {
                Card? card1 = this.deck.Draw();
                if (card1 != null)
                {
                    player1.AddCard(card1);
                }

                Card? card2 = this.deck.Draw();
                if (card2 != null)
                {
                    player2.AddCard(card2);
                }
            }

            this.briscola = this.deck.ShowBriscola();
            Console.WriteLine($"La briscola è:  {this.briscola}");
            
            (this.firstPlayer, this.secondPlayer) = ChoosePlayerStart(player1,player2);
            this.State = GameState.Playing;
            this.PlayState = PlayState.WaitingFirstCard;
        }
        private (Player firstPlayer, Player secondPlayer) ChoosePlayerStart(Player player1, Player player2)
        {     
            return random.Next(2) == 0 ? (player1, player2) : (player2, player1);
        }
        private void DrawPlayerCards(Player player, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Card? card = this.deck.Draw();
                if (card != null)
                {
                    player.AddCard(card);
                }
            }
        }
        public void PlayCard(Player player, Card card)
        {
            if(this.State != GameState.Playing)
                return;

            switch (this.PlayState)
            {
                case PlayState.WaitingFirstCard:
                    if(player != this.firstPlayer)
                        return;

                    if(!player.Hand.Remove(card))
                        return;
                    
                    this.firstCard = card;
                    this.PlayState = PlayState.WaitingSecondCard;
                    break;
                case PlayState.WaitingSecondCard:
                    if (player != this.secondPlayer)
                        return;

                    if (!player.Hand.Remove(card))
                        return;
                    
                    this.secondCard = card;
                    this.PlayState = PlayState.ResolvingTrick;
                    break;
                default:
                    return;
            }
        }
    }
}
