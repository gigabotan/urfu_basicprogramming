using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        var rows = original.GetLength(0);
        var cols = original.GetLength(1);
        var allPixels = GetAllPixels(original, rows, cols);
        var threshold = CalculateThreshold(allPixels, rows * cols, whitePixelsFraction);
        return ApplyThreshold(original, rows, cols, threshold);
    }

    private static List<double> GetAllPixels(double[,] original, int rows, int cols)
    {
        var allPixels = new List<double>();
        for (var x = 0; x < rows; x++)
            for (var y = 0; y < cols; y++)
                allPixels.Add(original[x, y]);
        return allPixels;
    }

    private static double CalculateThreshold(List<double> allPixels, int totalPixels, double whitePixelsFraction)
    {
        allPixels.Sort();
        allPixels.Reverse();
        var whitePixelsCount = (int)(whitePixelsFraction * totalPixels);
        return whitePixelsCount > 0 ? allPixels[whitePixelsCount - 1] : double.MaxValue;
    }

    private static double[,] ApplyThreshold(double[,] original, int rows, int cols, double threshold)
    {
        var result = new double[rows, cols];
        for (var x = 0; x < rows; x++)
            for (var y = 0; y < cols; y++)
                result[x, y] = original[x, y] >= threshold ? 1.0 : 0.0;
        return result;
    }
}
