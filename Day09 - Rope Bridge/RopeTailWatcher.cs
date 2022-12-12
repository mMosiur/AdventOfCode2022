namespace AdventOfCode.Year2022.Day09;

sealed class RopeTailWatcher
{
	private readonly Rope _rope;
	private readonly HashSet<Point> _tailPositions;

	public IReadOnlySet<Point> UniqueTailPositions => _tailPositions;

	public RopeTailWatcher(Rope rope)
	{
		_rope = rope;
		_tailPositions = new()
		{
			rope.TailPosition
		};
	}

	public void MoveRope(IEnumerable<Motion> seriesOfMotions)
	{
		foreach (Motion motion in seriesOfMotions)
		{
			for (int i = 0; i < motion.Distance; i++)
			{
				bool tailMoved = _rope.MoveHead(motion.Direction);
				if (tailMoved)
				{
					_tailPositions.Add(_rope.TailPosition);
				}
			}
		}
	}
}
