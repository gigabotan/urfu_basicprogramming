namespace Mazes;

public static class DiagonalMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var stepsRight = width - 3;
        var stepsDown = height - 3;
        var isRightFirst = stepsRight > stepsDown;
        var count = isRightFirst ? stepsDown : stepsRight;
        var step = isRightFirst ? stepsRight / count : stepsDown / count;

        for (var i = 0; i < count; i++)
        {
            Move(robot, step, isRightFirst ? Direction.Right : Direction.Down);
            Move(robot, 1, isRightFirst ? Direction.Down : Direction.Right);
        }
        Move(robot, step, isRightFirst ? Direction.Right : Direction.Down);
    }

    public static void Move(Robot robot, int count, Direction direction)
    {
        for (var i = 0; i < count; i++)
            robot.MoveTo(direction);
    }
}