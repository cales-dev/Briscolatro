using Briscolatro.Core.Cards;
using Briscolatro.Core.Decks;
using Briscolatro.Core.Players;

namespace Briscolatro.Core.GameStateMachine;
public class Game
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
    public TrickState PlayState { get; private set; }
    public Card Briscola => briscola;
    public Player Player1 => player1;
    public Player Player2 => player2;
    
    public Player? CurrentPlayer
    {
        get
        {
            return PlayState switch
            {
                TrickState.WaitingFirstCard => firstPlayer,
                TrickState.WaitingSecondCard => secondPlayer,
                _ => null
            };
        }
    }
    public Game(Random? random = null)
    {
        this.random = random ?? new Random();
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

        briscola = deck.ShowBriscola();
        
        (firstPlayer, secondPlayer) = ChoosePlayerStart(player1,player2);
    }
    public void StartGame(int amount = 3)
    {
        if (State != GameState.Start)
            return;
        //This might be another method{
        for (int i = 0; i < amount; i++)
        {
            DrawCard(player1);
            DrawCard(player2);
        }
        
        State = GameState.Playing;
        PlayState = TrickState.WaitingFirstCard;
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
            case TrickState.WaitingFirstCard:
                if(player != firstPlayer)
                    return;

                if(!player.RemoveCard(card))
                    return;
                
                firstCard = card;
                PlayState = TrickState.WaitingSecondCard;
                break;
            case TrickState.WaitingSecondCard:
                if (player != secondPlayer)
                    return;

                if (!player.RemoveCard(card))
                    return;
                
                secondCard = card;
                PlayState = TrickState.ResolvingTrick;
                break;
            default:
                return;
        }
    }
    private void ResolveCurrentTrick()
    {
        if (PlayState != TrickState.ResolvingTrick)
            return;

        if (firstCard == null || secondCard == null)
            return;

        Player winner = ResolveTrick(firstCard,secondCard,firstPlayer,secondPlayer);

        Player loser = winner == firstPlayer ? secondPlayer : firstPlayer;

        score[winner] += firstCard.Value + secondCard.Value;

        firstPlayer = winner;
        secondPlayer = loser;

        PlayState = TrickState.DrawingCards;
    }
    private void DrawCards()
    {
        if(PlayState != TrickState.DrawingCards)
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

        PlayState = TrickState.WaitingFirstCard;
    }
    public void Advance()
    {
        if (State != GameState.Playing)
            return;

        switch (PlayState)
        {
            case TrickState.ResolvingTrick:
                ResolveCurrentTrick();
                break;

            case TrickState.DrawingCards:
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
    public int GetFinalScore(Player player)
    {
        if(State != GameState.End)
        {
            throw new InvalidOperationException( "Score is not available before the game ends.");
        }

        return score[player];
    }
}
