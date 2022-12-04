using AdventOfCode.Abstractions;
using AdventOfCode.Common.Numerics;

namespace AdventOfCode.Year2022.Day04;

public sealed class Day04Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 4;
	public override string Title => "Camp Cleanup";

	private readonly IReadOnlyList<(Interval<int>, Interval<int>)> _ranges;

	public Day04Solver(Day04SolverOptions options) : base(options)
	{
		_ranges = InputLines.Select(s =>
		{
			var parts = s.Split(',');
			return (
				new Interval<int>(int.Parse(parts[0].Split('-')[0]), int.Parse(parts[0].Split('-')[1])),
				new Interval<int>(int.Parse(parts[1].Split('-')[0]), int.Parse(parts[1].Split('-')[1]))
			);
		}).ToList();
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
		int result = _ranges.Count(r =>
		{
			return r.Item1.Contains(r.Item2) || r.Item2.Contains(r.Item1);
		});
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int result = _ranges.Count(r =>
		{
			return r.Item1.Overlaps(r.Item2) || r.Item2.Overlaps(r.Item1);
		});
		return $"{result}";
	}
}
