using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day12;

public sealed class Day12SolverOptions : DaySolverOptions
{
	public char CurrentLocationChar { get; set; } = 'S';
	public char BestSignalLocationChar { get; set; } = 'E';

	public byte? MaxDownwardReach { get; set; } = null;
	public byte? MaxUpwardReach { get; set; } = 1;

	public int Part2MinStartHeight { get; set; } = 0;
}
