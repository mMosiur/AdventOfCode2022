using System.Diagnostics;

namespace AdventOfCode.Year2022.Day15;

sealed class BeaconExclusionZoneAnalyzer
{
	public IReadOnlyList<SensorInfo> Sensors { get; }

	public BeaconExclusionZoneAnalyzer(IEnumerable<SensorInfo> sensors)
	{
		ArgumentNullException.ThrowIfNull(sensors);
		Sensors = sensors.ToList();
	}

	public IEnumerable<Point> FindExcludedPositionsInRow(int y)
	{
		MultiRange beaconExcludedXs = new();
		foreach (SensorInfo sensor in Sensors)
		{
			int yDistance = Math.Abs(sensor.Location.Y - y);
			int diff = sensor.ClosestBeaconDistance - yDistance;
			if (diff >= 0)
			{
				Range range = new(sensor.Location.X - diff, sensor.Location.X + diff);
				beaconExcludedXs.Add(range);
				if (sensor.ClosestBeaconLocation.Y == y)
				{
					beaconExcludedXs.Remove(sensor.ClosestBeaconLocation.Y);
				}
			}
		}
		return beaconExcludedXs.Select(x => new Point(x, y));
	}

	private static bool CanSensorReachPoint(SensorInfo sensor, Point point)
	{
		return sensor.DistanceTo(point) <= sensor.ClosestBeaconDistance;
	}

	private static IEnumerable<Diagonal> GetDiagonalsAtDistance(Point point, int distance)
	{
		int cMinus = point.Y - point.X;
		int cPlus = -point.Y - point.X;
		yield return new(Diagonal.Direction.XMinusY, cMinus - distance);
		yield return new(Diagonal.Direction.XPlusY, cPlus - distance);
		yield return new(Diagonal.Direction.XMinusY, cMinus + distance);
		yield return new(Diagonal.Direction.XPlusY, cPlus + distance);
	}

	public Point FindDistressBeaconPositionIn(Area searchZone)
	{
		Dictionary<Diagonal, int> diagonals = new();
		foreach (SensorInfo sensor in Sensors)
		{
			int distanceOutsideReachZone = sensor.ClosestBeaconDistance + 1;
			foreach (Diagonal diagonal in GetDiagonalsAtDistance(sensor.Location, distanceOutsideReachZone))
			{
				diagonals.TryGetValue(diagonal, out int count);
				diagonals[diagonal] = count + 1;
			}
		}
		IEnumerable<Diagonal> diagonalsWithOverlap = diagonals.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key);
		List<Diagonal> diagonalsPlus = new();
		List<Diagonal> diagonalsMinus = new();
		foreach (Diagonal diagonal in diagonalsWithOverlap)
		{
			List<Diagonal> diagonalsGroup = diagonal.Orientation switch
			{
				Diagonal.Direction.XPlusY => diagonalsPlus,
				Diagonal.Direction.XMinusY => diagonalsMinus,
				_ => throw new UnreachableException()
			};
			diagonalsGroup.Add(diagonal);
		};
		IEnumerable<Point> intersections = diagonalsMinus.SelectMany(a => diagonalsPlus.Select(b => Diagonal.Intersect(a, b)));
		IEnumerable<Point> inSearchZone = intersections.Where(searchZone.Contains);
		IEnumerable<Point> outsideReachOfAllSensors = inSearchZone.Where(p => !Sensors.Any(s => CanSensorReachPoint(s, p)));
		try
		{
			return outsideReachOfAllSensors.Single();
		}
		catch (InvalidOperationException e)
		{
			throw new DaySolverException("No single distress beacon position found.", e);
		}
	}
}
