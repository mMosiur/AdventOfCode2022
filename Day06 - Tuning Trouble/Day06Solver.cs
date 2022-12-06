using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day06;

public sealed class Day06Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 6;
	public override string Title => "Tuning Trouble";

	private readonly IReadOnlyDictionary<MarkerType, int> _markerSequenceLengths;

	public Day06Solver(Day06SolverOptions options) : base(options)
	{
		_markerSequenceLengths = new Dictionary<MarkerType, int>
		{
			[MarkerType.StartOfPacket] = options.StartOfPacketMarkerSequenceLength,
			[MarkerType.StartOfMessage] = options.StartOfMessageMarkerSequenceLength
		};
	}

	public Day06Solver(Action<Day06SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day06Solver() : this(new Day06SolverOptions())
	{
	}

	private int FindForMarkerType(MarkerType markerType)
	{
		SignalMarkerLocator locator = new(_markerSequenceLengths);
		using TextReader reader = new StringReader(Input.Trim());
		return locator.FindMarkerLocation(reader, markerType);
	}

	public override string SolvePart1()
	{
		int result = FindForMarkerType(MarkerType.StartOfPacket);
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int result = FindForMarkerType(MarkerType.StartOfMessage);
		return $"{result}";
	}
}
