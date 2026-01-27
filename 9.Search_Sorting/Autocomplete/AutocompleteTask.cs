using System;
using System.Collections.Generic;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];

        return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var leftBorderIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        var startIndex = leftBorderIndex + 1;

        var result = new List<string>();
        for (int i = startIndex; i < phrases.Count && result.Count < count; i++)
        {
            if (phrases[i].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                result.Add(phrases[i]);
            else
                break;
        }

        return result.ToArray();
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var leftBorderIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        var rightBorderIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

        return rightBorderIndex - leftBorderIndex - 1;
    }
}
