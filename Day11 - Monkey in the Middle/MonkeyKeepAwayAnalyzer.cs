namespace AdventOfCode.Year2022.Day11;

class MonkeyKeepAwayAnalyzer
{
	private readonly IReadOnlyList<Monkey> _monkeys;

	public uint? NoDamageWorryLevelDivisor { get; }
	public bool UseModuloWorryLevels { get; }

	public MonkeyKeepAwayAnalyzer(IReadOnlyList<Monkey> monkeys, uint? noDamageWorryLevelDivisor = null, bool useModuloWorryLevels = false)
	{
		_monkeys = monkeys;
		NoDamageWorryLevelDivisor = noDamageWorryLevelDivisor;
		UseModuloWorryLevels = useModuloWorryLevels;
	}

	private void SimulateRound(List<Monkey> monkeys, IList<ulong>? inspectionCounts = null)
	{
		if (inspectionCounts is not null && inspectionCounts.Count != monkeys.Count)
		{
			throw new ArgumentException("Inspection counts must be null or have the same number of elements as monkeys.", nameof(inspectionCounts));
		}
		foreach (Monkey monkey in monkeys)
		{
			foreach (Item item in monkey.Items)
			{
				item.WorryLevel = monkey.Operation.Invoke(item.WorryLevel);
				if (NoDamageWorryLevelDivisor.HasValue)
				{
					item.WorryLevel /= NoDamageWorryLevelDivisor.Value;
				}
				bool testResult = item.WorryLevel % monkey.TestDivisor == 0;
				int targetMonkey = testResult ? monkey.TestIfTrue : monkey.TestIfFalse;
				monkeys[targetMonkey].Items.Add(item);
			}
			if (inspectionCounts is not null)
			{
				inspectionCounts[monkey.Index] += (ulong)monkey.Items.Count;
			}
			monkey.Items.Clear();
		}
	}

	/// <summary>
	/// Returns the number of items each monkey has expected.
	/// </summary>
	public List<ulong> SimulateRounds(ushort roundCount)
	{
		List<ulong> counts = Enumerable.Repeat(0UL, _monkeys.Count).ToList();
		List<Monkey> monkeys;
		if (UseModuloWorryLevels)
		{
			// We can safely mod all operations on the product of all worry levels.
			uint mod = _monkeys.Select(m => m.TestDivisor).Select(i => (uint)i).Aggregate((agg, next) => agg * next);
			monkeys = _monkeys.Select(m => m.Clone(i => new ItemModulo(i, mod))).ToList();
		}
		else
		{
			monkeys = _monkeys.Select(m => m.Clone()).ToList();
		}
		for (int i = 0; i < roundCount; i++)
		{
			SimulateRound(monkeys, counts);
		}
		return counts;
	}
}
