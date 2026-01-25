namespace Recognizer;

public static class GrayscaleTask
{
    public static double[,] ToGrayscale(Pixel[,] original)
    {
        var rows = original.GetLength(0);
        var cols = original.GetLength(1);
        var result = new double[original.GetLength(0), original.GetLength(1)];
        for (var x = 0; x < rows; x++)
        {
            for (var y = 0; y < cols; y++)
            {
                result[x, y] = (0.299 * original[x, y].R + 0.587 * original[x, y].G + 0.114 * original[x, y].B) / 255;
            }
        }
        return result;
    }
}
