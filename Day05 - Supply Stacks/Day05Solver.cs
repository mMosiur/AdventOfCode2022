using AdventOfCode.Abstractions;
using AdventOfCode.Year2022.Day05.Ship;
using AdventOfCode.Year2022.Day05.Ship.Cranes;

namespace AdventOfCode.Year2022.Day05;

public sealed class Day05Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 5;
	public override string Title => "Supply Stacks";

	private readonly CrateStacks _crateStacks;
	private readonly RearrangementProcedure _rearrangementProcedures;

	public Day05Solver(Day05SolverOptions options) : base(options)
	{
		(_crateStacks, _rearrangementProcedures) = InputParser.Parse(InputLines);
	}

	public Day05Solver(Action<Day05SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day05Solver() : this(new Day05SolverOptions())
	{
	}

	private string SolveUsingCrane(CargoCrane crane)
	{
		CrateStacks crateStacks = _crateStacks.Clone();
		crane.OperateOn(crateStacks);
		crane.Rearrange(_rearrangementProcedures);
		IEnumerable<char> topCrateMarks = crateStacks.Select(s => s.Peek().Mark);
		return string.Concat(topCrateMarks);
	}

	public override string SolvePart1()
	{
		CargoCrane crane = new CrateMover9000();
		return SolveUsingCrane(crane);
	}

	public override string SolvePart2()
	{
		CargoCrane crane = new CrateMover9001();
		return SolveUsingCrane(crane);
	}
}
