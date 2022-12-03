namespace AdventOfCode.Year2022.Day03.EnumerableExtensions;

static class EnumerableExtensions
{
	public static IEnumerable<IEnumerable<T>> Group<T>(this IEnumerable<T> source, int groupSize)
	{
		if (groupSize <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(groupSize), groupSize, "Group size must be greater than zero.");
		}
		return source
			.Select((s, i) => new { Value = s, Index = i })
			.GroupBy(item => item.Index / groupSize, item => item.Value)
			.Cast<IEnumerable<T>>();
	}
}
