using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EveryDayShuffling;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TestShuffle
{
    [TestClass]
    public class Core
    {
        /// <summary>
        /// Test that the Sort function brings an array of cards back to Ascending by rank and suit.
        /// </summary>
        [TestMethod]
        public void SortDeck_Sorted()
        {
            CardDeck cd = new CardDeck();
            cd.Cards = cd.Cards.OrderBy(c => c.rank).OrderBy(c => c.suit).ToArray<Card>();
            string baselineDeck = cd.ToSmallString();

            cd.Cards = cd.Cards.Reverse().ToArray<Card>(); 
            Assert.AreNotEqual(baselineDeck, cd.ToSmallString());

            CardDealer.Sort(ref cd); //Sort should return this to sorted by ascending ranks and suits.
            Assert.AreEqual(baselineDeck, cd.ToSmallString());
        }

        /// <summary>
        /// Testing that the deck is changed by shuffle, which is not an exact science.
        /// Will test that the shuffle function is changing the order, although technically, it is possible
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
        /// Testing that the deck is changed by shuffle, but only the order
        /// All cards should still make a valid 52 card, 4 suit deck.
        /// </summary>
        [TestMethod]
        public void ShuffleDeck_ShuffledAndStillValid()
        {
            CardDeck cd = new CardDeck();
            string startingDeck = cd.ToSmallString();

            CardDealer.Shuffle(ref cd);
            Assert.AreEqual(52, cd.Cards.Count());
            Assert.AreEqual(cd.Cards.Count(), cd.Cards.Distinct().Count());
            Assert.AreEqual(4, cd.Cards.GroupBy(c => c.suit).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Club).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Heart).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Diamond).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Spade).Count());
        }

        /// <summary>
        /// Testing that the shuffle method does not produce duplicate shuffle orders.
        /// Over a large number of shuffles this can help prove randomness as with a 52 deck of cards 
        /// there should never be the same deck order twice, as there are more combinations than atoms in the known universe.
        /// </summary>
        [TestMethod]
        public void ShuffleDeck_AlwaysUniqueShuffle()
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
        /// If we reduce the deck size, we can see if all expected combinations are seen with the same(nearly) frequency.
        /// </summary>
        [TestMethod]
        public void ShuffleDeck_BiasCheck()
        {
            ConcurrentDictionary<string, int> tallyOfCombinations = new ConcurrentDictionary<string, int>();
            int deckSize = 6;
            int timesToShuffle = 1000000;
            int numberOfCombinations = Helper.Factorial(deckSize);

            string currentCombination = "";

            //Initialize a deck with a testable sub-set of cards.
            CardDeck cd = new CardDeck(deckSize);

            for (int i = 0; i < timesToShuffle; i++)
            {
                CardDealer.Shuffle(ref cd);
                currentCombination = cd.ToSmallString();

                tallyOfCombinations.AddOrUpdate(currentCombination, 1, (id, count) => count + 1);
            }
            
            //The tally for each combination should be about equal if our algorithm is fair.
            foreach(KeyValuePair<string, int> entry in tallyOfCombinations)
            {
                Assert.IsTrue(Helper.IsCloseToTallyTarget(timesToShuffle, numberOfCombinations, entry.Value));
            }
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
            Assert.AreEqual(52, cd.Cards.Count());
            Assert.AreEqual(cd.Cards.Count(), cd.Cards.Distinct().Count());
        }
    }

    public static class Helper
    {
        public static int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }

        public static bool IsCloseToTallyTarget(int timesToShuffle, int numberOfCombinations, int actualTallyAmount)
        {
            int target = timesToShuffle / (numberOfCombinations <= 0 ? 1 : numberOfCombinations);
            //This gets tricky, we should expect more variance the larger numberOfCombinations is. What defines too much?
            decimal variance = target * (numberOfCombinations / (decimal)500);
            return actualTallyAmount >= (target - variance) && actualTallyAmount <= (target + variance);
        }
    }
}
