using System;
using System.Collections.Generic;
using System.Text;

namespace TextFragmentsMatch
{
    public interface ITextMatcher
    {
        string MatchText(List<string> textFrags);
    }
}
