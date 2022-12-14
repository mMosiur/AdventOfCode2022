namespace AdventOfCode.Year2022.Day08;

sealed partial class Forest
{
	private readonly Tree[,] _trees;

	public Rectangle Area { get; }

	public Forest(Tree[,] trees)
	{
		ArgumentNullException.ThrowIfNull(trees);
		_trees = trees;
		Area = new Rectangle
		{
			XRange = new(0, _trees.GetLength(0) - 1),
			YRange = new(0, _trees.GetLength(1) - 1)
		};
	}

	public Tree this[Point point] => _trees[point.X, point.Y];
}
