using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly Forest _forest;

	public override int Year => 2022;
	public override int Day => 8;
	public override string Title => "Treetop Tree House";

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_forest = Forest.Parse(Input);
	}

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day08Solver() : this(new Day08SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		TreeHouseLocationAnalyzer analyzer = new(_forest);
		int result = analyzer.CountTreesVisibleFromOutside();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		TreeHouseLocationAnalyzer analyzer = new(_forest);
		(_, int result) = analyzer.FindBestTreeLocation();
		return $"{result}";
	}
}
