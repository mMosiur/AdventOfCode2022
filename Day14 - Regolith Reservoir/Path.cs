using System.Collections;

namespace AdventOfCode.Year2022.Day14;

sealed class WaypointPath : IEnumerable<Point>
{
	private readonly List<Point> _waypoints;

	public IReadOnlyList<Point> Waypoints => _waypoints;

	public WaypointPath(IEnumerable<Point> waypoints)
	{
		_waypoints = waypoints.ToList();
		if (_waypoints.Count < 2)
		{
			throw new ArgumentException("Path must have at least two waypoints.", nameof(waypoints));
		}
		for (int i = 1; i < _waypoints.Count; i++)
		{
			Point p1 = _waypoints[i - 1];
			Point p2 = _waypoints[i];
			if (p1.X != p2.X && p1.Y != p2.Y)
			{
				throw new ArgumentException($"Any two path waypoints must be aligned in a straight line (point {p1} at index {i - 1} and {p2} at {i} were not).", nameof(waypoints));
			}
		}
	}

	public static WaypointPath Parse(string s)
	{
		IEnumerable<Point> waypoints = s.Split("->", StringSplitOptions.TrimEntries)
			.Select(s => Point.Parse(s));
		return new WaypointPath(waypoints);
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
		yield return current; // yield the last waypoint
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
