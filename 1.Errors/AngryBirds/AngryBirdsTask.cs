using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double v, double distance)
    {
        const double g = 9.8;
        if (double.IsNaN(v)
            || double.IsNaN(distance)
            || double.IsInfinity(v)
            || double.IsInfinity(distance))
            return double.NaN;
        if (v <= 0) return double.NaN;
        if (distance == 0) return 0.0;

        var k = distance * g / (v * v);
        if (k > 1.0 || k < -1.0) return double.NaN;

        var angle = 0.5 * Math.Asin(k);
        if (double.IsNaN(angle)) return double.NaN;
        return angle;
    }
}
