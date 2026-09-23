using CardClass;
using GameStateClass;
using PlayerClass;
internal class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();

        game.StartGame();

        while (game.State != GameState.End)
        {
            Console.WriteLine($"State: {game.State} - PlayState: {game.PlayState}");

            switch (game.PlayState)
            {
                case PlayState.WaitingFirstCard:
                case PlayState.WaitingSecondCard:
                    Player currentPlayer = game.CurrentPlayer!;
                    Card card = currentPlayer.Hand[0];

                    Console.WriteLine(
                        $"{currentPlayer.GetPlayerName()} gioca {card}"
                    );

                    game.PlayCard(currentPlayer, card);
                    break;

                case PlayState.ResolvingTrick:
                case PlayState.DrawingCards:
                    game.Advance();
                    break;
            }
        }

        Console.WriteLine("Partita terminata");
    }
}