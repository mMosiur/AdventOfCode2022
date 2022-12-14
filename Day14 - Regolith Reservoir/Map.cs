namespace AdventOfCode.Year2022.Day14;

class Map
{
	private readonly Tile[,] _tiles;

	private Point SandOrigin { get; }
	Area Bounds { get; }
	public int Width => Bounds.XRange.Count;
	public int Height => Bounds.YRange.Count;
	public bool HasFloor { get; }

	public Map(Area bounds, Point sandOrigin, bool hasFloor = false)
	{
		if (!bounds.Contains(sandOrigin))
		{
			throw new ArgumentException("Sand origin must be within the map bounds.", nameof(sandOrigin));
		}
		Bounds = bounds;
		SandOrigin = sandOrigin;
		HasFloor = hasFloor;
		int width = bounds.XRange.Count;
		int height = bounds.YRange.Count;
		_tiles = new Tile[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
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

	public static Map BuildFromRockPaths(Point sandOrigin, IEnumerable<Path> rockPaths, int? floorOffset = null)
	{
		Range xRange = new(sandOrigin.X, sandOrigin.X);
		Range yRange = new(sandOrigin.Y, sandOrigin.Y);
		foreach (Path path in rockPaths)
		{
			foreach (Point waypoint in path.Waypoints)
			{
				if (waypoint.X < xRange.Start)
				{
					xRange = new(waypoint.X, xRange.End);
				}
				else if (waypoint.X > xRange.End)
				{
					xRange = new(xRange.Start, waypoint.X);
				}
				if (waypoint.Y < yRange.Start)
				{
					yRange = new(waypoint.Y, yRange.End);
				}
				else if (waypoint.Y > yRange.End)
				{
					yRange = new(yRange.Start, waypoint.Y);
				}
			}
		}
		if (floorOffset is not null)
		{
			int floorY = yRange.End + floorOffset.Value;
			yRange = new(yRange.Start, floorY - 1); // The floor won't be part of the map.

			// We need to maximally widen the map so that it can contain the biggest potential
			// sand pile. The sand pile will be centered on the sand origin, so we need to
			// expand the map by the maximum possible distance from the sand origin to the
			// edge of the map.
			int height = yRange.End - sandOrigin.Y;
			int left = sandOrigin.X - height;
			int right = sandOrigin.X + height;
			if (left < xRange.Start)
			{
				xRange = new(left, xRange.End);
			}
			if (right > xRange.End)
			{
				xRange = new(xRange.Start, right);
			}
		}
		Area bounds = new(xRange, yRange);
		bool hasFloor = floorOffset is not null;
		Map map = new(bounds, sandOrigin, hasFloor);
		foreach (Path path in rockPaths)
		{
			foreach (Point point in path)
			{
				map[point] = Tile.Rock;
			}
		}
		return map;
	}

	/// <summary>
	/// Returns whether the sand particle came to rest.
	/// </summary>
	public bool DropSand()
	{
		if (this[SandOrigin] != Tile.Air)
		{
			return false; // Sand origin is covered
		}
		Point point = SandOrigin;
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

	public enum Tile
	{
		Air,
		Rock,
		Sand
	}
}
