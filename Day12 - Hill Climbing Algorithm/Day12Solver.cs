using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day12;

public sealed class Day12Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 12;
	public override string Title => "Hill Climbing Algorithm";

	private readonly HillMap _map;

	public Day12Solver(Day12SolverOptions options) : base(options)
	{
		InputParser parser = new(
			startChar: 'S',
			endChar: 'E'
		);
		_map = parser.Parse(InputLines);
	}

	public Day12Solver(Action<Day12SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day12Solver() : this(new Day12SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		HillTraverser traverser = new(_map);
		int result = traverser.FindShortestPathLength(
			start: _map.StartingLocation,
			end: _map.BestSignalLocation
		) ?? throw new DaySolverException("No path found.");
		return $"{result}";
	}

	public override string SolvePart2()
	{
		HillTraverser traverser = new(_map);
		int min = int.MaxValue;
		foreach (Point lowPoint in _map.Area.Points.Where(p => _map[p] == 0))
		{
			int distance = traverser.FindShortestPathLength(
				start: lowPoint,
				end: _map.BestSignalLocation
			) ?? int.MaxValue;
			min = Math.Min(min, distance);
		}
		if (min == int.MaxValue)
		{
			throw new DaySolverException("No path found.");
		}
		return $"{min}";
	}
}
