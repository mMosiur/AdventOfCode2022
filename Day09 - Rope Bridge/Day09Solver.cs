using AdventOfCode.Abstractions;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Year2022.Day09;

public sealed class Day09Solver : DaySolver
{
	private readonly (char, int)[] _instructions;

	public override int Year => 2022;
	public override int Day => 9;
	public override string Title => "Rope Bridge";

	public Day09Solver(Day09SolverOptions options) : base(options)
	{
		_instructions = InputLines.Select(line => (line[0], int.Parse(line[1..]))).ToArray();
	}

	public Day09Solver(Action<Day09SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day09Solver() : this(new Day09SolverOptions())
	{
	}

	private Vector2D<int> CharToVec(char c)
	{
		return c switch
		{
			'U' => new(-1, 0),
			'R' => new(0, 1),
			'D' => new(1, 0),
			'L' => new(0, -1),
			_ => throw new InvalidOperationException($"Invalid direction: {c}")
		};
	}

	private int CustomDist(Point2D<int> a, Point2D<int> b)
	{
		return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
	}

	private Point2D<int> MoveTailTowards(Point2D<int> tail, Point2D<int> head)
	{
		if (CustomDist(tail, head) < 2)
		{
			return tail;
		}
		if (tail.X == head.X)
		{
			if (tail.Y < (head.Y + 1))
			{
				return new(tail.X, tail.Y + 1);
			}
			else
			{
				return new(tail.X, tail.Y - 1);
			}
		}
		else if (tail.Y == head.Y)
		{
			if (tail.X < (head.X + 1))
			{
				return new(tail.X + 1, tail.Y);
			}
			else
			{
				return new(tail.X - 1, tail.Y);
			}
		}
		else
		{
			if (head.X > tail.X && head.Y > tail.Y)
			{
				return new(tail.X + 1, tail.Y + 1);
			}
			else if (head.X > tail.X && head.Y < tail.Y)
			{
				return new(tail.X + 1, tail.Y - 1);
			}
			else if (head.X < tail.X && head.Y > tail.Y)
			{
				return new(tail.X - 1, tail.Y + 1);
			}
			else if (head.X < tail.X && head.Y < tail.Y)
			{
				return new(tail.X - 1, tail.Y - 1);
			}
		}
		throw new Exception();
	}

	public override string SolvePart1()
	{
		Point2D<int> head = new(0, 0);
		Point2D<int> tail = new(0, 0);
		HashSet<Point2D<int>> visited = new() { tail };
		foreach ((char dir, int dist) in _instructions)
		{
			Vector2D<int> vec = CharToVec(dir);
			for (int i = 0; i < dist; i++)
			{
				head += vec;
				tail = MoveTailTowards(tail, head);
				visited.Add(tail);
			}
		}
		return $"{visited.Count}";
	}

	public override string SolvePart2()
	{
		Point2D<int>[] points = Enumerable.Repeat(0, 10).Select(z => new Point2D<int>(z, z)).ToArray();
		HashSet<Point2D<int>> visited = new() { points[^1] };
		foreach ((char dir, int dist) in _instructions)
		{
			Vector2D<int> vec = CharToVec(dir);
			for (int i = 0; i < dist; i++)
			{
				points[0] += vec;
				for (int j = 1; j < points.Length; j++)
				{
					points[j] = MoveTailTowards(points[j], points[j - 1]);
				}
				visited.Add(points[^1]);
			}
		}
		return $"{visited.Count}";
	}
}
