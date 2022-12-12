using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2022.Day09;

readonly struct Motion
{
	public Direction Direction { get; }
	public int Distance { get; }

	public Motion(Direction direction, int distance)
	{
		if (!DirectionHelpers.IsDefined(direction))
		{
			throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.");
		}
		if (distance < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(distance), distance, "Distance must be greater than zero.");
		}
		Direction = direction;
		Distance = distance;
	}

	public static Motion Parse(string input)
	{
		if (!input.AsSpan().TrySplitInTwo(' ', out ReadOnlySpan<char> first, out ReadOnlySpan<char> second, true))
		{
			throw new FormatException("Input doesn't contain two space-separated parts.");
		}
		if (first.Length != 1)
		{
			throw new FormatException("First part of input must be a single character.");
		}
		Direction direction = DirectionHelpers.FromChar(first[0]);
		if (!int.TryParse(second, out int distance))
		{
			throw new FormatException("Second part of input must be an integer.");
		}
		return new Motion(direction, distance);
	}
}
