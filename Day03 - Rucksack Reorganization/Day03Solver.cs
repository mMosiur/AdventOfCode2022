using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day03;

public sealed class Day03Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 3;
	public override string Title => "Rucksack Reorganization";

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		// Initialize Day03 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day03Solver(Action<Day03SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day03Solver() : this(new Day03SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		// alphabet
		int count = 0;
		string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		foreach (string line in InputLines)
		{
			int len = line.Length;
			string part1 = line[..(len / 2)];
			string part2 = line[(len / 2)..];
			var v = part1.ToHashSet().Intersect(part2.ToHashSet()).Single();
			count += (alph.IndexOf(v) + 1);
		}
		return $"{count}";
	}

	public override string SolvePart2()
	{
		// alphabet
		int count = 0;
		string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] lines = InputLines.ToArray();
		for (int i = 0; i < lines.Length; i+=3)
		{
			var v = lines[i].ToHashSet().Intersect(lines[i+1].ToHashSet()).Intersect(lines[i+2].ToHashSet()).Single();

			count += (alph.IndexOf(v) + 1);
		}
		return $"{count}";
	}
}
