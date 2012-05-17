using System;

namespace CodeGolf.Set.Core
{
    public class Card
    {
        public Int32[] Characteristics { get; set; }

        public Card(Count count, Color color, Shape shape, Shading shading)
        {
            Characteristics = new Int32[4];
            Characteristics[(int) Characteristic.Count] = (int) count;
            Characteristics[(int) Characteristic.Color] = (int) color;
            Characteristics[(int) Characteristic.Shape] = (int) shape;
            Characteristics[(int) Characteristic.Shading] = (int) shading;
        }

        public override string ToString()
        {
            return String.Format("[{0} {1} {2} {3}]", (Count)Characteristics[(int)Characteristic.Count], (Shading)Characteristics[(int)Characteristic.Shading], (Color)Characteristics[(int)Characteristic.Color], (Shape)Characteristics[(int)Characteristic.Shape]);
        }
    }
}