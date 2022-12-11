using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022.Day11;

class Monkey : ICloneable
{
	private static readonly Regex _indexRegex = new(@"^Monkey (\d+):$", RegexOptions.Compiled);
	private static readonly Regex _startingItemsRegex = new(@"^[ \t]*Starting items: ([\d, ]+)$", RegexOptions.Compiled);
	private static readonly Regex _operationRegex = new(@"^[ \t]*Operation: (.+)$", RegexOptions.Compiled);
	private static readonly Regex _testRegex = new(@"^[ \t]*Test: divisible by (\d+)$", RegexOptions.Compiled);
	private static readonly Regex _testIfTrueRegex = new(@"^[ \t]*If true: throw to monkey (\d+)$", RegexOptions.Compiled);
	private static readonly Regex _testIfFalseRegex = new(@"^[ \t]*If false: throw to monkey (\d+)$", RegexOptions.Compiled);

	public int Index { get; }
	public List<ulong> StartingItems { get; }
	public Operation Operation { get; }
	public int TestDivisible { get; }
	public int TestIfTrue { get; }
	public int TestIfFalse { get; }

	public Monkey(int index, List<ulong> startingItems, Operation operation, int testDivisible, int testIfTrue, int testIfFalse)
	{
		Index = index;
		StartingItems = startingItems;
		Operation = operation;
		TestDivisible = testDivisible;
		TestIfTrue = testIfTrue;
		TestIfFalse = testIfFalse;
	}

	public static Monkey Parse(IReadOnlyList<string> lines)
	{
		Match indexMatch = _indexRegex.Match(lines[0]);
		if (!indexMatch.Success) throw new FormatException();
		int index = int.Parse(indexMatch.Groups[1].ValueSpan);

		Match startingItemsMatch = _startingItemsRegex.Match(lines[1]);
		if (!startingItemsMatch.Success) throw new FormatException();
		List<ulong> startingItems = startingItemsMatch.Groups[1].Value.Split(", ").Select(ulong.Parse).ToList();

		Match operationMatch = _operationRegex.Match(lines[2]);
		if (!operationMatch.Success) throw new FormatException();
		Operation operation = Operation.Parse(operationMatch.Groups[1].Value);

		Match testMatch = _testRegex.Match(lines[3]);
		if (!testMatch.Success) throw new FormatException();
		int testDivisible = int.Parse(testMatch.Groups[1].Value);

		Match testIfTrueMatch = _testIfTrueRegex.Match(lines[4]);
		if (!testIfTrueMatch.Success) throw new FormatException();
		int testIfTrue = int.Parse(testIfTrueMatch.Groups[1].Value);

		Match testIfFalseMatch = _testIfFalseRegex.Match(lines[5]);
		if (!testIfFalseMatch.Success) throw new FormatException();
		int testIfFalse = int.Parse(testIfFalseMatch.Groups[1].Value);

		return new Monkey(index, startingItems, operation, testDivisible, testIfTrue, testIfFalse);
	}

	public Monkey Clone()
	{
		return new Monkey(Index, StartingItems.ToList(), Operation, TestDivisible, TestIfTrue, TestIfFalse);
	}

	object ICloneable.Clone() => Clone();
}
