namespace Pluralize;

public static class PluralizeTask
{
    public static string PluralizeRubles(int count)
    {
        var lastDigit = count % 10;
        var lastTwoDigits = count % 100;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        {
            return "рублей";
        }

        if (lastDigit == 1)
        {
            return "рубль";
        }

        if (lastDigit >= 2 && lastDigit <= 4)
        {
            return "рубля";
        }

        return "рублей";
    }
}
