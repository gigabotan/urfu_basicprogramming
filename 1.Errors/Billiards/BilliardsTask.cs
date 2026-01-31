using System;

namespace Billiards;

public static class BilliardsTask
{
    /// <summary>
    /// Вычисляет новое направление движения шара после отражения от стены.
    /// </summary>
    /// <param name="directionRadians">Угол направления движения шара</param>
    /// <param name="wallInclinationRadians">Угол наклона стены</param>
    /// <returns>Угол направления после отражения</returns>
    public static double BounceWall(double directionRadians, double wallInclinationRadians)
    {
        return 2 * wallInclinationRadians - directionRadians;
    }
}
