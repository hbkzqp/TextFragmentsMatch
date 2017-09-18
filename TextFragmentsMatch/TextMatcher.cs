using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TextFragmentsMatch
{
    public class TextMatcher : ITextMatcher
    {
        private Dictionary<string, TextDistance> _textDistanceDictionary;
        private List<string> _textFrags;
        private IStringHandler _stringHandler;
        public TextMatcher(IStringHandler stringHandler)
        {
            _textDistanceDictionary =  new Dictionary<string, TextDistance>();
            _stringHandler = stringHandler;
        }
        public string MatchText(List<string> textFrags)
        {
            if(textFrags.Count>1)
            {
                _textFrags = textFrags;
                InitializeMatckDictionary();
                do
                {
                    var maxDistance = GetMaxDistance();
                    var mergedText = _stringHandler.ConcatText(maxDistance.Key, maxDistance.Value.Text, maxDistance.Value.Distance);
                    updateList(maxDistance, mergedText);
                    UpdateDictionaryForMaxDistance(maxDistance, mergedText);
                    UpdateDictionaryForRelatedDistance(maxDistance);
                }
                while (_textDistanceDictionary.Count > 1);
            }
            
            return _textFrags?.FirstOrDefault();
        }
        private void updateList(KeyValuePair<string, TextDistance> maxDistance, string mergedText)
        {
            _textFrags.Remove(maxDistance.Key);
            _textFrags.Remove(maxDistance.Value.Text);
            _textFrags.Add(mergedText);
        }
        private KeyValuePair<string, TextDistance> GetMaxDistance()
        {
            return  _textDistanceDictionary.FirstOrDefault(kp => kp.Value == _textDistanceDictionary.Values.Max());
        }

        private void UpdateDictionaryForMaxDistance(KeyValuePair<string, TextDistance> maxDistance, string mergedText)
        {
            _textDistanceDictionary.Remove(maxDistance.Key);
            _textDistanceDictionary.Remove(maxDistance.Value.Text);
            //Update dictionary
            _textDistanceDictionary.Add(mergedText,
                _textFrags.Where(d => d != mergedText).Select(s1 => new TextDistance(s1, _stringHandler.CalculateMatchLength(mergedText, s1))).Max());
            

        }

        private void UpdateDictionaryForRelatedDistance(KeyValuePair<string, TextDistance> maxDistance)
        {
            if (_textDistanceDictionary.Count > 1)
            {
                _textDistanceDictionary.ToList().ForEach(kv=> {
                    if (kv.Value.Text == maxDistance.Key || kv.Value.Text == maxDistance.Value.Text)
                    {
                        _textDistanceDictionary.Remove(kv.Key);
                        UpdateMaxDistance(kv.Key);
                    }
                });
               
            }
        }


        private void UpdateMaxDistance(string key)
        {
            _textDistanceDictionary.Add(key,
                        _textFrags.Where(d => d != key).Select(s1 => new TextDistance(s1, _stringHandler.CalculateMatchLength(key, s1)))
                            .Max());
        }

        private void InitializeMatckDictionary()
        {
            //Remove strings which is contained by others
            RemoveContainedStr();
            //Init the dictionary of distance
            _textFrags.ForEach(s=> _textDistanceDictionary.Add(s, _textFrags.Where(d => d != s).Select(s1 => new TextDistance(s1, _stringHandler.CalculateMatchLength(s, s1))).Max())); 
        }
        private void RemoveContainedStr()
        {
            //Remove strings which is contained by others
            _textFrags.RemoveAll(match: s => _textFrags.Exists(s1 => s1 != s && s1.Contains(s)));
        }
    }
}

