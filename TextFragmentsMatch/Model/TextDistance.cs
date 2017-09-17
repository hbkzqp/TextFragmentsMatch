using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    public class TextDistance
    {
        public TextDistance()
        {
        }

        public TextDistance(string text, int distance)
        {
            Text = text;
            Distance = distance;
        }

        public string Text { get; set; }
        public int Distance { get; set; }
        public static bool operator >(TextDistance td1, TextDistance td2)
        {
            return td1.Distance > td2.Distance;
        }

        public static bool operator <(TextDistance td1, TextDistance td2)
        {
            return td1.Distance < td2.Distance;
        }
    }
}
