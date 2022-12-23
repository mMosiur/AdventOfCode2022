namespace AdventOfCode.Year2022.Day14;

sealed class Map
{
	private readonly Tile[,] _tiles;

	public Point SandSource { get; }
	public Area Bounds { get; }
	public bool HasFloor { get; }
	public int Width => Bounds.XRange.Count;
	public int Height => Bounds.YRange.Count;

	private Map(Area bounds, Point sandSource, bool hasFloor = false)
	{
		if (!bounds.Contains(sandSource))
		{
			throw new ArgumentException("Sand source must be within the map bounds.", nameof(sandSource));
		}
		SandSource = sandSource;
		Bounds = bounds;
		HasFloor = hasFloor;
		_tiles = new Tile[Width, Height];
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				_tiles[x, y] = Tile.Air;
			}
		}
	}

	public Tile this[Point p]
	{
		get => _tiles[p.X - Bounds.XRange.Start, p.Y - Bounds.YRange.Start];
		private set => _tiles[p.X - Bounds.XRange.Start, p.Y - Bounds.YRange.Start] = value;
	}

	public static Map BuildFromRockPaths(Point sandSource, IEnumerable<WaypointPath> rockPaths, int? floorOffset = null)
	{
		bool hasFloor = false;
		Range xRange = new(sandSource.X, sandSource.X);
		Range yRange = new(sandSource.Y, sandSource.Y);
		foreach (Point waypoint in rockPaths.SelectMany(p => p))
		{
			xRange = new(
				start: Math.Min(waypoint.X, xRange.Start),
				end: Math.Max(waypoint.X, xRange.End)
			);
			yRange = new(
				start: Math.Min(waypoint.Y, yRange.Start),
				end: Math.Max(waypoint.Y, yRange.End)
			);
		}
		if (floorOffset is not null)
		{
			hasFloor = true;
			int floorY = yRange.End + floorOffset.Value;
			yRange = new(yRange.Start, floorY - 1);
			// We need to maximally widen the map so that it can contain the biggest potential
			// sand pile. The sand pile will be centered on the sand source, so we need to
			// expand the map by the maximum possible distance from the sand source to the
			// edge of the map.
			int height = yRange.End - sandSource.Y;
			xRange = new(
				start: Math.Min(sandSource.X - height, xRange.Start),
				end: Math.Max(sandSource.X + height, xRange.End)
			);
		}
		Area bounds = new(xRange, yRange);
		Map map = new(bounds, sandSource, hasFloor);
		foreach (Point point in rockPaths.SelectMany(p => p))
		{
			map[point] = Tile.Rock;
		}
		return map;
	}

	/// <summary>
	/// Returns whether the sand particle came to rest.
	/// </summary>
	private bool DropOneSandFrom(Point point)
	{
		if (!Bounds.Contains(point) || this[point] != Tile.Air)
		{
			return false; // Point is already covered or not in bounds.
		}
		this[point] = Tile.Sand;
		while (true)
		{
			Point below = point + new Vector(0, 1);
			if (Bounds.Contains(below))
			{
				if (this[below] == Tile.Air)
				{
					this[point] = Tile.Air;
					this[below] = Tile.Sand;
					point = below;
					continue;
				}
			}
			else if (!HasFloor)
			{
				return false; // Sand fell off the map.
			}
			Point belowLeft = point + new Vector(-1, 1);
			if (Bounds.Contains(belowLeft))
			{
				if (this[belowLeft] == Tile.Air)
				{
					this[point] = Tile.Air;
					this[belowLeft] = Tile.Sand;
					point = belowLeft;
					continue;
				}
			}
			else if (!HasFloor)
			{
				return false; // Sand fell off the map.
			}
			Point belowRight = point + new Vector(1, 1);
			if (Bounds.Contains(belowRight))
			{
				if (this[belowRight] == Tile.Air)
				{
					this[point] = Tile.Air;
					this[belowRight] = Tile.Sand;
					point = belowRight;
					continue;
				}
			}
			else if (!HasFloor)
			{
				return false; // Sand fell off the map.
			}
			// Sand is stationary and came to rest.
			return true;
		}
	}

	/// <summary>
	/// Recursively drops all sand particles from the given point until
	/// no more can be dropped.
	/// IMPORTANT: it does not work if the map does not have a floor and particles
	/// can fall off the map.
	/// </summary>
	private int DropSandContinuouslyFrom(Point point)
	{
		if (!HasFloor) throw new InvalidOperationException("Cannot drop sand continuously from a point if the map does not have a floor.");
		if (this[point] != Tile.Air)
		{
			return 0; // Point is covered
		}
		int result = 0;
		Point below = point + new Vector(0, 1);
		if (Bounds.Contains(below))
		{
			result += DropSandContinuouslyFrom(below);
		}
		Point belowLeft = point + new Vector(-1, 1);
		if (Bounds.Contains(belowLeft))
		{
			result += DropSandContinuouslyFrom(belowLeft);
		}
		Point belowRight = point + new Vector(1, 1);
		if (Bounds.Contains(belowRight))
		{
			result += DropSandContinuouslyFrom(belowRight);
		}
		this[point] = Tile.Sand;
		return result + 1;
	}

	/// <summary>
	/// Returns the number of sand particles that came to rest.
	/// </summary>
	public int SimulateUntilAnOverflow()
	{
		if (HasFloor)
		{
			// Continuous sand dropping is only possible if there is floor present,
			// and no sand can fall off the map.
			return DropSandContinuouslyFrom(SandSource);
		}
		else // No floor
		{
			int count = 0;
			while (DropOneSandFrom(SandSource))
			{
				count++;
			}
			return count;
		}
	}

	public enum Tile
	{
		Air,
		Rock,
		Sand
	}
}
