namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var nGramFrequency = new Dictionary<string, Dictionary<string, int>>();
        CollectNGrams(text, nGramFrequency);
        return BuildResultDictionary(nGramFrequency);
    }

    private static void CollectNGrams(
        List<List<string>> text,
        Dictionary<string, Dictionary<string, int>> nGramFrequency)
    {
        foreach (var sentence in text)
        {
            for (var i = 0; i < sentence.Count - 1; i++)
            {
                AddNGram(nGramFrequency, sentence[i], sentence[i + 1]);
                if (i < sentence.Count - 2)
                {
                    var prefix = sentence[i] + " " + sentence[i + 1];
                    AddNGram(nGramFrequency, prefix, sentence[i + 2]);
                }
            }
        }
    }

    private static void AddNGram(
        Dictionary<string, Dictionary<string, int>> nGramFrequency,
        string prefix,
        string nextWord)
    {
        if (!nGramFrequency.TryGetValue(prefix, out var continuations))
        {
            continuations = [];
            nGramFrequency[prefix] = continuations;
        }
        if (!continuations.TryGetValue(nextWord, out var count))
            count = 0;
        continuations[nextWord] = count + 1;
    }

    private static Dictionary<string, string> BuildResultDictionary(
        Dictionary<string, Dictionary<string, int>> nGramFrequency)
    {
        var result = new Dictionary<string, string>();
        foreach (var prefix in nGramFrequency.Keys)
        {
            result[prefix] = GetMostFrequentWord(nGramFrequency[prefix]);
        }
        return result;
    }

    private static string GetMostFrequentWord(Dictionary<string, int> continuations)
    {
        var maxFrequency = GetMaxFrequency(continuations);
        return FindFirstWordWithFrequency(continuations, maxFrequency);
    }

    private static int GetMaxFrequency(Dictionary<string, int> continuations)
    {
        var maxFrequency = 0;
        foreach (var frequency in continuations.Values)
        {
            if (frequency > maxFrequency)
                maxFrequency = frequency;
        }
        return maxFrequency;
    }

    private static string FindFirstWordWithFrequency(
        Dictionary<string, int> continuations,
        int frequency)
    {
        var mostFrequentWord = (string)null;
        foreach (var pair in continuations)
        {
            if (pair.Value == frequency)
            {
                if (mostFrequentWord == null ||
                    string.CompareOrdinal(pair.Key, mostFrequentWord) < 0)
                    mostFrequentWord = pair.Key;
            }
        }
        return mostFrequentWord;
    }
}
