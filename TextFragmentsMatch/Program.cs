using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace TextFragmentsMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TextMatcher m = new TextMatcher();
            List<string> s = new List<string>(){"abcdc","cdcee","eeaa","aa"};
            m.MatchText(s);
        }
    }
}
