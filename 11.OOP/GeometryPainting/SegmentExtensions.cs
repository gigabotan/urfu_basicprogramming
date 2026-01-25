using System.Collections.Generic;
using Avalonia.Media;
using Geometry;

namespace Geometry;

public static class SegmentExtensions
{
    private static Dictionary<Segment, Color> segmentColors = new Dictionary<Segment, Color>();

    public static Color GetColor(this Segment segment)
    {
        if (segmentColors.TryGetValue(segment, out var color))
            return color;
        return Colors.Black;
    }

    public static void SetColor(this Segment segment, Color color)
    {
        segmentColors[segment] = color;
    }
}
