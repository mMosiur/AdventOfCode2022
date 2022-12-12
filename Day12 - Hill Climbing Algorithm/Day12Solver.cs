using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day12;

public sealed class Day12Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 12;
	public override string Title => "Hill Climbing Algorithm";

	private readonly int[,] _map;
	private readonly Point _start;
	private readonly Point _end;

	public Day12Solver(Day12SolverOptions options) : base(options)
	{
		InputParser parser = new('S', 'E');
		(_map, _start, _end) = parser.Parse(InputLines);
	}

	public Day12Solver(Action<Day12SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day12Solver() : this(new Day12SolverOptions())
	{
	}

	private int? ShortestPathForStartingPoint(Point start)
	{
		Dictionary<Point, int> visited = new();
		PriorityQueue<Point, int> queue = new();
		queue.Enqueue(start, 0);
		while (queue.TryDequeue(out Point point, out int distance))
		{
			if (point == _end)
				return distance;
			if (visited.TryGetValue(point, out int previousDistance) && previousDistance <= distance)
			{
				continue;
			}
			visited[point] = distance;
			foreach (Point newPoint in point.GetAdjacentPoints(_map.GetLength(0) - 1, _map.GetLength(1) - 1))
			{
				int diff = _map[newPoint.X, newPoint.Y] - _map[point.X, point.Y];
				if (diff > 1)
				{
					continue;
				}
				queue.Enqueue(newPoint, distance + 1);
			}
		}
		return null;
	}

	public override string SolvePart1()
	{
		int result = ShortestPathForStartingPoint(_start) ?? throw new InvalidOperationException("No path found.");
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int min = int.MaxValue;
		for (int x = 0; x < _map.GetLength(0); x++)
		{
			for (int y = 0; y < _map.GetLength(1); y++)
			{
				if (_map[x, y] == 0)
				{
					Point point = new(x, y);
					int? distance = ShortestPathForStartingPoint(point);
					if (distance.HasValue && distance < min)
					{
						min = distance.Value;
					}
				}
			}
		}
		if (min == int.MaxValue)
		{
			throw new InvalidOperationException("No path found.");
		}
		return $"{min}";
	}
}
