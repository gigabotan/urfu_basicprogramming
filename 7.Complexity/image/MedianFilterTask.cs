using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
    public static double[,] MedianFilter(double[,] original)
    {
        var rows = original.GetLength(0);
        var cols = original.GetLength(1);
        var result = new double[rows, cols];

        for (var x = 0; x < rows; x++)
            for (var y = 0; y < cols; y++)
                result[x, y] = GetMedian(original, x, y);

        return result;
    }

    private static double GetMedian(double[,] original, int x, int y)
    {
        var values = GetNeighborhoodValues(original, x, y);
        values.Sort();
        return CalculateMedian(values);
    }

    private static List<double> GetNeighborhoodValues(double[,] original, int x, int y)
    {
        var rows = original.GetLength(0);
        var cols = original.GetLength(1);
        var values = new List<double>();
        var xMin = Math.Max(0, x - 1);
        var xMax = Math.Min(rows - 1, x + 1);
        var yMin = Math.Max(0, y - 1);
        var yMax = Math.Min(cols - 1, y + 1);
        for (var i = xMin; i <= xMax; i++)
            for (var j = yMin; j <= yMax; j++)
                values.Add(original[i, j]);
        return values;
    }

    private static double CalculateMedian(List<double> values)
    {
        var count = values.Count;
        if (count % 2 == 1)
            return values[count / 2];
        else
            return (values[count / 2 - 1] + values[count / 2]) / 2.0;
    }
}
