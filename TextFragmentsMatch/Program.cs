using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace TextFragmentsMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            TextMatcher m = new TextMatcher(new StringHandler());
            List<string> s = new List<string>(){"abcdc","cdcee","eeaa","ccabcd","aa"};
            m.MatchText(s);
        }
    }
}
