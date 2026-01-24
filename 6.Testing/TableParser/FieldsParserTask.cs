using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
    public static void Test(string input, string[] expectedResult)
    {
        var actualResult = FieldsParserTask.ParseLine(input);
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
        for (int i = 0; i < expectedResult.Length; ++i)
        {
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
        }
    }

    [TestCase("hello world", new[] { "hello", "world" })]
    [TestCase("a    b", new[] { "a", "b" })]
    [TestCase("", new string[] { })]
    [TestCase("   ", new string[] { })]
    [TestCase("''", new[] { "" })]
    [TestCase("'hello world'", new[] { "hello world" })]
    [TestCase("\"a 'b' c\"", new[] { "a 'b' c" })]
    [TestCase("'a \"b\" c'", new[] { "a \"b\" c" })]
    [TestCase("a\"b c\"", new[] { "a", "b c" })]
    [TestCase("\"b c\"d", new[] { "b c", "d" })]
    [TestCase("\"abc", new[] { "abc" })]
    [TestCase("\"a b ", new[] { "a b " })]
    [TestCase("\"a \\\"c\\\"\"", new[] { "a \"c\"" })]
    [TestCase("'a \\'c\\''", new[] { "a 'c'" })]
    [TestCase("\"\\\\\"", new[] { "\\" })]
    [TestCase("\\\\", new[] { "\\\\" })]
    public static void RunTests(string input, string[] expectedOutput)
    {
        Test(input, expectedOutput);
    }
}

public class FieldsParserTask
{
    public static List<Token> ParseLine(string line)
    {
        var tokens = new List<Token>();
        int index = 0;
        while (index < line.Length)
        {
            if (char.IsWhiteSpace(line[index]))
            {
                index++;
                continue;
            }
            var token = ReadField(line, index);
            tokens.Add(token);
            index = token.GetIndexNextToToken();
        }
        return tokens;
    }

    private static Token ReadField(string line, int startIndex)
    {
        if (line[startIndex] == '"' || line[startIndex] == '\'')
            return ReadQuotedField(line, startIndex);
        return ReadSimpleField(line, startIndex);
    }

    private static Token ReadSimpleField(string line, int startIndex)
    {
        int index = startIndex;
        while (index < line.Length && !char.IsWhiteSpace(line[index])
            && line[index] != '"' && line[index] != '\'')
        {
            index++;
        }
        int length = index - startIndex;
        string value = line.Substring(startIndex, length);
        return new Token(value, startIndex, length);
    }

    public static Token ReadQuotedField(string line, int startIndex)
    {
        return QuotedFieldTask.ReadQuotedField(line, startIndex);
    }
}
