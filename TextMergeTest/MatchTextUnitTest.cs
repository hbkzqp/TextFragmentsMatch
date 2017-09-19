using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextFragmentsMatch;

namespace TextMergeTest
{
    [TestClass]
    public class MatchTextUnitTest
    {
        //Generally it should be injected by Dependency injection, currently we have only Unit test, so I directly new one.
        private ITextMatcher _matcher = new TextMatcher(new StringHandler());
        [TestMethod]
        public void TestBasicCase()
        {
            //Assign
            List<string> _basicTestList = new List<string>() { "abcdc", "cdcee", "eeaa", "ccabcd", "aa" };
            var expect = "ccabcdceeaa";
            //Act
            var result = _matcher.MatchText(_basicTestList);
            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void TestOneWordCase()
        {
            //Assign
            List<string> singleWordTest = new List<string>() { "abcdc" };
            var expect = "abcdc";
            //Act
            var result = _matcher.MatchText(singleWordTest);
            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void TestNullCase()
        {
            //Assign
            List<string> nullTestList = new List<string>() { null };
            string expect = null;
            //Act
            var result = _matcher.MatchText(nullTestList);
            //Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void TestDemoCase()
        {
            //Assign
            List<string> demoTestList = new List<string>() { "a l l   i s   w e l l", "e l l   t h a t   e n", "h a t   e n d", "t   e n d s   w e l l" };
            var expect = "a l l   i s   w e l l   t h a t   e n d s   w e l l";
            //Act
            var result = _matcher.MatchText(demoTestList);
            //Assert
            Assert.AreEqual(expect, result);
        }
        [TestMethod]
        public void TestBiglCase()
        {
            //Assign
            List<string> _basicTestList = new List<string>() { "abcdc", "cdcee", "eeaa", "ccabcd", "aa", "a l l   i s   w e l l", "e l l   t h a t   e n", "h a t   e n d", "t   e n d s   w e l l" };
            var expect = "ccabcdceeaa l l   i s   w e l l   t h a t   e n d s   w e l l";
            //Act
            var result = _matcher.MatchText(_basicTestList);
            //Assert
            Assert.AreEqual(expect, result);
        }
    }
}
