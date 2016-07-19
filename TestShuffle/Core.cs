using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EveryDayShuffling;

namespace TestShuffle
{
    [TestClass]
    public class Core
    {
        [TestMethod]
        public void SortDeck_Sorted()
        {
            CardDeck baseline = new CardDeck();
            CardDeck cd = new CardDeck();
            //change the ordering of a deck of cards
            cd.Cards = cd.Cards.OrderByDescending(c => c.rank).OrderByDescending(c => c.suit).ToArray<Card>();

            //Sort should return this to sorted by ascending ranks and suits.
            CardDealer.Sort(ref cd);
            CollectionAssert.AreEqual(baseline.Cards, cd.Cards);
        }
        [TestMethod]
        public void ShuffleDeck_Shuffled()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void ShuffleDeck_NotBiased()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CreateDeck_Valid52CardDeck()
        {
            //When CardDeck is instantiated, it should contain 52 elements.
            //All elements should be of type Card, but that is enforced by definition.
            //There should be 13 of each Suit and 4 Suits
            CardDeck cd = new CardDeck();
            Assert.AreEqual(52, cd.Cards.Count());
            
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Club).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Heart).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Diamond).Count());
            Assert.AreEqual(13, cd.Cards.Where(c => c.suit == Suit.Spade).Count());
            Assert.AreEqual(4, cd.Cards.GroupBy(c => c.suit).Count());
        }
    }
}
