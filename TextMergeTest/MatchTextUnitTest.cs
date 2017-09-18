using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextFragmentsMatch;

namespace TextMergeTest
{
    [TestClass]
    public class MatchTextUnitTest
    {
        private List<string> _basicTestList = new List<string>() { "abcdc", "cdcee", "eeaa", "ccabcd", "aa" };
        private ITextMatcher _matcher = new TextMatcher(new StringHandler());
        [TestMethod]
        public void TestBasicSuccessfullCase()
        {
            //Assign
            var expect = "ccabcdceeaa";
            //Act
            var result = _matcher.MatchText(_basicTestList);
            //Assert
            Assert.AreEqual(expect, result);
        }
    }
}
