using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day04;

public sealed class Day04Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 4;
	public override string Title => "Camp Cleanup";

	private readonly CampCleaningAssignments _cleaningAssignments;

	public Day04Solver(Day04SolverOptions options) : base(options)
	{
		try
		{
			_cleaningAssignments = CampCleaningAssignments.Parse(InputLines);
		}
		catch (FormatException e)
		{
			throw new InputException("Invalid input.", e);
		}
	}

	public Day04Solver(Action<Day04SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day04Solver() : this(new Day04SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int result = _cleaningAssignments.Comparisons
			.Count(r => r.Item1.Contains(r.Item2) || r.Item2.Contains(r.Item1));
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int result = _cleaningAssignments.Comparisons
			.Count(r => r.Item1.Overlaps(r.Item2) || r.Item2.Overlaps(r.Item1));
		return $"{result}";
	}
}
