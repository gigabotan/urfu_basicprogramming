using System;
using System.Collections.Generic;

namespace Autocomplete;

public class LeftBorderTask
{
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (left >= right - 1)
            return left;

        var middle = left + (right - left) / 2;
        var startsWithPrefix = phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        var comparison = string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase);
        return (startsWithPrefix || comparison >= 0) ?
            GetLeftBorderIndex(phrases, prefix, left, middle) :
            GetLeftBorderIndex(phrases, prefix, middle, right);
    }
}
