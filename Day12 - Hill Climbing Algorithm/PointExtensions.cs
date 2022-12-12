namespace AdventOfCode.Year2022.Day12;

static class PointExtensions
{
	public static bool IsInBounds(this Point point, int minX, int minY, int maxX, int maxY)
	{
		return point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY;
	}

	public static IEnumerable<Point> GetAdjacentPoints(this Point point, int maxX, int maxY, int minX = 0, int minY = 0)
	{
		Point newPoint = point + new Vector(-1, 0);
		if (newPoint.IsInBounds(minX, minY, maxX, maxY))
			yield return newPoint;
		newPoint = point + new Vector(1, 0);
		if (newPoint.IsInBounds(minX, minY, maxX, maxY))
			yield return newPoint;
		newPoint = point + new Vector(0, -1);
		if (newPoint.IsInBounds(minX, minY, maxX, maxY))
			yield return newPoint;
		newPoint = point + new Vector(0, 1);
		if (newPoint.IsInBounds(minX, minY, maxX, maxY))
			yield return newPoint;
	}
}
