using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day18;

public sealed class Day18Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 18;
	public override string Title => "Boiling Boulders";

	private readonly IEnumerable<Position> _positions;

	public Day18Solver(Day18SolverOptions options) : base(options)
	{
		_positions = InputLines.Select(s => Position.Parse(s)).ToHashSet();
	}

	public Day18Solver(Action<Day18SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day18Solver() : this(new Day18SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int result = _positions.Sum(p1 =>
			p1.GetAdjacent().Count(p2 => !_positions.Contains(p2))
		);
		return $"{result}";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}

static class Extensions
{
	public static IEnumerable<Position> GetAdjacent(this Position position)
	{
		yield return position + new Vector(0, 1, 0);
		yield return position + new Vector(0, -1, 0);
		yield return position + new Vector(1, 0, 0);
		yield return position + new Vector(-1, 0, 0);
		yield return position + new Vector(0, 0, 1);
		yield return position + new Vector(0, 0, -1);
	}
}
