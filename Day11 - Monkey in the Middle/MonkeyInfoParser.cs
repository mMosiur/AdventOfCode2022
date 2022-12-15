using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AdventOfCode.Common.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day11;

static class MonkeyInfoParser
{
	private static readonly Regex _headerRegex = new(@"^[ \t]*Monkey (\d+):[ \t]*$", RegexOptions.IgnoreCase);
	private static readonly Regex _startingItemsRegex = new(@"^[ \t]*Starting items: ([\d, ]+)[ \t]*$", RegexOptions.IgnoreCase);
	private static readonly Regex _operationRegex = new(@"^[ \t]*Operation: new *= *([\w\d]+) *([\*\+]) *([\w\d]+)[ \t]*$", RegexOptions.IgnoreCase);
	private static readonly Regex _testRegex = new(@"^[ \t]*Test: *divisible by +(\d+)[ \t]*$", RegexOptions.IgnoreCase);
	private static readonly Regex _ifTrueRegex = new(@"^[ \t]*If true: *throw to monkey +(\d+)[ \t]*$", RegexOptions.IgnoreCase);
	private static readonly Regex _ifFalseRegex = new(@"^[ \t]*If false: *throw to monkey +(\d+)[ \t]*$", RegexOptions.IgnoreCase);

	public static IReadOnlyList<Monkey> Parse(IEnumerable<string> lines)
	{
		try
		{
			List<Monkey> monkeys = new();
			foreach (IReadOnlyList<string> groupLines in lines.SplitWithSeparator(string.Empty))
			{
				if (groupLines.Count == 0) continue;
				if (groupLines.Count != 6)
				{
					throw new FormatException($"Expected 6 information lines per monkey (found {groupLines.Count}).");
				}
				ushort monkeyId = ParseHeader(groupLines[0]);
				if (monkeyId != monkeys.Count)
				{
					throw new InvalidOperationException($"Expected monkey ID {monkeys.Count} (found {monkeyId}).");
				}
				IList<Item> startingItems = ParseStartingItems(groupLines[1]);
				Monkey.ItemInspectOperation operation = ParseItemInspectOperation(groupLines[2]);
				byte testDivisor = ParseItemInspectTestDivisor(groupLines[3]);
				ushort ifTrueMonkeyId = ParseIfTestTrue(groupLines[4]);
				ushort ifFalseMonkeyId = ParseIfTestFalse(groupLines[5]);
				if (ifTrueMonkeyId == monkeyId || ifFalseMonkeyId == monkeyId)
				{
					throw new InvalidOperationException($"Monkey {monkeyId} cannot throw to itself.");
				}
				Monkey monkey = new(monkeyId, startingItems, operation, testDivisor, ifTrueMonkeyId, ifFalseMonkeyId);
				monkeys.Add(monkey);
			}
			monkeys.TrimExcess();
			return monkeys;
		}
		catch (SystemException e)
		{
			throw new FormatException("Could not parse input lines.", e);
		}
	}

	private static ushort ParseHeader(string s)
	{
		Match match = _headerRegex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Invalid header: '{s}'.");
		}
		return ushort.Parse(match.Groups[1].ValueSpan);
	}

	private static IList<Item> ParseStartingItems(string s)
	{
		Match match = _startingItemsRegex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Invalid starting items: '{s}'.");
		}
		return match.Groups[1].Value
			.Split(',', StringSplitOptions.TrimEntries)
			.Select(ulong.Parse)
			.Select(i => new Item(i))
			.ToList();
	}

	private static Monkey.ItemInspectOperation ParseItemInspectOperation(string s)
	{
		const string ParamName = "old";
		try
		{
			Match match = _operationRegex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid operation format.");
			}
			string param1 = match.Groups[1].Value;
			string op = match.Groups[2].Value;
			string param2 = match.Groups[3].Value;
			ParameterExpression paramOld = Expression.Parameter(typeof(ulong), ParamName);
			Expression param1Expression = param1.Equals(paramOld.Name, StringComparison.OrdinalIgnoreCase)
				? paramOld
				: Expression.Constant(ulong.Parse(param1));
			Expression param2Expression = param2.Equals(paramOld.Name, StringComparison.OrdinalIgnoreCase)
				? paramOld
				: Expression.Constant(ulong.Parse(param2));
			ExpressionType expressionType = op switch
			{
				"*" => ExpressionType.Multiply,
				"+" => ExpressionType.Add,
				_ => throw new NotSupportedException($"Operation '{op}' not supported.")
			};
			Expression expression = Expression.MakeBinary(expressionType, param1Expression, param2Expression);
			Monkey.ItemInspectOperation operation = Expression.Lambda<Monkey.ItemInspectOperation>(expression, paramOld).Compile();
			return operation;
		}
		catch (SystemException e)
		{
			throw new FormatException($"Could not parse operation: '{s}'.", e);
		}
	}

	private static byte ParseItemInspectTestDivisor(string s)
	{
		try
		{
			Match match = _testRegex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid test format.");
			}
			return byte.Parse(match.Groups[1].ValueSpan);
		}
		catch (SystemException e)
		{
			throw new FormatException($"Could not parse test: '{s}'.", e);
		}
	}

	private static ushort ParseIfTestTrue(string s)
	{
		try
		{
			Match match = _ifTrueRegex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid test if true format.");
			}
			return ushort.Parse(match.Groups[1].ValueSpan);
		}
		catch (SystemException e)
		{
			throw new FormatException($"Could not parse 'if true' section: '{s}'.", e);
		}
	}

	private static ushort ParseIfTestFalse(string s)
	{
		try
		{
			Match match = _ifFalseRegex.Match(s);
			if (!match.Success)
			{
				throw new FormatException($"Invalid test if false format.");
			}
			return ushort.Parse(match.Groups[1].ValueSpan);
		}
		catch (SystemException e)
		{
			throw new FormatException($"Could not parse 'if false' section: '{s}'.", e);
		}
	}
}
