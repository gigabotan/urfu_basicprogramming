using System;
using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var allPixels = GetAllPixels(original, width, height);
        var threshold = CalculateThreshold(allPixels, width * height, whitePixelsFraction);
        return ApplyThreshold(original, width, height, threshold);
    }

    private static List<double> GetAllPixels(double[,] original, int rows, int cols)
    {
        var allPixels = new List<double>(rows * cols);
        foreach (var value in original)
            allPixels.Add(value);
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
