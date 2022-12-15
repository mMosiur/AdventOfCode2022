using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Year2022.Day15;

public sealed class Day15Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 15;
	public override string Title => "Beacon Exclusion Zone";

	private readonly Day15SolverOptions _options;
	private readonly IReadOnlyList<SensorInfo> _sensors;

	public Day15Solver(Day15SolverOptions options) : base(options)
	{
		_options = options;
		_sensors = InputLines.Select(SensorInfo.Parse).ToList();
	}

	public Day15Solver(Action<Day15SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day15Solver() : this(new Day15SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int targetRowY = _options.Part1RowY;
		MultiRange impossibleBeaconXs = new();
		foreach (SensorInfo sensor in _sensors)
		{
			int beaconDistance = MathG.ManhattanDistance(sensor.Location, sensor.ClosestBeaconLocation);
			int targetRowDistance = Math.Abs(sensor.Location.Y - targetRowY);
			int diff = beaconDistance - targetRowDistance;
			if (diff >= 0)
			{
				Range range = new(sensor.Location.X - diff, sensor.Location.X + diff);
				impossibleBeaconXs.Add(range);
				if (sensor.ClosestBeaconLocation.Y == targetRowY)
				{
					impossibleBeaconXs.Remove(sensor.ClosestBeaconLocation.Y);
				}
			}
		}
		int result = impossibleBeaconXs.Count;
		return $"{result}";
	}

	private long CalculateTuningFrequency(Point point)
	{
		return (point.X * _options.TuningFrequencyXMultiplier) + point.Y;
	}

	public override string SolvePart2()
	{
		Area area = new()
		{
			XRange = new(_options.Part2MinCoordinates, _options.Part2MaxCoordinates),
			YRange = new(_options.Part2MinCoordinates, _options.Part2MaxCoordinates)
		};
		Dictionary<Diagonal, int> diagCounts = new();
		foreach (SensorInfo sensor in _sensors)
		{
			int distance = MathG.ManhattanDistance(sensor.Location, sensor.ClosestBeaconLocation) + 1;
			foreach (Diagonal diag in sensor.Location.GetDiagonalsAtDistance(distance))
			{
				diagCounts[diag] = diagCounts.GetValueOrDefault(diag) + 1;
			}
		}
		List<Diagonal> diagonalPlus = new();
		List<Diagonal> diagonalMinus = new();
		foreach (Diagonal diagonal in diagCounts.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key))
		{
			if (diagonal.Dir is Diagonal.Direction.XPlusY)
			{
				diagonalPlus.Add(diagonal);
			}
			else if (diagonal.Dir is Diagonal.Direction.XMinusY)
			{
				diagonalMinus.Add(diagonal);
			}
			else throw new UnreachableException();
		};
		Point p = diagonalMinus.SelectMany(a => diagonalPlus.Select(b => Diagonal.Intersect(a, b)))
			.Where(area.Contains)
			.Where(p => !_sensors.Any(s => s.CanReach(p)))
			.Single();
		long result = CalculateTuningFrequency(p);
		return $"{result}";
	}
}

static class Extensions
{
	public static bool CanReach(this SensorInfo sensor, Point point)
	{
		int beaconDistance = MathG.ManhattanDistance(sensor.Location, sensor.ClosestBeaconLocation);
		int distance = MathG.ManhattanDistance(sensor.Location, point);
		return distance <= beaconDistance;
	}
	public static IEnumerable<Diagonal> GetDiagonalsAtDistance(this Point point, int distance)
	{
		int cMinus = point.Y - point.X;
		int cPlus = -point.Y - point.X;
		yield return new(Diagonal.Direction.XMinusY, cMinus - distance);
		yield return new(Diagonal.Direction.XPlusY, cPlus - distance);
		yield return new(Diagonal.Direction.XMinusY, cMinus + distance);
		yield return new(Diagonal.Direction.XPlusY, cPlus + distance);
	}
}

readonly struct Diagonal : IEquatable<Diagonal>
{
	public Direction Dir { get; }
	public int C { get; }

	public Diagonal(Direction dir, int c)
	{
		Dir = dir;
		C = c;
	}

	public static Point Intersect(Diagonal a, Diagonal b)
	{
		if (a.Dir == b.Dir)
		{
			throw new ArgumentException("Diagonals must be different directions");
		}
		int c1, c2;
		if (a.Dir == Direction.XMinusY)
		{
			c1 = a.C;
			c2 = b.C;
		}
		else if (a.Dir == Direction.XPlusY)
		{
			c1 = b.C;
			c2 = a.C;
		}
		else throw new UnreachableException();
		int x = -(c1 + c2) / 2;
		int y = (c1 - c2) / 2;
		return new(x, y);
	}

	public bool Equals(Diagonal other) => Dir == other.Dir && C == other.C;
	public override bool Equals(object? obj)
	{
		return obj is Diagonal diagonal && Equals(diagonal);
	}
	public override int GetHashCode() => HashCode.Combine(Dir, C);
	public static bool operator ==(Diagonal left, Diagonal right) => left.Equals(right);
	public static bool operator !=(Diagonal left, Diagonal right) => !(left == right);

	public enum Direction
	{
		XMinusY, // x - y + C = 0
		XPlusY // x + y + C = 0
	}
}
