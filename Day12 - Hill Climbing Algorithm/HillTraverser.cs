namespace AdventOfCode.Year2022.Day12;

sealed class HillTraverser
{
	private readonly HillMap _map;
	private readonly byte? _maxDownwardReach;
	private readonly byte? _maxUpwardReach;

	public HillTraverser(HillMap map, byte? maxDownwardReach = null, byte? maxUpwardReach = null)
	{
		_map = map;
		_maxDownwardReach = maxDownwardReach;
		_maxUpwardReach = maxUpwardReach;
	}

	/// <summary>
	/// Returns the length of the shortest path between provided points
	/// or <see langword="null"/> if no such path exists.
	/// </summary>
	public int? FindShortestPathLength(Point start, Point end)
	{
		if (!_map.Area.Contains(start))
		{
			throw new ArgumentException($"Start location {start} is not within the map area.", nameof(start));
		}
		if (!_map.Area.Contains(end))
		{
			throw new ArgumentException($"End location {end} is not within the map area.", nameof(end));
		}
		if (start == end)
		{
			return 0;
		}
		Dictionary<Point, int> visited = new();
		Queue<(Point Location, int distance)> queue = new();
		queue.Enqueue((start, 0));
		while (queue.TryDequeue(out (Point, int) state))
		{
			(Point location, int distance) = state;
			if (location == end)
			{
				return distance;
			}
			if (visited.TryGetValue(location, out int previousDistance) && previousDistance <= distance)
			{
				continue;
			}
			visited[location] = distance;
			foreach (Point newPoint in _map.GetAdjacentPointsOf(location))
			{
				int heightDifference = _map[newPoint] - _map[location];
				if (_maxUpwardReach.HasValue && heightDifference > _maxUpwardReach)
				{
					continue;
				}
				if (_maxDownwardReach.HasValue && heightDifference < -_maxDownwardReach)
				{
					continue;
				}
				queue.Enqueue((newPoint, distance + 1));
			}
		}
		return null; // No path found
	}
}
