using Briscolatro.Core.Cards;
using Briscolatro.Core.Players;
using Briscolatro.Core.Decks;
namespace Briscolatro.Core.Game;
public class Game
{
    private Card Briscola = new Card();
    private Dictionary<Player,int> Score;
    private Random random = new Random();
    
    public void StartGame()
    {
        //Deck setup
        Deck deck = new Deck();

        //Players setup
        Player player1 = new Player("Player1", PlayerType.Human);
        Player player2 = new Player("Player2", PlayerType.Bot);

        Score = new()
        {
            {player1, 0},
            {player2, 0}
        };
        
        //This might be another method{
        for (int i = 0; i < 3; i++)
        {
            Card? card1 = deck.Draw();
            if (card1 != null)
            {
                player1.AddCard(card1);
            }

            Card? card2 = deck.Draw();
            if (card2 != null)
            {
                player2.AddCard(card2);
            }
        }

        Briscola = deck.ShowBriscola();
        Console.WriteLine($"La briscola è:  {Briscola}");
        //}

        //Setup game
        (Player firstPlayer, Player secondPlayer) = ChoosePlayerStart(player1,player2);

        while (firstPlayer.Hand.Count > 0 && secondPlayer.Hand.Count > 0)
        {
            Player winner = PlayTrick(firstPlayer,secondPlayer);
            Player loser = winner == firstPlayer ? secondPlayer : firstPlayer;
            
            Console.WriteLine(winner.Name + " Ha vinto la mano! ");

            Card? winnerCard = deck.Draw();
            Card? loserCard = deck.Draw();

            if (winnerCard != null)
                winner.AddCard(winnerCard);

            if (loserCard != null)
                loser.AddCard(loserCard);

            firstPlayer = winner;
            secondPlayer = loser;
        }
        ShowScore(player1, player2);
    }
    private (Player firstPlayer, Player secondPlayer) ChoosePlayerStart(Player player1, Player player2)
    {     
        return random.Next(2) == 0 ? (player1, player2) : (player2, player1);
    }
    private Card AskPlayerCard(Player player)
    {
        Console.WriteLine("Scegli una carta:");

        for (int j = 0; j < player.Hand.Count; j++)
        {
            Console.WriteLine($"{j + 1} - {player.Hand[j]}");
        }
        Card selectedCard =new Card();
        string? input = Console.ReadLine();
        
        if (int.TryParse(input, out int choice))    
        {
            selectedCard = player.Hand[choice - 1];
            player.RemoveCard(selectedCard);
        }

        return selectedCard;
    }
    private Card ChooseBotCard(Player bot)
    {
        int index = random.Next(0, bot.Hand.Count);
        Card selectedCard= bot.Hand[index];

        Console.WriteLine("Computer gioca: ");
        Console.WriteLine($"{index} - {bot.Hand[index]}");
        
        bot.RemoveCard(selectedCard);
        return selectedCard;
    }

    private Player ResolveTrick(Card firstCard, Card secondCard, Player firstPlayer, Player secondPlayer)
    {
        if (firstCard.Suit == Briscola.Suit && secondCard.Suit != Briscola.Suit)
        {
            return firstPlayer;
        }

        if (secondCard.Suit == Briscola.Suit && firstCard.Suit != Briscola.Suit)
        {
            return secondPlayer;
        }

        if (firstCard.Suit == secondCard.Suit)
        {
            return firstCard.GetCardStrength() > secondCard.GetCardStrength() ? firstPlayer : secondPlayer;
        }
        
        return firstPlayer;
    }
    private Card PickCard(Player player)
    {
        return player.Type == PlayerType.Bot ? ChooseBotCard(player) : AskPlayerCard(player);
    }
    private Player PlayTrick(Player player1, Player player2)
    {
        Card firstCard = PickCard(player1);
        Card secondCard = PickCard(player2);

        Player winner = ResolveTrick(firstCard, secondCard, player1, player2);

        Score[winner] += firstCard.Value + secondCard.Value;
        
        return winner;
    }  
    private void ShowScore(Player player1, Player player2)
    {
        Console.WriteLine(
            $"{player1.Name}: {Score[player1]} | " +
            $"{player2.Name}: {Score[player2]}"
        );
    }
    
}
