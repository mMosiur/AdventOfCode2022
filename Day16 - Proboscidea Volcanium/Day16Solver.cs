using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day16;

public sealed class Day16Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 16;
	public override string Title => "Proboscidea Volcanium";

	public Day16SolverOptions Options { get; }
	private ValveMap ValveMap { get; }

	public Day16Solver(Day16SolverOptions options) : base(options)
	{
		Options = options;
		InputReader inputReader = new InputReader();
		var valves = inputReader.ReadInput(InputLines);
		ValveMap = new(valves, Options.StartingValveName);
	}

	public Day16Solver(Action<Day16SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day16Solver() : this(new Day16SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		ValveMap.Optimize();
		var traverser = new ValveMapTraverser(ValveMap, Options.TimeInMinutes);
		int maxPressure = traverser.Traverse();
		return $"{maxPressure}";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
