using System.Collections;

namespace AdventOfCode.Year2022.Day14;

class Path : IEnumerable<Point>
{
	private readonly List<Point> _waypoints;

	public IReadOnlyList<Point> Waypoints => _waypoints;

	public Path(IEnumerable<Point> waypoints)
	{
		_waypoints = waypoints.ToList();
		if (_waypoints.Count < 2)
		{
			throw new ArgumentException("Path must have at least two waypoints.", nameof(waypoints));
		}
	}

	public static Path Parse(string s)
	{
		Point[] waypoints = s.Split("->", StringSplitOptions.TrimEntries)
			.Select(s => Point.Parse(s))
			.ToArray();
		return new Path(waypoints);
	}

	public IEnumerator<Point> GetEnumerator()
	{
		Point current = _waypoints[0];
		foreach (Point nextWaypoint in _waypoints.Skip(1))
		{
			int xDir = Math.Sign(nextWaypoint.X - current.X);
			int yDir = Math.Sign(nextWaypoint.Y - current.Y);
			Vector step = new(xDir, yDir);
			while (current != nextWaypoint)
			{
				yield return current;
				current += step;
			}
		}
		yield return current;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
