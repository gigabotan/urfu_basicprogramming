namespace Mazes;

public static class EmptyMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var stepsRight = width - 3;
        var stepsDown = height - 3;
        MoveToDirection(robot, stepsRight, Direction.Right);
        MoveToDirection(robot, stepsDown, Direction.Down);
    }

    private static void MoveToDirection(Robot robot, int stepCount, Direction direction)
    {
        for (var i = 0; i < stepCount; i++)
            robot.MoveTo(direction);
    }
}
