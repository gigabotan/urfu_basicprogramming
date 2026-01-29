using DynamicData.Diagnostics;
using System;
using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        var bestOrder = MakeTrivialPermutation(checkpoints.Length);
        var bestDist = checkpoints.GetPathLength(bestOrder);

        Console.WriteLine("---------");
        (bestDist, bestOrder) = CalculatePermutations(checkpoints, new int[checkpoints.Length], 1, bestDist, 0.0);
        Console.WriteLine(string.Join(", ", bestOrder));

        return bestOrder;
    }

    private static int[] MakeTrivialPermutation(int size)
    {
        var bestOrder = new int[size];
        for (var i = 0; i < bestOrder.Length; i++)
            bestOrder[i] = i;
        return bestOrder;
    }

    private static (double,int[]) CalculatePermutations(Point[] checkpoints, int[] permutation, int position, double bestDistance, double currentDistance)
    {

        if (position == permutation.Length)
        {
            return (bestDistance, permutation);
        }

        var bestOrder = permutation;
        for (var i = 1; i < permutation.Length; i++)
        {

            bool found = false;
            for (var j = 0; j < position; j++)
            {
                if (permutation[j] == i)
                {
                    found = true;
                    break;
                }
            }
            if (found) continue;

            Console.WriteLine("Current order " + string.Join(", ", bestOrder));

            currentDistance += checkpoints[permutation[position - 1]].DistanceTo(checkpoints[permutation[position]]);
            if (currentDistance > bestDistance)
                continue;

            permutation[position] = i;

            var (distance, order) = CalculatePermutations(checkpoints, permutation, position+1, bestDistance, currentDistance);
            if (distance <  bestDistance)
            {
                bestDistance = distance;
                bestOrder = order;
            }
        }

        return (currentDistance,permutation);
    }

}
