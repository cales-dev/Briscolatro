namespace CardClass
{
    public class Card{
        public int Number {get;set;}
        public Suit Suit {get;set;}
        public int Value {get;set;}

        private static readonly Dictionary<int,int> CardStrength = new()
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

        public int GetCardStrength()
        {
            return CardStrength[this.Number];
        }

        public override string ToString()
        {
            string name = Number switch
            {
                1 => "Asso (1)",
                8 => "Fante (8)",
                9 => "Cavallo (9)",
                10 => "Re (10)",
                _ => Number.ToString()
            };

            return $"{name} di {Suit}";
        }
    }
}