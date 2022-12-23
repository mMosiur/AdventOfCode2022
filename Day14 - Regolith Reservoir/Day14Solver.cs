using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day14;

public sealed class Day14Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 14;
	public override string Title => "Regolith Reservoir";

	private readonly Point _sandSource;
	private readonly IReadOnlyList<WaypointPath> _rockPaths;

	public Day14Solver(Day14SolverOptions options) : base(options)
	{
		_sandSource = new Point(options.SandSourceX, options.SandSourceY);
		_rockPaths = InputLines.Select(WaypointPath.Parse).ToList();
	}

	public Day14Solver(Action<Day14SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day14Solver() : this(new Day14SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		Map map = Map.BuildFromRockPaths(_sandSource, _rockPaths);
		int result = map.SimulateUntilAnOverflow();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		Map map = Map.BuildFromRockPaths(_sandSource, _rockPaths, floorOffset: 2);
		int result = map.SimulateUntilAnOverflow();
		return $"{result}";
	}
}
