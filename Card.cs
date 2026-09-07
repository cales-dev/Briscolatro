namespace CardClass
{
    public class Card{
        public int Number {get;set;}
        public Suit Suit {get;set;}
        public int Value {get;set;}

        private Dictionary<int,int> CardStrength = new()
        {
           {1, 11},
           {3, 10},
           {10, 9},
           {9, 8},
           {8, 7},
           {7, 6},
           {6, 5},
           {5, 4},
           {4, 3},
           {2, 2}
        };

        public int GetCardStrenght()
        {
            return CardStrength[this.Number];
        }
    }

    public enum Suit
    {
        Denari,
        Bastoni,
        Coppe,
        Spade
    }
}