using CardClass;
using DeckClass;

namespace PlayerClass
{
    public enum PlayerType
    {
        Human,
        Bot
    }
    public class Player
    {
        private string Name="";
        private PlayerType Type;
        public List<Card> Hand { get; set; } = new List<Card>();
        public int lifes { get; set; } = 3;

        public Player(string name, PlayerType type)
        {
            this.SetPlayerName(name);
            this.SetPlayerType(type);
        }
        public void LoseLife()
        {
            lifes--;
        }
        public void AddCard(Card card)
        {
            Hand.Add(card);
        }
        public int GetPlayerHand()
        {
            return Hand.ToArray().Length;   
        }
        public void SetPlayerName(string name)
        {
            this.Name=name;
        }
        public string GetPlayerName()
        {
            return this.Name;
        }
        public void SetPlayerType(PlayerType type)
        {
            this.Type=type;
        }
        public PlayerType GetPlayerType()
        {
            return this.Type;
        }
    }
}   