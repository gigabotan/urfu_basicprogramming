using System;

namespace Rectangles;

public static class RectanglesTask
{
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        return !(r1.Right < r2.Left || r1.Left > r2.Right || r1.Bottom < r2.Top || r1.Top > r2.Bottom);
    }

    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        if (!AreIntersected(r1, r2))
            return 0;

        var intersectionLeft = Math.Max(r1.Left, r2.Left);
        var intersectionRight = Math.Min(r1.Right, r2.Right);
        var intersectionTop = Math.Max(r1.Top, r2.Top);
        var intersectionBottom = Math.Min(r1.Bottom, r2.Bottom);

        var intersectionWidth = intersectionRight - intersectionLeft;
        var intersectionHeight = intersectionBottom - intersectionTop;

        return intersectionWidth * intersectionHeight;
    }

    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        var r1InsideR2 = r1.Left >= r2.Left && r1.Right <= r2.Right &&
                         r1.Top >= r2.Top && r1.Bottom <= r2.Bottom;

        var r2InsideR1 = r2.Left >= r1.Left && r2.Right <= r1.Right &&
                         r2.Top >= r1.Top && r2.Bottom <= r1.Bottom;

        if (r1InsideR2)
            return 0;

        if (r2InsideR1)
            return 1;

        return -1;
    }
}
