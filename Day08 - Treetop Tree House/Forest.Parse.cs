using System.Text;
using AdventOfCode.Common.EnumeratorExtensions;
using Microsoft.Toolkit.HighPerformance;

namespace AdventOfCode.Year2022.Day08;

sealed partial class Forest
{
	public static Forest Parse(string s)
	{
		try
		{
			ReadOnlySpan<char> span = s.AsSpan();
			SpanLineEnumerator it = span.EnumerateLines().GetEnumerator();
			it.EnsureMoveToNextNonEmptyLine("No lines in input.");
			int width = it.Current.Length;
			List<Tree[]> rows = new();
			do
			{
				if (it.Current.Length != width)
				{
					throw new FormatException("Line length is not consistent.");
				}
				Tree[] row = new Tree[width];
				for (int i = 0; i < width; i++)
				{
					row[i] = new Tree(
						height: Convert.ToByte(char.GetNumericValue(it.Current[i]))
					);
				}
				rows.Add(row);
			} while (it.MoveToNextNonEmptyLine());
			Tree[,] map = new Tree[rows.Count, width];
			for (int i = 0; i < rows.Count; i++)
			{
				rows[i].CopyTo(map.GetRowMemory(i));
			}
			return new Forest(map);
		}
		catch (Exception e)
		{
			throw new FormatException("Invalid input.", e);
		}
	}
}
