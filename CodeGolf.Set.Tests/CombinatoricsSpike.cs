using System;
using Facet.Combinatorics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGolf.Set.Tests
{
    [TestClass]
    public class CombinatoricsSpike
    {
        [TestMethod]
        public void CanMakeValidCombinations()
        {
            char[] inputSet = {'A', 'B', 'C', 'D'};

            var combinations = new Combinations<char>(inputSet, 3);
            string cformat = "Combinations of {{A B C D}} choose 3: size = {0}";
            Console.WriteLine(String.Format(cformat, combinations.Count));
            foreach (var c in combinations)
            {
                Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
            }
        }
    }
}