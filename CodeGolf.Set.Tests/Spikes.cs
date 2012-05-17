using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeGolf.Set.Core;
using Facet.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGolf.Set.Tests
{
    [TestClass]
    public class Spikes
    {
        [TestMethod]
        public void GetSetCombinations()
        {
            var deck = new Deck();
            var table = new Table { Cards = deck.GetNext(12) };

            Combinations<Card> combinations = new Combinations<Card>(table.Cards, 3);

            foreach (IList<Card> c in combinations)
            {
                Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
            }
        }
       
        [TestMethod]
        public void FindFirstMatchingCombinations()
        {
            var deck = new Deck();
            var table = new Table {Cards = deck.GetNext(12)};

            var combinations = new Combinations<Card>(table.Cards, 3);
            
            foreach (IList<Card> c in combinations)
            {
                if (IsMatch(c))
                {
                    Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
                    break;
                }
            }
        }

        [TestMethod]
        public void FindMatchingCombinationsAndReplaceCards()
        {
            var deck = new Deck();
            var table = new Table { Cards = deck.GetNext(12) };
            var combinations = new Combinations<Card>(table.Cards, 3);
            var sets = combinations.Where(IsMatch);
            var cards = sets.FirstOrDefault();
            Console.WriteLine(String.Format("Match found: {{{0} {1} {2}}}", cards[0], cards[1], cards[2]));
            ReplaceSetWithNewCards(deck, table, cards);
        }

        [TestMethod]
        public void EndOfDeckReturnsNoCards()
        {
            var deck = new Deck();
            var cards = deck.GetNext(100);
            Assert.AreEqual(0, cards.Count());
        }

        [TestMethod]
        public void PlayToEnd()
        {
            PlayGame();
        }

        [TestMethod]
        public void PlayToEndFiveTimes()
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            Console.WriteLine("Started");

            for (int i = 0; i < 5; i++)
            {
                PlayGame();
            }

            st.Stop();
            Console.WriteLine("Elapsed = {0}", st.Elapsed);
        }

        private static void PlayGame()
        {
            var deck = new ShuffledDeck();
            var table = new Table { Cards = deck.GetNext(12) };
            int hands = 1;
            while (true)
            {
                IList<Card> set = FindSet(table);

                if (set == null) break;

                Console.WriteLine(String.Format("Match found: {{{0} {1} {2}}}", set[0], set[1], set[2]));

                if (!ReplaceSetWithNewCards(deck, table, set)) break;
                hands++;
            }

            Console.WriteLine(String.Format("Game Over: {0} hands played.", hands));
            Console.WriteLine(hands < 24 ? "No more matches." : "No more cards.");
            Console.WriteLine("What's left on the Table?");
            Console.WriteLine(String.Join<Card>(",", table.Cards.Take(4)));
            Console.WriteLine(String.Join<Card>(",", table.Cards.Skip(4).Take(4)));
            Console.WriteLine(String.Join<Card>(",", table.Cards.Skip(8).Take(4)));
            Console.WriteLine();
        }

        private static IList<Card> FindSet(Table table)
        {
            var combinations = new Combinations<Card>(table.Cards, 3);
            var sets = combinations.Where(IsMatch);
            return sets.FirstOrDefault();
        }

        private static bool ReplaceSetWithNewCards(Deck deck, Table table, IList<Card> set)
        {
            Card[] nextCards = deck.GetNext(3);
            if (!nextCards.Any()) return false;              
            
            for (int i = 0; i < set.Count; i++)
            {
                String before = String.Join<Card>(",", table.Cards);
                
                for (int j = 0; j < table.Cards.Count(); j++)
                {
                    if (table.Cards[j] != set[i]) continue;
                    Console.WriteLine(String.Format("Replace Card {0}, {1} with {2}", i, table.Cards[j], nextCards[i]));
                    table.Cards[j] = nextCards[i];
                    break;
                }

                String after = String.Join<Card>(",", table.Cards);
                Assert.AreNotEqual(before, after);
            }

            return true;
        }

        private static bool IsMatch(IList<Card> set)
        {
            bool found = false;
            for (int i = 0; i < 4; i++)
            {
                int sum = set[0].Characteristics[i] + set[1].Characteristics[i] + set[2].Characteristics[i];
                if (sum % 3 != 0) break;
                found = i == 3;
            }
            return found;
        }
    }
}