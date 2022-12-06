using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day06;

public sealed class Day06Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 6;
	public override string Title => "Tuning Trouble";

	public Day06Solver(Day06SolverOptions options) : base(options)
	{
	}

	public Day06Solver(Action<Day06SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day06Solver() : this(new Day06SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		string input = Input.Trim();
		LinkedList<char> list = new(input.Take(4));
		int x = 4;
		while (list.Distinct().Count() != 4)
		{
			list.AddLast(input[x]);
			list.RemoveFirst();
			x++;
		}
		return $"{x}";
	}

	public override string SolvePart2()
	{
		string input = Input.Trim();
		LinkedList<char> list = new(input.Take(14));
		int x = 14;
		while (list.Distinct().Count() != 14)
		{
			list.AddLast(input[x]);
			list.RemoveFirst();
			x++;
		}
		return $"{x}";
	}
}
