using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TextFragmentsMatch
{
    public class TextMatcher : ITextMatcher
    {
        //The dictionary which stores the string and its longest distance.
        private Dictionary<string, TextDistance> _textDistanceDictionary;
        //The text to merge
        private List<string> _textFrags;
        //The string handler to make string process
        private readonly IStringHandler _stringHandler;
        public TextMatcher(IStringHandler stringHandler)
        {
            _textDistanceDictionary = new Dictionary<string, TextDistance>();
            _stringHandler = stringHandler;
        }
        public string MatchText(List<string> textFrags)
        {
            if (textFrags.Count > 1)
            {
                _textFrags = textFrags;
                InitializeMatckDictionary();
                do
                {
                    var maxDistance = GetMaxDistance();
                    var mergedText = _stringHandler.ConcatText(maxDistance.Key, maxDistance.Value.Text, maxDistance.Value.Distance);
                    UpdateFragList(maxDistance, mergedText);
                    UpdateDictionaryForMaxDistance(maxDistance, mergedText);
                    UpdateDictionaryForRelatedDistance(maxDistance);
                }
                while (_textDistanceDictionary.Count > 1);
            }

            return _textFrags?.FirstOrDefault();
        }
        /// <summary>
        /// Once we merge a pair of text, we need to remove them and add merged in t he list.
        /// </summary>
        /// <param name="maxDistance"></param>
        /// <param name="mergedText"></param>
        private void UpdateFragList(KeyValuePair<string, TextDistance> maxDistance, string mergedText)
        {
            _textFrags.Remove(maxDistance.Key);
            _textFrags.Remove(maxDistance.Value.Text);
            _textFrags.Add(mergedText);
        }
        /// <summary>
        /// Get the Max distance in the dictionary.
        /// </summary>
        /// <returns></returns>
        private KeyValuePair<string, TextDistance> GetMaxDistance()
        {
            return _textDistanceDictionary.FirstOrDefault(kp => kp.Value == _textDistanceDictionary.Values.Max());
        }
        /// <summary>
        /// Once we merge a pair of text, we need to remove them and add merged in the dictionary with the Max distance.
        /// </summary>
        /// <param name="maxDistance"></param>
        /// <param name="mergedText"></param>
        private void UpdateDictionaryForMaxDistance(KeyValuePair<string, TextDistance> maxDistance, string mergedText)
        {
            _textDistanceDictionary.Remove(maxDistance.Key);
            _textDistanceDictionary.Remove(maxDistance.Value.Text);
            //Update dictionary
            _textDistanceDictionary.Add(mergedText,
                _textFrags.Where(d => d != mergedText).Select(s1 => new TextDistance(s1, _stringHandler.CalculateMatchDistance(mergedText, s1))).Max());
        }
        /// <summary>
        /// Once we merge a pair of text, we need to update all the text in the list which had longgest distance with that pair.
        /// </summary>
        /// <param name="maxDistance"></param>
        private void UpdateDictionaryForRelatedDistance(KeyValuePair<string, TextDistance> maxDistance)
        {
            if (_textDistanceDictionary.Count > 1)
            {
                _textDistanceDictionary.ToList().ForEach(kv => {
                    if (kv.Value.Text == maxDistance.Key || kv.Value.Text == maxDistance.Value.Text)
                    {
                        _textDistanceDictionary.Remove(kv.Key);
                        InsertMaxDistance(kv.Key);
                    }
                });

            }
        }
        /// <summary>
        /// Find the max distance from the list for the exact string .
        /// </summary>
        /// <param name="key">The string to find distance</param>
        private void InsertMaxDistance(string key)
        {
            _textDistanceDictionary.Add(key,
                _textFrags.Where(d => d != key).Select(s1 => new TextDistance(s1, _stringHandler.CalculateMatchDistance(key, s1)))
                    .Max());
        }
        /// <summary>
        /// Depending on the text list, init the Dictionary with the text and its loggest distance.
        /// </summary>
        private void InitializeMatckDictionary()
        {
            //Remove strings which is contained by others
            RemoveContainedStr();
            //Init the dictionary of distance
            _textFrags.ForEach(InsertMaxDistance);
        }
        /// <summary>
        /// Remove strings which is contained by others
        /// </summary>
        private void RemoveContainedStr()
        {
            
            _textFrags.RemoveAll(match: s => _textFrags.Exists(s1 => s1 != s && s1.Contains(s)));
        }
    }
}

