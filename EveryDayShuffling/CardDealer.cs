using System;
using System.Linq;
using EveryDayShuffling.Models;

namespace EveryDayShuffling
{
    public static class CardDealer
    {
        static Random r = new Random();
        //Based on Fisher-Yates shuffle and as demonstrated by Donald Knuth in The Art of Computer Programming 2. Seminumerical algorithms.
        public static void Shuffle(ref CardDeck cd)
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
            cd.Cards = cd.Cards.OrderBy(c => c.Rank).ThenBy(c => c.Suit).ToArray();
        }
    }
}
