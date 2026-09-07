using CardClass;
using DeckClass;

namespace PlayerClass
{
    public class Player
    {
        
        private string Name="";
        public List<Card> Hand { get; set; } = new List<Card>();
        public int lifes { get; set; } = 3;
          
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
    }
}   