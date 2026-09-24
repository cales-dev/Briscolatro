using Briscolatro.Core.Cards;

namespace Briscolatro.Core.Decks;
public class Deck
{
    private int nextCardIndex = 0;
    private readonly Card[] deck = new Card[40];
    private readonly Dictionary<int, int> cardPoints = new()
    {
        { 1, 11 },
        { 3, 10 },
        { 8, 2 },
        { 9, 3 },
        { 10, 4 }
    };
    public Deck()
    {
        GenerateDeck();
        ShuffleDeck();
    }
    public void GenerateDeck()
    {
        int index = 0;
        nextCardIndex = 0;

        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            for (int number = 1; number <= 10; number++)
            {
                Card card = new Card();

                card.Number = number;
                card.Suit = suit;
                card.Value = cardPoints.TryGetValue(number, out int value) ? value : 0;
                deck[index] = card;
                index++;
            }
        }
    }

    public void ShuffleDeck()
    {
        Random random = new Random();

        for (int i = deck.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    public Card? Draw()
    {
        return nextCardIndex < deck.Length ? deck[nextCardIndex++] : null;
    }

    public Card ShowBriscola()
    {
        //Return the last card
        return deck[^1];
    }
}