namespace AdventOfCode.Year2022.Day09;

enum Direction
{
	Up,
	Down,
	Left,
	Right
}

static class DirectionHelpers
{
	public static Vector ToUnitVector(this Direction direction)
	{
		return direction switch
		{
			Direction.Up => new Vector(0, -1),
			Direction.Down => new Vector(0, 1),
			Direction.Left => new Vector(-1, 0),
			Direction.Right => new Vector(1, 0),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.")
		};
	}

	public static bool IsDefined(Direction direction)
	{
		return direction
			is Direction.Up
			or Direction.Down
			or Direction.Left
			or Direction.Right;
	}

	public static Direction FromChar(char c)
	{
		return char.ToUpper(c) switch
		{
			'U' => Direction.Up,
			'D' => Direction.Down,
			'L' => Direction.Left,
			'R' => Direction.Right,
			_ => throw new ArgumentOutOfRangeException(nameof(c), c, "Invalid direction.")
		};
	}
}
