using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TextFragmentsMatch
{
    class TextMatcher : ITextMatcher
    {
        private Dictionary<string, TextDistance> _textDistanceDictionary;
        private List<string> _textFrags;

        public TextMatcher()
        {
            _textDistanceDictionary =  new Dictionary<string, TextDistance>();
        }
        public string MatchText(List<string> textFrags)
        {
            _textFrags = textFrags;
            InitializeMatckDictionary();
            return null;
        }
        private int CalculateMatchLength(string firstStr, string secondStr)
        {
            while (secondStr.IndexOf(firstStr, StringComparison.Ordinal) != 0 && firstStr.Length != 0)
            {
                firstStr = firstStr.Remove(0, 1);
            }
            return firstStr.Length;
        }
        private string ConcatText(string firstStr, string secondStr, int length)
        {
            return firstStr + secondStr.Remove(0, length);
        }
        private void UpdateDictionary(string newValue, string mergedValue1, string mergedValue2)
        {

        }
        private void InitializeMatckDictionary()
        {
            //Order from long to short.
            //_textFrags = _textFrags.Distinct().OrderBy(s => s.Length).ToList();
            _textFrags.RemoveAll(match: s => _textFrags.Exists(s1 => s1 != s && s1.Contains(s)));
            //foreach (var s in _textFrags)
            //{
            //    _textDistanceDictionary.Add(s, _textFrags.Where(d => d != s).Select(s1 => new TextDistance(s1, CalculateMatchLength(s, s1))).Max());

            //}
            _textFrags.ForEach(s=> _textDistanceDictionary.Add(s, _textFrags.Where(d => d != s).Select(s1 => new TextDistance(s1, CalculateMatchLength(s, s1))).Max()));
            while (_textDistanceDictionary.Count>1)
            {
                var biggestDistance = _textDistanceDictionary.FirstOrDefault(kp => kp.Value == _textDistanceDictionary.Values.Max());
                var concatedStr = ConcatText(biggestDistance.Key, biggestDistance.Value.Text, biggestDistance.Value.Distance);
                _textFrags.Remove(biggestDistance.Key);
                _textFrags.Remove(biggestDistance.Value.Text);
                _textFrags.Add(concatedStr);
               
                _textDistanceDictionary.Remove(biggestDistance.Key);
                _textDistanceDictionary.Remove(biggestDistance.Value.Text);
                if (_textDistanceDictionary.Count < 1)
                {
                    break;
                }
                _textDistanceDictionary.Add(concatedStr,
                    _textFrags.Where(d => d != concatedStr).Select(s1 => new TextDistance(s1, CalculateMatchLength(concatedStr, s1))).Max());
                //foreach (var keyValuePair in _textDistanceDictionary)
                //{
                //    if (keyValuePair.Value.Text == biggestDistance.Key || keyValuePair.Value.Text == biggestDistance.Value.Text)
                //    {
                //        _textDistanceDictionary.Remove(keyValuePair.Key);
                //        _textDistanceDictionary.Add(keyValuePair.Key,
                //            _textFrags.Where(d => d != keyValuePair.Key).Select(s1 => new TextDistance(s1, CalculateMatchLength(keyValuePair.Key, s1)))
                //                .Max());
                //    }
                //}
                foreach (var kv in _textDistanceDictionary.ToList())
                {
                    if (kv.Value.Text == biggestDistance.Key || kv.Value.Text == biggestDistance.Value.Text)
                    {
                        _textDistanceDictionary.Remove(kv.Key);
                        _textDistanceDictionary.Add(kv.Key,
                            _textFrags.Where(d => d != kv.Key).Select(s1 => new TextDistance(s1, CalculateMatchLength(kv.Key, s1)))
                                .Max());
                    }
                }
            }
           
        }
    }
}

