using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    public class StringHandler:IStringHandler
    {
        public  int CalculateMatchLength(string firstStr, string secondStr)
        {
            while (secondStr.IndexOf(firstStr, StringComparison.Ordinal) != 0 && firstStr.Length != 0)
            {
                firstStr = firstStr.Remove(0, 1);
            }
            return firstStr.Length;
        }

        public string ConcatText(string firstStr, string secondStr, int length)
        {
            return firstStr + secondStr.Remove(0, length);
        }
    }
}
