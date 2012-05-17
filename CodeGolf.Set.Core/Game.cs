using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace CodeGolf.Set.Core
{
    public class Game
    {
        public Int32 Hands { get; set; }

        public void Play()
        {
            var deck = new ShuffledDeck();
            var table = new Table {Cards = deck.GetNext(12)};
            Hands = 1;

            while (true)
            {
                IList<Card> set = FindSet(table);

                if (set == null) break; // Game over, no sets found
                Console.WriteLine(String.Format("Match found: {{{0} {1} {2}}}", set[0], set[1], set[2]));

                if (!ReplaceSetWithNewCards(deck, table, set)) break; // Game over, no more cards

                Hands++;
            }

            Console.WriteLine(String.Format("Game Over: {0} hands played.", Hands));
            Console.WriteLine(Hands < 24 ? "No more matches." : "No more cards.");
            Console.WriteLine("What's left on the Table?");
            Console.WriteLine(String.Join(",", table.Cards.Take(4)));
            Console.WriteLine(String.Join(",", table.Cards.Skip(4).Take(4)));
            Console.WriteLine(String.Join(",", table.Cards.Skip(8).Take(4)));
            Console.WriteLine();
        }

        private static IList<Card> FindSet(Table table)
        {
            // Get all 3 card combinations and sets there within
            // http://www.codeproject.com/KB/recipes/Combinatorics.aspx
            var combinations = new Combinations<Card>(table.Cards, 3);
            IEnumerable<IList<Card>> sets = combinations.Where(IsMatch);

            // Return the first set found, return null if no sets
            return sets.FirstOrDefault();
        }

        private static bool IsMatch(IList<Card> set)
        {
            bool found = false;

            // Compare each characteristic of the 3 cards. 
            for (int i = 0; i < 4; i++)
            {
                // If the sum of each characteristic is mod 3 then we have a matching set.
                if (!CharactersticIsMod3(i, set)) break;
                found = i == 3;
            }
            return found;
        }

        private static bool CharactersticIsMod3(int i, IList<Card> set)
        {
            int sum = set[0].Characteristics[i] + set[1].Characteristics[i] + set[2].Characteristics[i];
            return sum%3 != 0;
        }

        private static bool ReplaceSetWithNewCards(Deck deck, Table table, IList<Card> set)
        {
            Card[] nextCards = deck.GetNext(3);
            if (!nextCards.Any()) return false; // Not enough cards left in deck

            // Loop through each card in the set looking for it's position on the table
            // And replace card with new card from the deck.
            for (int i = 0; i < set.Count; i++)
            {
                for (int j = 0; j < table.Cards.Count(); j++)
                {
                    if (table.Cards[j] != set[i]) continue;

                    Console.WriteLine(String.Format("Replace Card {0}, {1} with {2}", i, table.Cards[j], nextCards[i]));
                    //TODO use list. Don't replace in array.
                    //TODO look for more than one set
                    //TODO if no sets, add 3 more cards
                    //TODO try combinations of two and loop through remaining cards
                    table.Cards[j] = nextCards[i];
                    break;
                }
            }

            return true;
        }
    }
}