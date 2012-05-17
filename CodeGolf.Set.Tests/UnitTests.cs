using System;
using System.Linq;
using CodeGolf.Set.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGolf.Set.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CanBuildDeck()
        {
            var deck = new Deck();
            Assert.AreEqual(deck.Cards.Count(), 81);
        }

        [TestMethod]
        public void CanBuildShuffledDeck()
        {
            var deck = new Deck();
            String x = String.Join<Card>(",", deck.Cards);

            var shuffledDeck = new ShuffledDeck();
            String y = String.Join<Card>(",", shuffledDeck.Cards);

            var deck2 = new Deck();
            String z = String.Join<Card>(",", deck2.Cards);

            Assert.AreNotEqual(x, y);
            Assert.AreEqual(x, z);
        }

        [TestMethod]
        public void CanShuffleDeck()
        {
            var deck = new Deck();
            String newDeck = String.Join<Card>(",", deck.Cards);

            deck.Shuffle();
            String shuffledDeck = String.Join<Card>(",", deck.Cards);

            Assert.AreNotEqual(newDeck, shuffledDeck);
        }

        [TestMethod]
        public void CanGetCardsFromDeck()
        {
            var deck = new Deck();
            Card[] firstDeal = deck.GetNext(12);
            Assert.AreEqual(12, firstDeal.Count());
            Assert.AreEqual(12, deck.Index);
            Card[] secondHand = deck.GetNext(3);
            Assert.AreEqual(3, secondHand.Count());
            Assert.AreEqual(15, deck.Index);
        }

        [TestMethod]
        public void CanDealFirst12CardsOntoTable()
        {
            var deck = new Deck();
            var table = new Table {Cards = deck.GetNext(12)};
            Assert.AreEqual(table.Cards.Count(), 12);
        }

        [TestMethod]
        public void CanReplaceCardsInSet()
        {
            var deck = new Deck();
            var table = new Table {Cards = deck.GetNext(12)};

            Card[] nextCards = deck.GetNext(3);
            table.Cards[0] = nextCards[0];
            Assert.AreEqual(table.Cards[0], deck.Cards[12]);

            table.Cards[1] = nextCards[1];
            Assert.AreEqual(table.Cards[1], deck.Cards[13]);

            table.Cards[2] = nextCards[2];
            Assert.AreEqual(table.Cards[2], deck.Cards[14]);
        }

        [TestMethod]
        public void EndOfDeckReturnsNoCards()
        {
            var deck = new Deck();
            Card[] cards = deck.GetNext(100);
            Assert.AreEqual(0, cards.Count());
        }
    }
}