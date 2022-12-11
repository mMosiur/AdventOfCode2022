using AdventOfCode.Abstractions;
using AdventOfCode.Common.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day11;

public sealed class Day11Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 11;
	public override string Title => "Monkey in the Middle";

	private readonly List<Monkey> _monkeys;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		_monkeys = InputLines.SplitWithSeparator("").Select(Monkey.Parse).ToList();
	}

	public Day11Solver(Action<Day11SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day11Solver() : this(new Day11SolverOptions())
	{
	}

	private static void PlayRound1(List<Monkey> monkeys, List<int> counts)
	{
		foreach (Monkey monkey in monkeys)
		{
			while (monkey.StartingItems.Count > 0)
			{
				counts[monkey.Index]++;
				ulong item = monkey.StartingItems[0];
				monkey.StartingItems.RemoveAt(0);
				item = monkey.Operation.Apply(item);
				item /= 3;
				int targetMonkey = item % (ulong)monkey.TestDivisible == 0 ? monkey.TestIfTrue : monkey.TestIfFalse;
				monkeys[targetMonkey].StartingItems.Add(item);
			}
		}
	}

	public override string SolvePart1()
	{
		List<Monkey> monkeys = _monkeys.Select(m => m.Clone()).ToList();
		List<int> counts = Enumerable.Repeat(0, _monkeys.Count).ToList();
		for (int i = 0; i < 20; i++)
		{
			PlayRound1(monkeys, counts);
		}
		counts.Sort();
		int monkeyBusiness = counts[^1] * counts[^2];
		return $"{monkeyBusiness}";
	}

	private static void PlayRound2(List<Monkey> monkeys, List<ulong> counts, ulong mod)
	{
		foreach (Monkey monkey in monkeys)
		{
			while (monkey.StartingItems.Count > 0)
			{
				counts[monkey.Index]++;
				ulong item = monkey.StartingItems[0] % mod;
				monkey.StartingItems.RemoveAt(0);
				item = monkey.Operation.Apply(item, mod);
				int targetMonkey = item % (ulong)monkey.TestDivisible == 0 ? monkey.TestIfTrue : monkey.TestIfFalse;
				monkeys[targetMonkey].StartingItems.Add(item);
			}
		}
	}

	public override string SolvePart2()
	{
		List<Monkey> monkeys = _monkeys.Select(m => m.Clone()).ToList();
		List<ulong> counts = Enumerable.Repeat(0UL, _monkeys.Count).ToList();
		ulong mod = _monkeys.Select(m => m.TestDivisible).Select(i => (ulong)i).Aggregate((agg, next) => agg * next);
		for (int i = 0; i < 10000; i++)
		{
			PlayRound2(monkeys, counts, mod);
		}
		counts.Sort();
		ulong monkeyBusiness = counts[^1] * counts[^2];
		return $"{monkeyBusiness}";
	}
}
