namespace AdventOfCode.Year2022.Day12;

sealed class HillMap
{
	private readonly int[,] _heightMap;

	public Rectangle Area { get; }
	public int Width => Area.XRange.Count;
	public int Height => Area.YRange.Count;
	public Point StartingLocation { get; }
	public Point BestSignalLocation { get; }

	public HillMap(int[,] heightMap, Point startingLocation, Point bestSignalLocation)
	{
		_heightMap = heightMap;
		Area = new Rectangle
		{
			XRange = new(0, _heightMap.GetLength(0) - 1),
			YRange = new(0, _heightMap.GetLength(1) - 1)
		};
		if (!Area.Contains(startingLocation))
		{
			throw new ArgumentException($"Starting location {startingLocation} is not within the map area of height map.", nameof(startingLocation));
		}
		StartingLocation = startingLocation;
		if (!Area.Contains(bestSignalLocation))
		{
			throw new ArgumentException($"Best signal location {bestSignalLocation} is not within the map area of height map.", nameof(bestSignalLocation));
		}
		BestSignalLocation = bestSignalLocation;
	}

	public int this[Point point] => _heightMap[point.X, point.Y];

	public IEnumerable<Point> GetAdjacentPointsOf(Point point)
	{
		Point newPoint = point + new Vector(-1, 0);
		if (Area.Contains(newPoint))
		{
			yield return newPoint;
		}
		newPoint = point + new Vector(1, 0);
		if (Area.Contains(newPoint))
		{
			yield return newPoint;
		}
		newPoint = point + new Vector(0, -1);
		if (Area.Contains(newPoint))
		{
			yield return newPoint;
		}
		newPoint = point + new Vector(0, 1);
		if (Area.Contains(newPoint))
		{
			yield return newPoint;
		}
	}
}
