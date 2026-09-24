using Briscolatro.Core.Cards;


namespace Briscolatro.Core.Players
{
    public class Player
    {
        public string Name {get;}
        public PlayerType Type {get;}
        public List<Card> Hand { get; set; } = new List<Card>();
        public int lifes { get; set; } = 3;

        public Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
        }
        public void LoseLife()
        {
            lifes--;
        }
        public void AddCard(Card card)
        {
            Hand.Add(card);
        }
    }
}   