namespace Recognizer;

public static class GrayscaleTask
{
    public static double[,] ToGrayscale(Pixel[,] original)
    {
        var width = original.GetLength(0);
        var height = original.GetLength(1);
        var result = new double[original.GetLength(0), original.GetLength(1)];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = original[x, y];
                result[x, y] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
            }
        }
        return result;
    }
}
