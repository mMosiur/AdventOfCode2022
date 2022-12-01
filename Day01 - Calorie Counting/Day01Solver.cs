using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day01;

public sealed class Day01Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 1;
	public override string Title => "Calorie Counting";

	public IEnumerable<int> InputNumbers => InputLines.Select(int.Parse);

	public Day01Solver(Day01SolverOptions options) : base(options)
	{
		// Initialize Day01 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day01Solver(Action<Day01SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day01Solver() : this(new Day01SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int max = int.MinValue;
		int? currentElf = null;

		foreach (string line in InputLines.Append(""))
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (currentElf is not null)
				{
					max = Math.Max(max, currentElf.Value);
					currentElf = null;
				}
				continue;
			}
			currentElf ??= 0;
			int calories = int.Parse(line);
			currentElf += calories;
		}
		return $"{max}";
	}

	public override string SolvePart2()
	{
		List<int> elves = new();
		int? currentElf = null;

		foreach (string line in InputLines.Append(""))
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (currentElf is not null)
				{
					elves.Add(currentElf.Value);
					currentElf = null;
				}
				continue;
			}
			currentElf ??= 0;
			int calories = int.Parse(line);
			currentElf += calories;
		}
		elves.Sort();
		int maxTopThree = elves.TakeLast(3).Sum();
		return $"{maxTopThree}";
	}
}
