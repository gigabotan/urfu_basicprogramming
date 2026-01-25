using System;

namespace DistanceTask;

public static class DistanceTask
{
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        var segmentLengthSquared = GetSquaredDistance(ax, ay, bx, by);

        if (segmentLengthSquared == 0)
            return GetDistance(ax, ay, x, y);

        var projectionParameter = CalculateProjectionParameter(ax, ay, bx, by, x, y, segmentLengthSquared);

        if (projectionParameter <= 0)
            return GetDistance(ax, ay, x, y);

        if (projectionParameter >= 1)
            return GetDistance(bx, by, x, y);

        return GetDistanceToProjectedPoint(ax, ay, bx, by, x, y, projectionParameter);
    }

    private static double CalculateProjectionParameter(double ax, double ay, double bx, double by, double x, double y, double segmentLengthSquared)
    {
        var vectorToPointX = x - ax;
        var vectorToPointY = y - ay;
        var segmentVectorX = bx - ax;
        var segmentVectorY = by - ay;
        var dotProduct = vectorToPointX * segmentVectorX + vectorToPointY * segmentVectorY;
        return dotProduct / segmentLengthSquared;
    }

    private static double GetDistanceToProjectedPoint(double ax, double ay, double bx, double by, double x, double y, double projectionParameter)
    {
        var projectedX = ax + projectionParameter * (bx - ax);
        var projectedY = ay + projectionParameter * (by - ay);
        return GetDistance(projectedX, projectedY, x, y);
    }

    private static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(GetSquaredDistance(x1, y1, x2, y2));
    }

    private static double GetSquaredDistance(double x1, double y1, double x2, double y2)
    {
        var dx = x2 - x1;
        var dy = y2 - y1;
        return dx * dx + dy * dy;
    }
}
