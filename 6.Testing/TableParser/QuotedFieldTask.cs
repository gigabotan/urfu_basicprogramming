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
    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        NUnit.Framework.Legacy.ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
    }

    [Test]
    public void TestMain()
    {
        // Простой способ проверить работу метода
        var token1 = QuotedFieldTask.ReadQuotedField("'hello'", 0);
        System.Console.WriteLine($"Test 1: {token1}");

        var token2 = QuotedFieldTask.ReadQuotedField("\"a \\\"c\\\"\"", 0);
        System.Console.WriteLine($"Test 2: {token2}");

        var token3 = QuotedFieldTask.ReadQuotedField("\"unclosed", 0);
        System.Console.WriteLine($"Test 3: {token3}");
    }
}

class QuotedFieldTask
{
    public static Token ReadQuotedField(string line, int startIndex)
    {
        // Гарантируется, что на startIndex находится открывающая кавычка
        char quoteChar = line[startIndex];
        int currentIndex = startIndex + 1;
        string value = "";

        while (currentIndex < line.Length)
        {
            char currentChar = line[currentIndex];

            // Проверяем экранирование
            if (currentChar == '\\' && currentIndex + 1 < line.Length)
            {
                // Добавляем экранированный символ
                value += line[currentIndex + 1];
                currentIndex += 2;
            }
            // Проверяем закрывающую кавычку
            else if (currentChar == quoteChar)
            {
                // Нашли закрывающую кавычку
                int length = currentIndex - startIndex + 1;
                return new Token(value, startIndex, length);
            }
            else
            {
                // Обычный символ
                value += currentChar;
                currentIndex++;
            }
        }

        // Кавычка не закрыта - поле до конца строки
        int finalLength = line.Length - startIndex;
        return new Token(value, startIndex, finalLength);
    }
}
