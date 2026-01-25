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
        for (var i = 0; i < expectedResult.Length; ++i)
        {
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
        }
    }

    // Простое слово без пробелов
    [TestCase("text", new[] { "text" })]
    // Два слова, разделенные пробелом
    [TestCase("hello world", new[] { "hello", "world" })]
    // Множественные пробелы между словами
    [TestCase("a    b", new[] { "a", "b" })]
    // Пустая строка
    [TestCase("", new string[] { })]
    // Только пробелы
    [TestCase("   ", new string[] { })]
    // Пустая строка в одинарных кавычках
    [TestCase("''", new[] { "" })]
    // Текст с пробелами в одинарных кавычках
    [TestCase("'hello world'", new[] { "hello world" })]
    // Двойные кавычки, содержащие одинарные кавычки
    [TestCase("\"a 'b' c\"", new[] { "a 'b' c" })]
    // Одинарные кавычки, содержащие двойные кавычки
    [TestCase("'a \"b\" c'", new[] { "a \"b\" c" })]
    // Слово, за которым следует текст в кавычках без пробела
    [TestCase("a\"b c\"", new[] { "a", "b c" })]
    // Текст в кавычках, за которым следует слово без пробела
    [TestCase("\"b c\"d", new[] { "b c", "d" })]
    // Незакрытая двойная кавычка
    [TestCase("\"abc", new[] { "abc" })]
    // Незакрытая двойная кавычка с пробелами
    [TestCase("\"a b ", new[] { "a b " })]
    // Экранированные двойные кавычки внутри двойных кавычек
    [TestCase("\"a \\\"c\\\"\"", new[] { "a \"c\"" })]
    // Экранированные одинарные кавычки внутри одинарных кавычек
    [TestCase("'a \\'c\\''", new[] { "a 'c'" })]
    // Экранированный обратный слеш в кавычках
    [TestCase("\"\\\\\"", new[] { "\\" })]
    // Обратные слеши вне кавычек
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
        var index = 0;
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
        var index = startIndex;
        while (index < line.Length && !char.IsWhiteSpace(line[index])
            && line[index] != '"' && line[index] != '\'')
        {
            index++;
        }
        var length = index - startIndex;
        var value = line.Substring(startIndex, length);
        return new Token(value, startIndex, length);
    }

    public static Token ReadQuotedField(string line, int startIndex)
    {
        return QuotedFieldTask.ReadQuotedField(line, startIndex);
    }
}
