using AdventOfCode.Common.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day12;

sealed class InputParser
{
	private readonly char _startChar;
	private readonly char _endChar;

	public InputParser(char startChar, char endChar)
	{
		if (char.IsAsciiLetterLower(startChar))
		{
			throw new ArgumentException($"Start char cannot be a lowercase ASCII letter (was '{startChar}').", nameof(startChar));
		}
		if (char.IsAsciiLetterLower(endChar))
		{
			throw new ArgumentException($"End char cannot be a lowercase ASCII letter (was '{endChar}').", nameof(endChar));
		}
		_startChar = startChar;
		_endChar = endChar;
	}

	public HillMap Parse(IEnumerable<string> lines)
	{
		int height = lines.Count();
		int width = lines.First().Length;
		int[,] map = new int[height, width];
		Point start = Point.Origin;
		Point end = Point.Origin;
		foreach ((string line, int x) in lines.WithIndex())
		{
			foreach ((char c, int y) in line.WithIndex())
			{
				char charElevation = c;
				if (c == _startChar)
				{
					start = new Point(x, y);
					charElevation = 'a';
				}
				else if (c == _endChar)
				{
					end = new Point(x, y);
					charElevation = 'z';
				}
				else if (!char.IsAsciiLetterLower(c))
				{
					throw new FormatException($"Unexpected character '{c}' at ({x}, {y}).");
				}
				int elevation = charElevation - 'a';
				map[x, y] = elevation;
			}
		}
		return new(map, start, end);
	}
}
