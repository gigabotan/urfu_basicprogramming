namespace Passwords;

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (startIndex == word.Length)
        {
            result.Add(new string(word));
            return;
        }
        if (char.IsLetter(word[startIndex]))
        {
            char lower = char.ToLower(word[startIndex]);
            char upper = char.ToUpper(word[startIndex]);

            word[startIndex] = lower;
            AlternateCharCases(word, startIndex + 1, result);

            if (lower != upper)
            {
                word[startIndex] = upper;
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
        else
        {
            AlternateCharCases(word, startIndex + 1, result);
        }
    }
}
