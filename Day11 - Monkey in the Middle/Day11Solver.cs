using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day11;

public sealed class Day11Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 11;
	public override string Title => "Monkey in the Middle";

	private readonly Day11SolverOptions _options;
	private readonly IReadOnlyList<Monkey> _monkeys;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		_options = options;
		_monkeys = MonkeyInfoParser.Parse(InputLines);
	}

	public Day11Solver(Action<Day11SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day11Solver() : this(new Day11SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		MonkeyKeepAwayAnalyzer analyzer = new(
			_monkeys,
			noDamageWorryLevelDivisor: _options.Part1NoDamageWorryLevelDivisor
		);
		List<ulong> counts = analyzer.SimulateRounds(_options.Part1RoundCount);
		counts.Sort();
		ulong monkeyBusiness = counts[^1] * counts[^2];
		return $"{monkeyBusiness}";
	}

	public override string SolvePart2()
	{
		MonkeyKeepAwayAnalyzer analyzer = new(
			_monkeys,
			useModuloWorryLevels: true
		);
		List<ulong> counts = analyzer.SimulateRounds(_options.Part2RoundCount);
		counts.Sort();
		ulong monkeyBusiness = counts[^1] * counts[^2];
		return $"{monkeyBusiness}";
	}
}
