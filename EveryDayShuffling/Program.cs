using System;

namespace EveryDayShuffling
{
    public enum Suit
    {
        Heart = 1,
        Club = 2,
        Diamond = 3,
        Spade = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. Coming soon options to sort and shuffle.");
            CardDeck cd = new CardDeck();
            foreach(Card card in cd.Cards)
            {
                Console.WriteLine("Card is a {0} of {1}s.", card.rank, card.suit);
            }
            Console.ReadLine();
        }
    }

    public struct Card
    {

        private int _rank;
        private Suit _suit;

        public int rank
        {
            get { return _rank; }
            set { _rank = value; }
        }
        public Suit suit
        {
            get { return _suit; }
            set { _suit = value; }
        }
    }

    public class CardDeck
    {
        private Card[] _cards;

        public Card[] Cards
        {
            get { return _cards; }
            set { _cards = new Card[52]; }
        }
        public CardDeck()
        {
            //initialize sorted Deck of Cards.
            Cards = new Card[52];
            
            int initialDeckPlacement = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                //Cards rank from 2 to 14, we are assuming Ace-High rules for now.
                for(int i = 2; i <=14; i++)
                {
                    Cards[initialDeckPlacement].rank = i;
                    Cards[initialDeckPlacement].suit = suit;
                    initialDeckPlacement++;
                }
            }
        }
    }

    public static class CardDealer
    {
        public static CardDeck Shuffle(CardDeck cd)
        {
            return cd;
        }

        public static CardDeck Sort(CardDeck cd)
        {
            return cd;
        }
    }
}
