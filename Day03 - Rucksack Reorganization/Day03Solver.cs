using AdventOfCode.Abstractions;
using AdventOfCode.Year2022.Day03.Rucksacks;

namespace AdventOfCode.Year2022.Day03;

public sealed class Day03Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 3;
	public override string Title => "Rucksack Reorganization";

	private readonly Rucksack[] _rucksacks;

	public Day03Solver(Day03SolverOptions options) : base(options)
	{
		_rucksacks = InputLines.Select(Rucksack.Parse).ToArray();
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
		try
		{
			RucksackAnalyzer analyzer = new(_rucksacks);
			int result = analyzer.SumCommonItemTypesInRucksackPriorities();
			return $"{result}";
		}
		catch (Exception e)
		{
			throw new DaySolverException("Unable to solve part 1.", e);
		}
	}

	public override string SolvePart2()
	{
		// alphabet
		int count = 0;
		string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string[] lines = InputLines.ToArray();
		for (int i = 0; i < lines.Length; i += 3)
		{
			var v = lines[i].ToHashSet().Intersect(lines[i + 1].ToHashSet()).Intersect(lines[i + 2].ToHashSet()).Single();

			count += (alph.IndexOf(v) + 1);
		}
		return $"{count}";
	}
}
