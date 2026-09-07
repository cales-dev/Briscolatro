using CardClass;
using PlayerClass;
using DeckClass;

namespace GameClass
{
    public class Game
    {
        Card briscola = new Card();
        Dictionary<Player,int> score;
        Random random = new Random();
        private Card AskPlayerCard(Player player)
        {
            Console.WriteLine("Scegli una carta:");

            for (int j = 0; j < player.Hand.Count; j++)
            {
                Console.WriteLine($"{j + 1} - {player.Hand[j].Number}  - {player.Hand[j].Suit}");
            }
            Card selectedCard =new Card();
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out int choice))    
            {
                selectedCard = player.Hand[choice - 1];
                player.Hand.RemoveAt(choice - 1);
            }

            return selectedCard;
        }
        private Card ChooseBotCard(Player bot)
        {
            int index = random.Next(0, bot.Hand.Count);
            Card selectedCard= bot.Hand[index];

            Console.WriteLine("Computer gioca: ");
            Console.WriteLine($"{index} - {bot.Hand[index].Number}  - {bot.Hand[index].Suit}");
            
            bot.Hand.RemoveAt(index);
            return selectedCard;
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
                return firstCard.GetCardStrenght() > secondCard.GetCardStrenght() ? firstPlayer : secondPlayer;
            }

            return firstPlayer;
        }
        
        private void PlayGame(Player player1, Player player2, Deck deck)
        {
            for (int i = 0; i < 3; i++)
            {
                Card playerCard = AskPlayerCard(player1);
                Card botCard = ChooseBotCard(player2);

                Player result = ResolveTrick(playerCard, botCard, player1, player2);
                score[result]++;
                
                Console.WriteLine(result.GetPlayerName() + " Ha vinto la mano! ");
            }
        }  
        public void StartGame()
        {
            Deck deck = new Deck();
            deck.GenerateDeck();
            deck.ShuffleDeck();

            Player player1 = new Player();
            Player player2 = new Player();
            player1.SetPlayerName("Player1");
            player2.SetPlayerName("Computer");

            score = new()
            {
                {player1, 0},
                {player2, 0}
            };

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

            briscola = deck.DrawBriscola();
            Console.WriteLine("La briscola è: " + briscola.Number +" - " + briscola.Suit);
            PlayGame(player1,player2,deck);
        }
    }
}