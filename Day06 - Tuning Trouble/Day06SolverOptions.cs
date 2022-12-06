using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day06;

public sealed class Day06SolverOptions : DaySolverOptions
{
	public int StartOfPacketMarkerSequenceLength { get; set; } = 4;
	public int StartOfMessageMarkerSequenceLength { get; set; } = 14;
}
