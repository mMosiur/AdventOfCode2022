using AdventOfCode.Abstractions;
using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Year2022.Day13.Packets;

namespace AdventOfCode.Year2022.Day13;

public sealed class Day13Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 13;
	public override string Title => "Distress Signal";

	private readonly IReadOnlyList<PacketPair> _packetPairs;
	private readonly Day13SolverOptions _options;

	public Day13Solver(Day13SolverOptions options) : base(options)
	{
		_packetPairs = InputParser.Parse(InputLines);
		_options = options;
	}

	public Day13Solver(Action<Day13SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day13Solver() : this(new Day13SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int result = _packetPairs
			.WithIndex()
			.Where((v) => PacketAnalyzer.IsInCorrectOrder(v.Item))
			.Sum(v => v.Index + 1); // Indices start at 1 in this puzzle
		return $"{result}";
	}

	public override string SolvePart2()
	{
		IReadOnlyList<Packet> dividerPackets = _options.DividerPackets
			.Select(Packet.Parse).ToList();
		PacketAnalyzer analyzer = new(_packetPairs);
		int decoderKey = analyzer.GetDecoderKey(dividerPackets);
		return $"{decoderKey}";
	}
}
