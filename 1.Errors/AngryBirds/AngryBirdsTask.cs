using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double v, double distance)
    {
        // Projectile motion for target at same height:
        // distance = v^2 * sin(2*theta) / g
        // => sin(2*theta) = distance * g / v^2
        // We return the lower angle solution: theta = 0.5 * asin(distance * g / v^2)
        const double g = 9.8;
        // Validate inputs
        if (double.IsNaN(v) || double.IsNaN(distance) || double.IsInfinity(v) || double.IsInfinity(distance))
            return double.NaN;
        if (v <= 0) return double.NaN;
        if (distance == 0) return 0.0;

        double k = distance * g / (v * v);
        // Numerical clamping for safety
        if (k > 1.0 && k <= 1.0 + 1e-12) k = 1.0;
        if (k < -1.0 && k >= -1.0 - 1e-12) k = -1.0;
        // No solution if |k| > 1
        if (k > 1.0 || k < -1.0) return double.NaN;

        // Lower-angle solution
        double angle = 0.5 * Math.Asin(k);
        if (double.IsNaN(angle)) return double.NaN;
        return angle;
    }
}