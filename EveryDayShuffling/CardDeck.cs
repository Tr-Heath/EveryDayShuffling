using System;
using System.Text;
using EveryDayShuffling.Models;

namespace EveryDayShuffling
{
    public class CardDeck
    {
        public Card[] Cards { get; set; }

        //initialize sorted Deck of Cards.
        public CardDeck() : this(52)
        {
        }

        public CardDeck(int size)
        {
            Cards = new Card[size];

            int deckPlacement = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    if (deckPlacement == size)
                    {
                        break;
                    }

                    Cards[deckPlacement].Rank = value;
                    Cards[deckPlacement].Suit = suit;
                    deckPlacement++;
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
}