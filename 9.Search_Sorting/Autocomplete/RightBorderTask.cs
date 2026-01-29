using System;
using System.Collections.Generic;

namespace Autocomplete;

public class RightBorderTask
{
    public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        while (left + 1 < right)
        {
            var middle = left + (right - left) / 2;
            var startsWithPrefix = phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
            var comparison = string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase);
            if (startsWithPrefix || comparison <= 0)
            {
                left = middle;
            }
            else
            {
                right = middle;
            }
        }

        return right;
    }
}
