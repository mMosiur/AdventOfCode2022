using System.Text.RegularExpressions;
using AdventOfCode.Year2022.Day05.Ship;

namespace AdventOfCode.Year2022.Day05;

static class InputParser
{
	private static readonly Regex ColumnsLineRegex = new(@"^(?: *(?:\d+))+ *$");
	private static readonly Regex ProcedureStepRegex = new(@"^ *move +(\d+) +from +(\d+) +to +(\d+) *$");

	private static (int ColumnsLineIndex, List<ColumnDefinition> Columns) FindColumnsLine(IReadOnlyList<string> inputLines)
	{
		for (int index = 0; index < inputLines.Count; index++)
		{
			string line = inputLines[index];
			if (ColumnsLineRegex.IsMatch(line))
			{
				List<ColumnDefinition> columns = new();
				int offset = 0;
				foreach (string part in line.Split(' '))
				{
					if (part == "")
					{
						offset++;
						continue;
					}
					int number = int.Parse(part);
					columns.Add(new ColumnDefinition
					{
						ColumnNumber = number,
						HorizontalOffset = offset
					});
					offset += 1 + part.Length;
				}
				return (index, columns);
			}
		}
		throw new InvalidOperationException("Could not find columns line.");
	}

	private static CrateStacks ParseCrateStacks(IReadOnlyList<string> inputLines, int columnsLineIndex, List<ColumnDefinition> columns)
	{
		if (columnsLineIndex < 1)
		{
			throw new InvalidOperationException("No column data found.");
		}
		Dictionary<int, Stack<Crate>> crateStacksDictionary = columns.ToDictionary(
			c => c.ColumnNumber,
			c => new Stack<Crate>()
		);
		for (int i = columnsLineIndex - 1; i >= 0; i--)
		{
			string line = inputLines[i];
			foreach (ColumnDefinition column in columns)
			{
				char c = line.ElementAtOrDefault(column.HorizontalOffset);
				if (char.IsAsciiLetter(c))
				{
					crateStacksDictionary[column.ColumnNumber].Push(new Crate { Mark = c });
				}
			}
		}
		crateStacksDictionary.TrimExcess();
		return new(crateStacksDictionary);
	}

	private static RearrangementProcedure ParseRearrangementProcedure(IReadOnlyList<string> inputLines, int columnsLineIndex, List<ColumnDefinition> columns)
	{
		int procedureStartIndex = columnsLineIndex + 1;
		while (procedureStartIndex < inputLines.Count && string.IsNullOrWhiteSpace(inputLines[procedureStartIndex]))
		{
			procedureStartIndex++;
		}
		if (procedureStartIndex >= inputLines.Count)
		{
			throw new InvalidOperationException("No rearrangement procedure found.");
		}
		List<RearrangementProcedureStep> steps = new();
		for (int i = procedureStartIndex; i < inputLines.Count; i++)
		{
			string line = inputLines[i];
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}
			if (ProcedureStepRegex.Match(line) is not { Success: true } match)
			{
				throw new InvalidOperationException($"Invalid rearrangement procedure step: \"{line}\".");
			}
			int crateCount = int.Parse(match.Groups[1].ValueSpan);
			int fromStackNumber = int.Parse(match.Groups[2].ValueSpan);
			int toStackNumber = int.Parse(match.Groups[3].ValueSpan);
			steps.Add(new RearrangementProcedureStep
			{
				CrateCount = crateCount,
				FromStackNumber = fromStackNumber,
				ToStackNumber = toStackNumber
			});
		}
		steps.TrimExcess();
		return new(steps);
	}

	public static (CrateStacks, RearrangementProcedure) Parse(IEnumerable<string> inputLines)
	{
		List<string> lines = inputLines.ToList();
		(int columnsLineIndex, List<ColumnDefinition> columns) = FindColumnsLine(lines);
		CrateStacks crateStacks = ParseCrateStacks(lines, columnsLineIndex, columns);
		RearrangementProcedure rearrangementProcedure = ParseRearrangementProcedure(lines, columnsLineIndex, columns);
		return (crateStacks, rearrangementProcedure);
	}

	private readonly record struct ColumnDefinition
	{
		public required int ColumnNumber { get; init; }
		public required int HorizontalOffset { get; init; }
	}
}

