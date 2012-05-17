using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGolf.Set.Core
{
    public class Deck
    {
        public Card[] Cards { get; set; }
        public Int32 Index { get; internal set; }

        public Deck()
        {
            Cards = new Card[81];

            Index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            Cards[Index] = new Card((Count) i, (Color) j, (Shape) k, (Shading) l);
                            Index++;
                        }
                    }
                }
            }

            Index = 0;
        }
        
        public void Shuffle()
        {
            var random = new Random();
            var shuffledCards = new Card[81];

            Cards.CopyTo(shuffledCards, 0);

            // Fisher-Yates shuffle algorithm
            // http://stackoverflow.com/questions/1150646/card-shuffling-in-c
            // http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            for (int i = shuffledCards.Length - 1; i > 0; --i)
            {
                int k = random.Next(i + 1);
                Card tempCard = shuffledCards[i];
                shuffledCards[i] = shuffledCards[k];
                shuffledCards[k] = tempCard;
            }

            shuffledCards.CopyTo(Cards, 0);

            Index = 0;
        }

        public Card[] GetNext(Int32 count)
        {
            if (Index + count > 81) return new Card[0];
            IEnumerable<Card> next = Cards.Skip(Index).Take(count);
            Index += count;
            return next.ToArray();
        }
    }
}