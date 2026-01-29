using System;
using System.Collections.Generic;

namespace Autocomplete;

public class LeftBorderTask
{
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (left >= right - 1)
            return left;

        int middle = left + (right - left) / 2;
        bool startsWithPrefix = phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        int comparison = string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase);
        if (startsWithPrefix || comparison >= 0)
        {
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
        return GetLeftBorderIndex(phrases, prefix, middle, right);
    }
}
