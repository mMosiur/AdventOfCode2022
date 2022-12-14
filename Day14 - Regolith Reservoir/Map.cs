namespace AdventOfCode.Year2022.Day14;

class Map
{
	private readonly Tile[,] _tiles;

	Area Bounds { get; }
	public int Width => Bounds.XRange.Count;
	public int Height => Bounds.YRange.Count;

	public Map(Area bounds)
	{
		Bounds = bounds;
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

	public static Map BuildFromRockPaths(Point sandOrigin, IEnumerable<Path> rockPaths)
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
		Area bounds = new(xRange, yRange);
		Map map = new(bounds);
		foreach (Path path in rockPaths)
		{
			foreach (Point point in path)
			{
				map[point] = Tile.Rock;
			}
		}
		return map;
	}

	public bool DropSand(Point point)
	{
		if (!Bounds.Contains(point))
		{
			throw new ArgumentException("Sand can only be dropped within the map.", nameof(point));
		}
		if (this[point] != Tile.Air)
		{
			throw new ArgumentException("Sand can only be dropped on air.", nameof(point));
		}
		this[point] = Tile.Sand;
		while (true)
		{
			Point below = point + new Vector(0, 1);
			if (!Bounds.Contains(below))
			{
				return false; // Sand fell off the map.
			}
			if (this[below] == Tile.Air)
			{
				this[point] = Tile.Air;
				this[below] = Tile.Sand;
				point = below;
				continue;
			}
			Point belowLeft = point + new Vector(-1, 1);
			if (!Bounds.Contains(belowLeft))
			{
				return false; // Sand fell off the map.
			}
			if (this[belowLeft] == Tile.Air)
			{
				this[point] = Tile.Air;
				this[belowLeft] = Tile.Sand;
				point = belowLeft;
				continue;
			}
			Point belowRight = point + new Vector(1, 1);
			if (!Bounds.Contains(belowRight))
			{
				return false; // Sand fell off the map.
			}
			if (this[belowRight] == Tile.Air)
			{
				this[point] = Tile.Air;
				this[belowRight] = Tile.Sand;
				point = belowRight;
				continue;
			}
			// Sand is stationary.
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
