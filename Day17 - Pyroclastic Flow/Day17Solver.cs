using System.Collections;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day17;

public sealed class Day17Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 17;
	public override string Title => "Pyroclastic Flow";

	private readonly Jets _jets;

	public Day17Solver(Day17SolverOptions options) : base(options)
	{
		_jets = new(Input);
	}

	public Day17Solver(Action<Day17SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day17Solver() : this(new Day17SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		return "UNSOLVED";
		Range xRange = new(0, 6);
		int topY = -1;
		int rocksStoppedCount = 0;
		ShapesIterator shapes = new();
		JetsIterator jets = _jets.GetEnumerator();
		HashSet<Point> occupiedPoints = new();

		jets.MoveNext();
		Rock? rockFalling = null;
		while (rocksStoppedCount < 2022)
		{
			if (rockFalling is null)
			{
				shapes.MoveNext();
				Point origin = new(xRange.Start + 3, topY + 4);
				rockFalling = new(shapes.Current, origin);
			}
			while (rockFalling is not null)
			{
				Point origin = new(xRange.Start, topY);
				WindJet jet = jets.Current;
				rockFalling.Move(jet.Direction, xRange);
			}
		}
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}

sealed class Shape
{
	public ICollection<Vector> Offsets { get; }
	public int Width { get; }

	private Shape(ICollection<Vector> offsets)
	{
		Offsets = offsets;
		int minX = offsets.Min(offset => offset.X);
		int maxX = offsets.Max(offset => offset.X);
		Width = maxX - minX + 1;
	}

	public static readonly Shape HorizontalLineShape = new(new Vector[] { new(0, 0), new(1, 0), new(2, 0), new(3, 0) });
	public static readonly Shape PlusShape = new(new Vector[] { new(1, 0), new(0, 1), new(1, 1), new(2, 1), new(1, 2) });
	public static readonly Shape CornerShape = new(new Vector[] { new(0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2) });
	public static readonly Shape VerticalLineShape = new(new Vector[] { new(0, 0), new(0, 1), new(0, 2), new(0, 3) });
	public static readonly Shape SquareShape = new(new Vector[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) });

	public IEnumerable<Point> GetPoints(Point origin) => Offsets.Select(offset => origin + offset);
}

sealed class Rock
{
	public Shape Shape { get; }
	public Point Origin { get; private set; }
	public int MinX { get; private set; }
	public int MaxX { get; private set; }
	public int MinY { get; private set; }
	public int MaxY { get; private set; }
	public int Width => Shape.Width;
	public IEnumerable<Point> Points => Shape.Offsets.Select(offset => Origin + offset);

	public void Move(Vector vector, Range? xRange = null)
	{
		Origin += vector;
		MinX = xRange.HasValue ? Math.Max(MinX + vector.X, xRange.Value.Start) : (MinX + vector.X);
		MaxX = xRange.HasValue ? Math.Min(MaxX + vector.X, xRange.Value.End) : (MaxX + vector.X);
		MinY += vector.Y;
		MaxY += vector.Y;
	}

	public Rock(Shape shape, Point origin)
	{
		Shape = shape;
		Origin = origin;
		MinX = Points.Select(p => p.X).Min();
		MaxX = Points.Select(p => p.X).Max();
		MinY = Points.Select(p => p.Y).Min();
		MaxY = Points.Select(p => p.Y).Max();
	}
}

sealed class ShapesIterator : IEnumerator<Shape>
{
	private static readonly Shape[] _shapes = new[]
	{
		Shape.HorizontalLineShape,
		Shape.PlusShape,
		Shape.CornerShape,
		Shape.VerticalLineShape,
		Shape.SquareShape
	};

	private int _index = -1;

	public Shape Current => _index >= 0 ? _shapes[_index]
		: throw new InvalidOperationException("The enumerator has not been started yet.");
	object IEnumerator.Current => Current;

	public void Dispose()
	{
	}

	public bool MoveNext()
	{
		_index = (_index + 1) % _shapes.Length;
		return true;
	}

	public void Reset()
	{
		_index = -1;
	}
}

readonly struct WindJet
{
	public Vector Direction { get; }

	public WindJet(Vector direction)
	{
		Direction = direction;
	}

	public static implicit operator WindJet(Vector vector) => new(vector);

	public static WindJet FromChar(char c) => c switch
	{
		'>' => new Vector(1, 0),
		'<' => new Vector(-1, 0),
		_ => throw new InvalidOperationException($"Invalid wind jet character: '{c}'")
	};
}

sealed class Jets : IEnumerable<WindJet>
{
	private readonly string _s;

	public Jets(string s)
	{
		_s = s.Trim();
		if (_s.Any(c => c is not '>' and not '<'))
		{
			throw new InvalidOperationException($"Invalid wind jet string: '{s}'");
		}
	}

	public JetsIterator GetEnumerator() => new(_s);
	IEnumerator<WindJet> IEnumerable<WindJet>.GetEnumerator() => GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

sealed class JetsIterator : IEnumerator<WindJet>
{
	private readonly CharEnumerator _charEnumerator;

	public JetsIterator(string s)
	{
		_charEnumerator = s.GetEnumerator();
	}

	public WindJet Current => WindJet.FromChar(_charEnumerator.Current);
	object IEnumerator.Current => Current;

	public void Dispose()
	{
		_charEnumerator.Dispose();
	}

	public bool MoveNext()
	{
		bool moved = _charEnumerator.MoveNext();
		if (!moved)
		{
			_charEnumerator.Reset();
			_charEnumerator.MoveNext();
		}
		return true;
	}

	public void Reset()
	{
		_charEnumerator.Reset();
	}
}
