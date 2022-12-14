using AdventOfCode.Common.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day12;

sealed class InputParser
{
	private readonly char _currentLocationChar;
	private readonly char _bestSignalLocationChar;

	public InputParser(char currentLocationChar, char bestSignalLocationChar)
	{
		if (char.IsAsciiLetterLower(currentLocationChar))
		{
			throw new ArgumentException($"Current location char cannot be a lowercase ASCII letter (was '{currentLocationChar}').", nameof(currentLocationChar));
		}
		if (char.IsAsciiLetterLower(bestSignalLocationChar))
		{
			throw new ArgumentException($"Best signal location char cannot be a lowercase ASCII letter (was '{bestSignalLocationChar}').", nameof(bestSignalLocationChar));
		}
		_currentLocationChar = currentLocationChar;
		_bestSignalLocationChar = bestSignalLocationChar;
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
				if (c == _currentLocationChar)
				{
					start = new Point(x, y);
					charElevation = 'a';
				}
				else if (c == _bestSignalLocationChar)
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
