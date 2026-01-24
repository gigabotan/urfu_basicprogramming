namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var nGramFrequency = new Dictionary<string, Dictionary<string, int>>();
        CollectNGrams(text, nGramFrequency);
        return BuildResultDictionary(nGramFrequency);
    }

    private static void CollectNGrams(List<List<string>> text, Dictionary<string, Dictionary<string, int>> nGramFrequency)
    {
        foreach (var sentence in text)
        {
            CollectBigrams(sentence, nGramFrequency);
            CollectTrigrams(sentence, nGramFrequency);
        }
    }

    private static void CollectBigrams(List<string> sentence, Dictionary<string, Dictionary<string, int>> nGramFrequency)
    {
        for (int i = 0; i < sentence.Count - 1; i++)
        {
            AddNGram(nGramFrequency, sentence[i], sentence[i + 1]);
        }
    }

    private static void CollectTrigrams(List<string> sentence, Dictionary<string, Dictionary<string, int>> nGramFrequency)
    {
        for (int i = 0; i < sentence.Count - 2; i++)
        {
            var prefix = sentence[i] + " " + sentence[i + 1];
            AddNGram(nGramFrequency, prefix, sentence[i + 2]);
        }
    }

    private static void AddNGram(Dictionary<string, Dictionary<string, int>> nGramFrequency, string prefix, string nextWord)
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

    private static Dictionary<string, string> BuildResultDictionary(Dictionary<string, Dictionary<string, int>> nGramFrequency)
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
        var maxFrequency = continuations.Values.Max();
        return continuations
            .Where(pair => pair.Value == maxFrequency)
            .OrderBy(pair => pair.Key, System.StringComparer.Ordinal)
            .First()
            .Key;
    }
}
