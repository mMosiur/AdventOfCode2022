using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day15;

public sealed class Day15Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 15;
	public override string Title => "Beacon Exclusion Zone";

	private readonly Day15SolverOptions _options;
	private readonly BeaconExclusionZoneAnalyzer _exclusionZoneAnalyzer;
	private readonly BeaconAnalyzer _beaconAnalyzer;

	public Day15Solver(Day15SolverOptions options) : base(options)
	{
		_options = options;
		_exclusionZoneAnalyzer = new(InputLines.Select(SensorInfo.Parse));
		_beaconAnalyzer = new(options.TuningFrequencyXMultiplier);
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
		IEnumerable<Point> excludedPositions = _exclusionZoneAnalyzer
			.FindExcludedPositionsInRow(_options.Part1RowY);
		int result = excludedPositions.Count();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		Area searchZone = new()
		{
			XRange = new(_options.Part2MinCoordinates, _options.Part2MaxCoordinates),
			YRange = new(_options.Part2MinCoordinates, _options.Part2MaxCoordinates)
		};
		Point point = _exclusionZoneAnalyzer.FindDistressBeaconPositionIn(searchZone);
		long result = _beaconAnalyzer.CalculateTuningFrequencyAt(point);
		return $"{result}";
	}
}
