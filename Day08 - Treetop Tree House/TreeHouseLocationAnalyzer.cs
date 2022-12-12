namespace AdventOfCode.Year2022.Day08;

sealed class TreeHouseLocationAnalyzer
{
	private readonly Forest _forest;

	public TreeHouseLocationAnalyzer(Forest forest)
	{
		ArgumentNullException.ThrowIfNull(forest);
		_forest = forest;
	}

	public bool IsVisibleFromOutside(Point treeLocation)
	{
		if (!_forest.Area.Contains(treeLocation))
		{
			throw new ArgumentOutOfRangeException(nameof(treeLocation), treeLocation, "The point is not within the forest.");
		}
		byte treeHeight = _forest[treeLocation].Height;
		ReadOnlySpan<Vector> directionVectors = stackalloc Vector[4]
		{
			new Vector(0, -1),
			new Vector(0, 1),
			new Vector(-1, 0),
			new Vector(1, 0)
		};
		foreach (Vector directionVector in directionVectors)
		{
			bool visible = true;
			Point point = treeLocation + directionVector;
			while (_forest.Area.Contains(point))
			{
				int otherHeight = _forest[point].Height;
				if (otherHeight >= treeHeight)
				{
					visible = false;
					break;
				}
				point += directionVector;
			}
			if (visible)
			{
				return true;
			}
		}
		return false;
	}

	public int CalculateScenicScore(Point treeLocation)
	{
		if (!_forest.Area.Contains(treeLocation))
		{
			throw new ArgumentOutOfRangeException(nameof(treeLocation), treeLocation, "The point is not within the forest.");
		}
		byte treeHeight = _forest[treeLocation].Height;
		ReadOnlySpan<Vector> directionVectors = stackalloc Vector[4]
		{
			new Vector(0, -1),
			new Vector(0, 1),
			new Vector(-1, 0),
			new Vector(1, 0)
		};
		int scenicScore = 1;
		foreach (Vector directionVector in directionVectors)
		{
			int treeCount = 0;
			Point point = treeLocation + directionVector;
			while (_forest.Area.Contains(point))
			{
				treeCount++;
				int otherHeight = _forest[point].Height;
				if (otherHeight >= treeHeight)
				{
					break;
				}
				point += directionVector;
			}
			scenicScore *= treeCount;
		}
		return scenicScore;
	}

	public int CountTreesVisibleFromOutside()
	{
		return _forest.Area.Points.Count(IsVisibleFromOutside);
	}

	public (Point Location, int ScenicScore) FindBestTreeLocation()
	{
		return _forest.Area.Points
			.Select(p => (p, CalculateScenicScore(p)))
			.MaxBy(p => p.Item2);
	}
}
