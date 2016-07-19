using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EveryDayShuffling;
using System.Collections.Generic;

namespace TestShuffle
{
    [TestClass]
    public class Core
    {
        [TestMethod]
        public void SortDeck_Sorted()
        {
            CardDeck cd = new CardDeck();
            //Ensure the baseline is ordered Ascending by Rank and then by Suit.
            cd.Cards = cd.Cards.OrderBy(c => c.rank).OrderBy(c => c.suit).ToArray<Card>();
            string baselineDeck = cd.ToSmallString();

            cd.Cards = cd.Cards.Reverse().ToArray<Card>(); 
            Assert.AreNotEqual(baselineDeck, cd.ToSmallString());

            CardDealer.Sort(ref cd); //Sort should return this to sorted by ascending ranks and suits.
            Assert.AreEqual(baselineDeck, cd.ToSmallString());
        }
        /// <summary>
        /// Testing that the deck is shuffled, which is not an exact science.
        /// Will test that the shuffle function is changing the order, although technically, it could possibly 
        /// a valid shuffle to end up in the same order again.
        /// </summary>
        [TestMethod]
        public void ShuffleDeck_Shuffled()
        {
            CardDeck cd = new CardDeck();
            string startingDeck = cd.ToSmallString();

            CardDealer.Shuffle(ref cd);
            Assert.AreNotEqual(startingDeck, cd.ToSmallString());

        }
        /// <summary>
        /// Testing that the shuffle method does not produce duplicate shuffle orders.
        /// This would demonstrate that we have bias in our randomization process.
        /// </summary>
        [TestMethod]
        public void ShuffleDeck_NotBiased()
        {
            //The HashSet will only allow unique shuffles to insert.
            HashSet<string> shuffleResults = new HashSet<string>();
            CardDeck cd = new CardDeck();
            int timesToShuffle = 1000000;

            for(int i = 0; i < timesToShuffle; i++)
            {
                CardDealer.Shuffle(ref cd);
                shuffleResults.Add(cd.ToSmallString());
            }
            //If the number of shuffles is the same as the count in our HashSet, all shuffles were unique.
            Assert.IsTrue(shuffleResults.Count == timesToShuffle);
        }
        /// <summary>
        /// When CardDeck is instantiated, it should contain 52 elements.
        /// All elements should be of type Card, but that is enforced by definition.
        /// There should be 13 of each Suit and 4 Suits
        /// </summary>
        [TestMethod]
        public void CreateDeck_AreThere13PerSuit()
        {
            CardDeck cd = new CardDeck();
            
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Club).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Heart).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Diamond).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Spade).Count());
        }
        /// <summary>
        /// When CardDeck is instantiated, it should contain 52 elements.
        /// All elements should be of type Card, but that is enforced by definition.
        /// There should be 4 unique Suits
        /// </summary>
        [TestMethod]
        public void CreateDeck_AreThere4Suits()
        {
            CardDeck cd = new CardDeck();

            Assert.AreEqual(4, cd.Cards.GroupBy(c => c.suit).Count());
        }
        /// <summary>
        /// When CardDeck is instantiated, it should contain 52 elements.
        /// All elements should be of type Card, but that is enforced by definition.
        /// There should be only unique cards.
        /// </summary>
        [TestMethod]
        public void CreateDeck_AreThereOnlyUniqueCards()
        {
            CardDeck cd = new CardDeck();

            Assert.AreEqual(cd.Cards.Count(), cd.Cards.Distinct().Count());
        }
    }
}
