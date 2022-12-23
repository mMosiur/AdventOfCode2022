using System.Diagnostics;

namespace AdventOfCode.Year2022.Day15;

readonly struct Diagonal : IEquatable<Diagonal>
{
	public Direction Orientation { get; }
	public int C { get; }

	public Diagonal(Direction orientation, int c)
	{
		if (!Enum.IsDefined(orientation))
		{
			throw new ArgumentException("Invalid direction", nameof(orientation));
		}
		Orientation = orientation;
		C = c;
	}

	public static Point Intersect(Diagonal a, Diagonal b)
	{
		if (a.Orientation == b.Orientation)
		{
			throw new ArgumentException("Diagonals must be of different orientations");
		}
		int c1, c2;
		switch (a.Orientation)
		{
			case Direction.XMinusY:
				c1 = a.C;
				c2 = b.C;
				break;
			case Direction.XPlusY:
				c1 = b.C;
				c2 = a.C;
				break;
			default:
				throw new UnreachableException();
		}
		int x = -(c1 + c2) / 2;
		int y = (c1 - c2) / 2;
		return new(x, y);
	}

	public bool Equals(Diagonal other) => Orientation == other.Orientation && C == other.C;
	public override bool Equals(object? obj)
	{
		return obj is Diagonal diagonal && Equals(diagonal);
	}
	public override int GetHashCode() => HashCode.Combine(Orientation, C);
	public static bool operator ==(Diagonal left, Diagonal right) => left.Equals(right);
	public static bool operator !=(Diagonal left, Diagonal right) => !(left == right);

	public enum Direction
	{
		XMinusY, // x - y + C = 0
		XPlusY // x + y + C = 0
	}
}
