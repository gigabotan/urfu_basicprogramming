namespace Mazes;

public static class EmptyMazeTask
{
	public static void MoveToDirection(Robot robot, int steps, Direction direction)
	{
		for (var i = 0; i < steps; i++)
			robot.MoveTo(direction);
	}

	public static void MoveOut(Robot robot, int width, int height)
	{
		var stepsRight = width - 3;
		var stepsDown = height - 3;
		MoveToDirection(robot, stepsRight, Direction.Right);
		MoveToDirection(robot, stepsDown, Direction.Down);
	}
}