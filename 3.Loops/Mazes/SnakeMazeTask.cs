namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        var stepsDown = (height - 3) / 2;
        var direction = Direction.Right;
        while (stepsDown-- > 0)
        {
            Move(robot, width - 3, direction);
            Move(robot, 2, Direction.Down);
            direction = direction == Direction.Right ? Direction.Left : Direction.Right;
        }
        Move(robot, width - 3, Direction.Left);
    }

    public static void Move(Robot robot, int steps, Direction direction)
    {
        for (var i = 0; i < steps; i++)
            robot.MoveTo(direction);
    }
}