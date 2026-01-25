namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var words = phraseBeginning.Split(' ').ToList();

        for (int i = 0; i < wordsCount; i++)
        {
            var nextWord = GetNextWord(nextWords, words);
            if (nextWord == null)
                break;
            words.Add(nextWord);
        }

        return string.Join(" ", words);
    }

    private static string GetNextWord(Dictionary<string, string> nextWords, List<string> words)
    {
        if (words.Count >= 2)
        {
            var bigramKey = words[words.Count - 2] + " " + words[words.Count - 1];
            if (nextWords.TryGetValue(bigramKey, out var nextWord))
                return nextWord;
        }

        var unigramKey = words[words.Count - 1];
        if (nextWords.TryGetValue(unigramKey, out var word))
            return word;

        return null;
    }
}
