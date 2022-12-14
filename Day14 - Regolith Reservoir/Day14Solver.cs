using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day14;

public sealed class Day14Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 14;
	public override string Title => "Regolith Reservoir";

	private readonly Point _sandOrigin;
	private readonly IReadOnlyList<Path> _rockPaths;

	public Day14Solver(Day14SolverOptions options) : base(options)
	{
		_sandOrigin = new Point(500, 0);
		_rockPaths = InputLines.Select(Path.Parse).ToList();
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
		Map map = Map.BuildFromRockPaths(_sandOrigin, _rockPaths);
		int count = 0;
		bool dropped = map.DropSand();
		while (dropped)
		{
			count++;
			dropped = map.DropSand();
		}
		return $"{count}";
	}

	public override string SolvePart2()
	{
		Map map = Map.BuildFromRockPaths(_sandOrigin, _rockPaths, floorOffset: 2);
		int count = 0;
		bool dropped = map.DropSand();
		while (dropped)
		{
			count++;
			dropped = map.DropSand();
		}
		return $"{count}";
	}
}
