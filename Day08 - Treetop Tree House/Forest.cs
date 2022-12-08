namespace AdventOfCode.Year2022.Day08;

sealed partial class Forest
{
	private readonly Tree[,] _trees;

	public int Width => _trees.GetLength(0);
	public int Height => _trees.GetLength(1);

	public Forest(Tree[,] trees)
	{
		_trees = trees;
	}

	public Tree this[int x, int y] => _trees[x, y];
}
