using Briscolatro.Core.Players;
using Briscolatro.Core.Cards;
using Briscolatro.Core.GameStateMachine;
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
                case TrickState.WaitingFirstCard:
                case TrickState.WaitingSecondCard:
                    Player currentPlayer = game.CurrentPlayer!;
                    Card card = currentPlayer.Hand[0];

                    Console.WriteLine(
                        $"{currentPlayer.Name} gioca {card}"
                    );

                    game.PlayCard(currentPlayer, card);
                    break;

                case TrickState.ResolvingTrick:
                case TrickState.DrawingCards:
                    game.Advance();
                    break;
            }
        }
        //OLD test purpose
        // if (game.CurrentScore.Values.Sum() != 120)
        // {
        //     throw new Exception("Invalid total score");
        // }

        // Console.WriteLine(
        //     $"Total score: {game.CurrentScore.Values.Sum()}"
        // );
        Console.WriteLine("Partita terminata");
    }
}