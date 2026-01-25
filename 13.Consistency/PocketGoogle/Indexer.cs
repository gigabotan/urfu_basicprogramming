using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private Dictionary<string, HashSet<int>> wordToDocuments = new Dictionary<string, HashSet<int>>();
    private Dictionary<string, Dictionary<int, List<int>>> wordPositions = new Dictionary<string, Dictionary<int, List<int>>>();
    private Dictionary<int, List<string>> documentWords = new Dictionary<int, List<string>>();

    public void Add(int id, string documentText)
    {
        var separators = new[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        var wordsInDocument = new List<string>();
        var wordStart = -1;

        for (var i = 0; i <= documentText.Length; i++)
        {
            var isSeparator = i == documentText.Length || Array.IndexOf(separators, documentText[i]) >= 0;

            if (!isSeparator)
            {
                if (wordStart == -1)
                    wordStart = i;
            }
            else if (wordStart != -1)
            {
                AddWord(id, documentText[wordStart..i], wordStart, wordsInDocument);
                wordStart = -1;
            }
        }

        documentWords[id] = wordsInDocument;
    }

    private void AddWord(int id, string word, int position, List<string> wordsInDocument)
    {
        if (!wordToDocuments.TryGetValue(word, out var docSet))
        {
            docSet = new HashSet<int>();
            wordToDocuments[word] = docSet;
        }
        docSet.Add(id);

        if (!wordPositions.TryGetValue(word, out var wordDict))
        {
            wordDict = new Dictionary<int, List<int>>();
            wordPositions[word] = wordDict;
        }
        if (!wordDict.TryGetValue(id, out var positions))
        {
            positions = new List<int>();
            wordDict[id] = positions;
        }
        positions.Add(position);

        wordsInDocument.Add(word);
    }

    public List<int> GetIds(string word)
    {
        if (wordToDocuments.TryGetValue(word, out var docSet))
        {
            return new List<int>(docSet);
        }
        return new List<int>();
    }

    public List<int> GetPositions(int id, string word)
    {
        if (wordPositions.TryGetValue(word, out var wordDict) && wordDict.TryGetValue(id, out var positions))
        {
            return positions;
        }
        return new List<int>();
    }

    public void Remove(int id)
    {
        if (!documentWords.TryGetValue(id, out var words))
        {
            return;
        }

        foreach (var word in words)
        {
            RemoveWordFromDocuments(word, id);
            RemoveWordFromPositions(word, id);
        }

        documentWords.Remove(id);
    }

    private void RemoveWordFromDocuments(string word, int id)
    {
        if (wordToDocuments.TryGetValue(word, out var docSet))
        {
            docSet.Remove(id);
            if (docSet.Count == 0)
            {
                wordToDocuments.Remove(word);
            }
        }
    }

    private void RemoveWordFromPositions(string word, int id)
    {
        if (wordPositions.TryGetValue(word, out var wordDict))
        {
            wordDict.Remove(id);
            if (wordDict.Count == 0)
            {
                wordPositions.Remove(word);
            }
        }
    }
}
