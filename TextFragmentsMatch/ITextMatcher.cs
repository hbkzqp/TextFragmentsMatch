using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    interface ITextMatcher
    {
        string MatchText(List<string> textFrags);
    }
}
