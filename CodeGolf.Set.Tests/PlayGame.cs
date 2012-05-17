using System;
using System.Diagnostics;
using CodeGolf.Set.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGolf.Set.Tests
{
    [TestClass]
    public class PlayGame
    {
        [TestMethod]
        public void PlaySet()
        {
            const Int32 gamesToPlay = 1;

            var st = new Stopwatch();
            st.Start();

            Console.WriteLine(String.Format("Started {0} Games of Set", gamesToPlay));

            var game = new Game();

            for (int i = 0; i < gamesToPlay; i++)
            {
                game.Play();
            }

            st.Stop();
            Console.WriteLine("Elapsed = {0}", st.Elapsed);

            // Started 1000 Games of Set
            // Elapsed = 00:00:01.3345030
        }
    }
}