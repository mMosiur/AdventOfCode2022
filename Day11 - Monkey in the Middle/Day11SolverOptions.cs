using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day11;

public sealed class Day11SolverOptions : DaySolverOptions
{
	public uint Part1NoDamageWorryLevelDivisor { get; set; } = 3;
	public ushort Part1RoundCount { get; set; } = 20;
	public ushort Part2RoundCount { get; set; } = 10_000;
}
