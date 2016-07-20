using System;
using System.Linq;
using System.Text;

namespace EveryDayShuffling
{
    public enum Suit
    {
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4
    }

    public enum CardValue
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 1
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. Coming soon options to sort and shuffle.");
            CardDeck cd = new CardDeck();
            Console.WriteLine(cd.ToString());
            Console.ReadKey();
        }
    }
    
    public struct Card
    {

        private CardValue _rank;
        private Suit _suit;

        public CardValue rank
        {
            get { return _rank; }
            set { _rank = value; }
        }
        public Suit suit
        {
            get { return _suit; }
            set { _suit = value; }
        }

        public override string ToString()
        {
            return String.Format("{0} of {1}s.", rank, suit);
        }

        public string ToSmallString()
        {
            return String.Format("{0}{1}", (int)rank, (int)suit);
        }
    }

    public class CardDeck
    {
        private Card[] _cards;

        public Card[] Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }
        public CardDeck(): this(52)
        {
            
        }
        public CardDeck(int size)
        {
            //int size = 52;
            //initialize sorted Deck of Cards.
            Cards = new Card[size];
            
            int deckPlacement = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    if (deckPlacement == size)
                    {
                        break;
                    }
                    else
                    {
                        Cards[deckPlacement].rank = value;
                        Cards[deckPlacement].suit = suit;
                        deckPlacement++;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card card in Cards)
            {
                sb.AppendLine(card.ToString());
            }
            return sb.ToString();
        }

        public string ToSmallString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card card in Cards)
            {
                sb.Append(card.ToSmallString());
            }
            return sb.ToString();
        }
    }

    //Define a series of Card manipulation methods
    public static class CardDealer
    {
        static Random r = new Random();
        //Based on Fisher-Yates shuffle and as demonstrated by Donald Knuth in The Art of Computer Programming 2. Seminumerical algorithms.
        public static void Shuffle( ref CardDeck cd)
        {
            for (int n = cd.Cards.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);

                Card temp = cd.Cards[k];
                cd.Cards[k] = cd.Cards[n];
                cd.Cards[n] = temp;
            }
        }

        public static void Sort(ref CardDeck cd)
        {
            cd.Cards = cd.Cards.OrderBy(c => c.rank).OrderBy(c => c.suit).ToArray<Card>();
        }

    }
}
