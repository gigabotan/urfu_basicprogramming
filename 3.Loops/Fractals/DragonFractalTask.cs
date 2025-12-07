using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        // Начальная точка
        double x = 1.0, y = 0.0;
        var random = new Random(seed);

        // Предвычисляем константы
        var sqrt2 = Math.Sqrt(2);
        var cos45 = Math.Cos(Math.PI / 4);
        var sin45 = Math.Sin(Math.PI / 4);
        var cos135 = Math.Cos(3 * Math.PI / 4);
        var sin135 = Math.Sin(3 * Math.PI / 4);

        for (var i = 0; i < iterationsCount; i++)
        {
            if (random.Next(2) == 0)
            {
                // Преобразование 1: поворот на 45° и сжатие
                var newX = (x * cos45 - y * sin45) / sqrt2;
                var newY = (x * sin45 + y * cos45) / sqrt2;
                x = newX;
                y = newY;
            }
            else
            {
                // Преобразование 2: поворот на 135°, сжатие, сдвиг по X на 1
                var newX = (x * cos135 - y * sin135) / sqrt2 + 1;
                var newY = (x * sin135 + y * cos135) / sqrt2;
                x = newX;
                y = newY;
            }

            pixels.SetPixel(x, y);
        }
    }
}