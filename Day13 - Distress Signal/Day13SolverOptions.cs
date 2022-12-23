using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day13;

public sealed class Day13SolverOptions : DaySolverOptions
{
	public string[] DividerPackets { get; set; } = new string[2]
	{
		"[[2]]",
		"[[6]]"
	};
}
