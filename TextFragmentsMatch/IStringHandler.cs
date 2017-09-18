using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    public interface IStringHandler
    {
        int CalculateMatchLength(string firstStr, string secondStr);
        string ConcatText(string firstStr, string secondStr, int length);
    }
}
