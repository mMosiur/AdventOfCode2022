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
		for (int y = _options.Part2MinCoordinates; y <= _options.Part2MaxCoordinates; y++)
		{
			MultiRange possibleBeaconXs = new() { new Range(_options.Part2MinCoordinates, _options.Part2MaxCoordinates) };
			foreach (SensorInfo sensor in _sensors)
			{
				int beaconDistance = MathG.ManhattanDistance(sensor.Location, sensor.ClosestBeaconLocation);
				int targetRowDistance = Math.Abs(sensor.Location.Y - y);
				int diff = beaconDistance - targetRowDistance;
				if (diff >= 0)
				{
					Range range = new(sensor.Location.X - diff, sensor.Location.X + diff);
					possibleBeaconXs.Remove(range);
					if (!possibleBeaconXs.Any())
					{
						break;
					}
				}
			}
			if (possibleBeaconXs.Any())
			{
				int x = possibleBeaconXs.Single();
				Point point = new(x, y);
				long result = CalculateTuningFrequency(point);
				return $"{result}";
			}
		}
		throw new DaySolverException("No solution found");
	}
}
