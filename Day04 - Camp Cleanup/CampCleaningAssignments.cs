using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022.Day04;

class CampCleaningAssignments
{
	private static readonly Regex ComparisonRegex = new(@"^\s*(\d+)\s*-\s*(\d+)\s*,\s*(\d+)\s*-\s*(\d+)\s*$", RegexOptions.Compiled);

	public IReadOnlyCollection<(Range, Range)> Comparisons { get; }

	public CampCleaningAssignments(IEnumerable<(Range, Range)> comparisons)
	{
		Comparisons = comparisons.ToList();
	}

	public static CampCleaningAssignments Parse(IEnumerable<string> lines)
	{
		return new CampCleaningAssignments(lines.Select(line =>
		{
			Match match = ComparisonRegex.Match(line);
			if (!match.Success)
			{
				throw new FormatException($"Invalid assignment comparison: \"{line}\".");
			}
			return (
				new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
				new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))
			);
		}));
	}
}
