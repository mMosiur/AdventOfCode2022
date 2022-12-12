using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day09;

public sealed class Day09Solver : DaySolver
{
	private readonly int _part1RopeLength;
	private readonly int _part2RopeLength;
	private readonly IReadOnlyList<Motion> _motions;

	public override int Year => 2022;
	public override int Day => 9;
	public override string Title => "Rope Bridge";

	public Day09Solver(Day09SolverOptions options) : base(options)
	{
		_part1RopeLength = options.Part1RopeLength;
		_part2RopeLength = options.Part2RopeLength;
		_motions = InputLines.Select(Motion.Parse).ToList();
	}

	public Day09Solver(Action<Day09SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day09Solver() : this(new Day09SolverOptions())
	{
	}

	private int CountUniqueTailPositions(int ropeLength)
	{
		Rope rope = new(ropeLength);
		RopeTailWatcher watcher = new(rope);
		watcher.MoveRope(_motions);
		return watcher.UniqueTailPositions.Count;
	}

	public override string SolvePart1()
	{
		int result = CountUniqueTailPositions(_part1RopeLength);
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int result = CountUniqueTailPositions(_part2RopeLength);
		return $"{result}";
	}
}
