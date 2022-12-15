using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day15;

public sealed class Day15SolverOptions : DaySolverOptions
{
	const bool EXAMPLE = false;

	public int Part1RowY { get; set; } = EXAMPLE ? 10 : 2_000_000;
}
