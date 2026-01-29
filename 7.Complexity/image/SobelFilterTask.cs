using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var imageWidth = g.GetLength(0);
        var imageHeight = g.GetLength(1);
        var result = new double[imageWidth, imageHeight];
        var sy = Transpose(sx);
        var kernelSize = sx.GetLength(0);
        var offset = kernelSize / 2;
        for (var x = offset; x < imageWidth - offset; x++)
            for (var y = offset; y < imageHeight - offset; y++)
            {
                var gx = Convolve(g, sx, x, y);
                var gy = Convolve(g, sy, x, y);
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
    }

    private static double[,] Transpose(double[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var result = new double[cols, rows];
        for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                result[j, i] = matrix[i, j];
        return result;
    }

    private static double Convolve(double[,] g, double[,] kernel, int x, int y)
    {
        var size = kernel.GetLength(0);
        var offset = size / 2;
        var sum = 0.0;
        for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                sum += g[x - offset + i, y - offset + j] * kernel[i, j];
        return sum;
    }
}
