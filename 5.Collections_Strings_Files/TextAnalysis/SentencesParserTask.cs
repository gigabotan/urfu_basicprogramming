namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        var sentenceDelimiters = new char[] { '.', '!', '?', ';', ':', '(', ')' };
        var sentences = text.Split(sentenceDelimiters);

        foreach (var sentence in sentences)
        {
            var words = ExtractWordsFromSentence(sentence);
            if (words.Count > 0)
                sentencesList.Add(words);
        }

        return sentencesList;
    }

    private static List<string> ExtractWordsFromSentence(string sentence)
    {
        var words = new List<string>();
        var currentWord = new System.Text.StringBuilder();

        foreach (var ch in sentence)
        {
            ProcessCharacter(ch, currentWord, words);
        }

        AddWordIfNotEmpty(currentWord, words);
        return words;
    }

    private static void ProcessCharacter(char ch, System.Text.StringBuilder currentWord, List<string> words)
    {
        if (char.IsLetter(ch) || ch == '\'')
        {
            currentWord.Append(char.ToLower(ch));
        }
        else
        {
            AddWordIfNotEmpty(currentWord, words);
        }
    }

    private static void AddWordIfNotEmpty(System.Text.StringBuilder word, List<string> words)
    {
        if (word.Length > 0)
        {
            words.Add(word.ToString());
            word.Clear();
        }
    }
}
