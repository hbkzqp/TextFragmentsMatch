using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextFragmentsMatch
{
    class TextMatcher : ITextMatcher
    {
        private Dictionary<string, TextDistance> _textDistanceDictionary;
        public string MatchText(List<string> textFrags)
        {
            throw new NotImplementedException();
        }
        private int CalculateMatchLength(string firstStr, string SecondStr)
        {
            while (SecondStr.IndexOf(firstStr) != 0&&firstStr.Length!=0)
            {
                firstStr = firstStr.Remove(0, 1);
            }
            return firstStr.Length;
        }
        private string ConcatText(string firstStr, string SecondStr,int length)
        {
            return firstStr + SecondStr.Remove(0, length);
        }
        private void UpdateDictionary(string newValue, string mergedValue1,string mergedValue2)
        {

        }
        private void InitializeMatckDictionary(List<string> textFrags)
        {
            //Order from long to short.
            textFrags.Distinct().OrderBy(s => s.Length);
            foreach (var s in textFrags)
            {
                _textDistanceDictionary.Add(s, textFrags.Where(d => d!= s).Select(s1 => new TextDistance(s1, CalculateMatchLength(s, s1))).Max());
            }

        }
    }
}
