using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    public class TextDistance:IComparable
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


        public int CompareTo(object obj)
        {
            if (obj is TextDistance)
            {
                var objDistance = obj as TextDistance;
                if (Distance > objDistance.Distance)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            throw new Exception("Object not valid.");
        }
    }
}
