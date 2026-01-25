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
            var words = new List<string>();
            var currentWord = new System.Text.StringBuilder();

            foreach (var ch in sentence)
            {
                if (char.IsLetter(ch) || ch == '\'')
                {
                    currentWord.Append(char.ToLower(ch));
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        words.Add(currentWord.ToString());
                        currentWord.Clear();
                    }
                }
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            if (words.Count > 0)
            {
                sentencesList.Add(words);
            }
        }

        return sentencesList;
    }
}
