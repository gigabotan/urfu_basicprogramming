using System;

namespace Interest;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine();
        var result = Calculate(input);
        Console.WriteLine(result);
    }

    public static double Calculate(string userInput)
    {
        var parts = userInput.Split(' ');
        var initialAmount = double.Parse(parts[0]);
        var annualRate = double.Parse(parts[1]);
        var monthCount = int.Parse(parts[2]);

        var monthlyRate = annualRate / 100 / 12;
        var finalAmount = initialAmount * Math.Pow(1 + monthlyRate, monthCount);

        return Math.Floor(finalAmount);
    }
}
