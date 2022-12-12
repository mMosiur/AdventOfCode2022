namespace AdventOfCode.Year2022.Day09;

sealed class Rope
{
	private readonly List<Knot> _knots;

	public IEnumerable<Point> KnotPositions => _knots.Select(k => k.Position);
	public Point HeadPosition => _knots[0].Position;
	public Point TailPosition => _knots[^1].Position;

	public Rope(int numberOfKnots)
	{
		if (numberOfKnots < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(numberOfKnots), numberOfKnots, "Number of knots must be greater than zero.");
		}
		_knots = Enumerable
			.Repeat(Point.Origin, numberOfKnots)
			.Select(p => new Knot(p))
			.ToList();
	}

	/// <summary>
	/// Moves the knot towards the target position by one step and returns its new position.
	/// </summary>
	private static Point MovedKnotTowards(Knot knot, Point target)
	{
		(int x, int y) = knot.Position;
		int xDistance = target.X - x;
		int yDistance = target.Y - y;
		if (Math.Abs(xDistance) <= 1 && Math.Abs(yDistance) <= 1)
		{
			return knot.Position; // Knot is already touching the target.
		}
		int xStep = Math.Sign(xDistance);
		int yStep = Math.Sign(yDistance);
		knot.Position = new(x + xStep, y + yStep);
		return knot.Position;
	}

	/// <summary>
	/// Moves the head of the rope in the specified direction,
	/// drags the rest of the knots with it and returns whether all
	/// knots have been moved.
	/// </summary>
	public bool MoveHead(Direction direction)
	{
		if (!DirectionHelpers.IsDefined(direction))
		{
			throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction.");
		}
		_knots[0].Position += direction.ToUnitVector();
		Point nextKnotPosition = _knots[0].Position;
		for (int i = 1; i < _knots.Count; i++)
		{
			Knot knot = _knots[i];
			if (knot.IsTouching(nextKnotPosition))
			{
				return false;
			}
			nextKnotPosition = MovedKnotTowards(knot, nextKnotPosition);
		}
		return true;
	}

	private class Knot
	{
		public Point Position { get; set; }

		public Knot(Point position)
		{
			Position = position;
		}

		public bool IsTouching(Point otherPosition)
		{
			return Math.Abs(Position.X - otherPosition.X) <= 1
				&& Math.Abs(Position.Y - otherPosition.Y) <= 1;
		}
	}
}
