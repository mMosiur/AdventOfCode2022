using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day15;

public sealed class Day15SolverOptions : DaySolverOptions
{
	public int Part1RowY { get; set; } = 2_000_000;

	public long TuningFrequencyXMultiplier { get; set; } = 4_000_000;
	public int Part2MinCoordinates { get; set; } = 0;
	public int Part2MaxCoordinates { get; set; } = 4_000_000;
}
