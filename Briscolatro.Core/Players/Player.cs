using Briscolatro.Core.Cards;


namespace Briscolatro.Core.Players
{
    public class Player
    {
        public string Name {get;}
        public PlayerType Type {get;}
        private readonly List<Card> hand = new List<Card>();
        public IReadOnlyList<Card> Hand => hand;
        public int Lifes { get; private set; } = 3;

        public Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
        }
        public void LoseLife()
        {
            Lifes--;
        }
        public void AddCard(Card card)
        {
            hand.Add(card);
        }
        public bool RemoveCard(Card card)
        {
            return hand.Remove(card);
        }
    }
}   