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

        public Player? CurrentPlayer
        {
            get
            {
                return PlayState switch
                {
                    PlayState.WaitingFirstCard => firstPlayer,
                    PlayState.WaitingSecondCard => secondPlayer,
                    _ => null
                };
            }
        }
        public void StartGame()
        {
            //Deck setup
            deck = new Deck();

            //Players setup
            player1 = new Player("Player1", PlayerType.Human);
            player2 = new Player("Player2", PlayerType.Bot);

            score = new()
            {
                {player1, 0},
                {player2, 0}
            };
            
            //This might be another method{
            for (int i = 0; i < 3; i++)
            {
                DrawCard(player1);
                DrawCard(player2);
            }

            briscola = deck.ShowBriscola();
            
            (firstPlayer, secondPlayer) = ChoosePlayerStart(player1,player2);
            State = GameState.Playing;
            PlayState = PlayState.WaitingFirstCard;
        }
        private (Player firstPlayer, Player secondPlayer) ChoosePlayerStart(Player player1, Player player2)
        {     
            return random.Next(2) == 0 ? (player1, player2) : (player2, player1);
        }
        private void DrawCard(Player player)
        {
            Card? card = deck.Draw();
            if (card != null)
            {
                player.AddCard(card);
            }
        }
        public void PlayCard(Player player, Card card)
        {
            if(State != GameState.Playing)
                return;

            switch (PlayState)
            {
                case PlayState.WaitingFirstCard:
                    if(player != firstPlayer)
                        return;

                    if(!player.Hand.Remove(card))
                        return;
                    
                    firstCard = card;
                    PlayState = PlayState.WaitingSecondCard;
                    break;
                case PlayState.WaitingSecondCard:
                    if (player != secondPlayer)
                        return;

                    if (!player.Hand.Remove(card))
                        return;
                    
                    secondCard = card;
                    PlayState = PlayState.ResolvingTrick;
                    break;
                default:
                    return;
            }
        }
        private void ResolveCurrentTrick()
        {
            if (PlayState != PlayState.ResolvingTrick)
                return;

            if (firstCard == null || secondCard == null)
                return;

            Player winner = ResolveTrick(firstCard,secondCard,firstPlayer,secondPlayer);

            Player loser = winner == firstPlayer ? secondPlayer : firstPlayer;

            score[winner] += firstCard.Value + secondCard.Value;

            firstPlayer = winner;
            secondPlayer = loser;

            PlayState = PlayState.DrawingCards;
        }
        private void DrawCards()
        {
            if(PlayState != PlayState.DrawingCards)
                return;

            Card? winnerCard = deck.Draw();
            Card? loserCard = deck.Draw();

            if (winnerCard != null)
                firstPlayer.AddCard(winnerCard);

            if (loserCard != null)
                secondPlayer.AddCard(loserCard);

            firstCard = null;
            secondCard = null;

            if (firstPlayer.Hand.Count == 0 && secondPlayer.Hand.Count == 0)
            {
                State = GameState.End;
                return;
            }

            PlayState = PlayState.WaitingFirstCard;
        }
        public void Advance()
        {
            if (State != GameState.Playing)
                return;

            switch (PlayState)
            {
                case PlayState.ResolvingTrick:
                    ResolveCurrentTrick();
                    break;

                case PlayState.DrawingCards:
                    DrawCards();
                    break;
            }
        }
        private Player ResolveTrick(Card firstCard, Card secondCard, Player firstPlayer, Player secondPlayer)
        {
            if (firstCard.Suit == briscola.Suit && secondCard.Suit != briscola.Suit)
            {
                return firstPlayer;
            }

            if (secondCard.Suit == briscola.Suit && firstCard.Suit != briscola.Suit)
            {
                return secondPlayer;
            }

            if (firstCard.Suit == secondCard.Suit)
            {
                return firstCard.GetCardStrength() > secondCard.GetCardStrength() ? firstPlayer : secondPlayer;
            }
            
            return firstPlayer;
        }
    }
}
