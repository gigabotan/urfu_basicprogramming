using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
    [TestCase("''", 0, "", 2)]
    [TestCase("'a'", 0, "a", 3)]
    [TestCase("\"\"", 0, "", 2)]
    [TestCase("\"abc\"", 0, "abc", 5)]
    [TestCase("'hello world'", 0, "hello world", 13)]
    [TestCase("\"a 'b' c\"", 0, "a 'b' c", 9)]
    [TestCase("'a \"b\" c'", 0, "a \"b\" c", 9)]
    [TestCase("\"a \\\"c\\\"\"", 0, "a \"c\"", 9)]
    [TestCase("\"\\\\\"", 0, "\\", 4)]
    [TestCase("\"abc", 0, "abc", 4)]
    [TestCase("'unclosed", 0, "unclosed", 9)]
    [TestCase("\"a\\\\b\"", 0, "a\\b", 6)]
    [TestCase("\"test\\\"quote\"", 0, "test\"quote", 13)]
    public void Test(string line, int startIndex,
        string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        var expectedToken = new Token(expectedValue, startIndex, expectedLength);
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(expectedToken, actualToken);
    }
}

class QuotedFieldTask
{
    public static Token ReadQuotedField(string line, int startIndex)
    {
        var quoteChar = line[startIndex];
        var currentIndex = startIndex + 1;
        var value = "";
        while (currentIndex < line.Length)
        {
            var result = ProcessCharacter(line, quoteChar, startIndex,
                currentIndex, value);
            if (result.Token != null)
                return result.Token;
            currentIndex = result.Index;
            value = result.Value;
        }
        return new Token(value, startIndex, line.Length - startIndex);
    }

    private static (Token Token, int Index, string Value) ProcessCharacter(
        string line, char quoteChar, int startIndex, int index, string value)
    {
        var currentChar = line[index];
        if (currentChar == '\\' && index + 1 < line.Length)
        {
            return (null, index + 2, value + line[index + 1]);
        }
        else if (currentChar == quoteChar)
        {
            var token = new Token(value, startIndex, index - startIndex + 1);
            return (token, index, value);
        }
        else
        {
            return (null, index + 1, value + currentChar);
        }
    }
}
