namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var stepsRight = width - 3;
        var stepsDown = height - 3;
        var isRightFirst = stepsRight > stepsDown;
        var moveCount = isRightFirst ? stepsDown : stepsRight;
        var stepsPerMove = isRightFirst ? stepsRight / moveCount : stepsDown / moveCount;

        ExecuteDiagonalSteps(robot, moveCount, stepsPerMove, isRightFirst);
        MoveToDirection(robot, stepsPerMove, isRightFirst ? Direction.Right : Direction.Down);
    }

    private static void ExecuteDiagonalSteps(Robot robot, int moveCount, int stepsPerMove, bool isRightFirst)
    {
        for (var i = 0; i < moveCount; i++)
        {
            MoveToDirection(robot, stepsPerMove, isRightFirst ? Direction.Right : Direction.Down);
            MoveToDirection(robot, 1, isRightFirst ? Direction.Down : Direction.Right);
        }
    }

    public static void MoveToDirection(Robot robot, int count, Direction direction)
    {
        for (var i = 0; i < count; i++)
            robot.MoveTo(direction);
    }
}
