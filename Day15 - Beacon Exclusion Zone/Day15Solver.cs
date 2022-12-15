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
		HashSet<Point> excludedPoints = new();
		int targetRowY = _options.Part1RowY;
		foreach (SensorInfo sensor in _sensors)
		{
			int beaconDistance = MathG.ManhattanDistance(sensor.Location, sensor.ClosestBeaconLocation);
			int targetRowDistance = Math.Abs(sensor.Location.Y - targetRowY);
			int diff = beaconDistance - targetRowDistance;
			if (diff >= 0)
			{
				Range range = new(sensor.Location.X - diff, sensor.Location.X + diff);
				for (int x = range.Start; x <= range.End; x++)
				{
					excludedPoints.Add(new(x, targetRowY));
				}
				excludedPoints.Remove(sensor.ClosestBeaconLocation);
			}
		}
		int result = excludedPoints.Count;
		return $"{result}";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
