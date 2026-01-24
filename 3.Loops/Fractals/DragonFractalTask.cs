using System;

namespace Fractals;

internal static class DragonFractalTask
{
    private static readonly double Sqrt2 = Math.Sqrt(2);
    private static readonly double Cos45 = Math.Cos(Math.PI / 4);
    private static readonly double Sin45 = Math.Sin(Math.PI / 4);
    private static readonly double Cos135 = Math.Cos(3 * Math.PI / 4);
    private static readonly double Sin135 = Math.Sin(3 * Math.PI / 4);

    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        var x = 1.0;
        var y = 0.0;
        var random = new Random(seed);

        for (var i = 0; i < iterationsCount; i++)
        {
            var (newX, newY) = random.Next(2) == 0
                ? ApplyFirstTransformation(x, y)
                : ApplySecondTransformation(x, y);

            x = newX;
            y = newY;
            pixels.SetPixel(x, y);
        }
    }

    private static (double, double) ApplyFirstTransformation(double x, double y)
    {
        var newX = (x * Cos45 - y * Sin45) / Sqrt2;
        var newY = (x * Sin45 + y * Cos45) / Sqrt2;
        return (newX, newY);
    }

    private static (double, double) ApplySecondTransformation(double x, double y)
    {
        var newX = (x * Cos135 - y * Sin135) / Sqrt2 + 1;
        var newY = (x * Sin135 + y * Cos135) / Sqrt2;
        return (newX, newY);
    }
}
