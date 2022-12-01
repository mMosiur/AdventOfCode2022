using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day01;

public sealed class Day01Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 1;
	public override string Title => "Calorie Counting";

	private readonly Day01SolverOptions _options;
	private readonly IReadOnlyList<int> _caloriesCarriedByElves;

	public Day01Solver(Day01SolverOptions options) : base(options)
	{
		try
		{
			if (options.PartTwoTopCaloriesElvesCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(_options), options.PartTwoTopCaloriesElvesCount, "The number of elves carrying the most calories must be greater than 0.");
			}
			_options = options;
			_caloriesCarriedByElves = GenerateElvesCalorieCounts();
		}
		catch (Exception e)
		{
			throw new DaySolverException("Unable to generate elves calorie counts", e);
		}
	}

	public Day01Solver(Action<Day01SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day01Solver() : this(new Day01SolverOptions())
	{
	}

	private List<int> GenerateElvesCalorieCounts()
	{
		List<int> caloriesCarriedByElves = new();
		int? calories = null;
		foreach (string line in InputLines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (calories is not null)
				{
					caloriesCarriedByElves.Add(calories.Value);
					calories = null;
				}
				continue;
			}
			calories ??= 0;
			calories += int.Parse(line);
		}
		if (calories is not null)
		{
			caloriesCarriedByElves.Add(calories.Value);
		}
		return caloriesCarriedByElves;
	}

	public override string SolvePart1()
	{
		int result = _caloriesCarriedByElves.Max();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		Comparer<int> descendingComparer = Comparer<int>.Create((int x, int y) => y.CompareTo(x));
		IOrderedEnumerable<int> descendingCaloriesCounts = _caloriesCarriedByElves.Order(descendingComparer);
		int result = descendingCaloriesCounts.Take(_options.PartTwoTopCaloriesElvesCount).Sum();
		return $"{result}";
	}
}
