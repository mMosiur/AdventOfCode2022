using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly int[,] _grid;

	public override int Year => 2022;
	public override int Day => 8;
	public override string Title => "Treetop Tree House";

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		string[] arr = InputLines.ToArray();
		_grid = new int[arr[0].Length, arr.Length];
		for (int i = 0; i < arr.Length; i++)
		{
			for (int j = 0; j < arr[i].Length; j++)
			{
				_grid[j, i] = (int)char.GetNumericValue(arr[i][j]);
			}
		}
	}

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day08Solver() : this(new Day08SolverOptions())
	{
	}

	private bool IsVisible(int x, int y)
	{
		int height = _grid[x, y];
		bool visible = true;
		for (int yy = y - 1; yy >= 0; yy--)
		{
			int otherHeight = _grid[x, yy];
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		visible = true;
		for (int yy = y + 1; yy < _grid.GetLength(1); yy++)
		{
			int otherHeight = _grid[x, yy];
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		visible = true;
		for (int xx = x - 1; xx >= 0; xx--)
		{
			int otherHeight = _grid[xx, y];
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		visible = true;
		for (int xx = x + 1; xx < _grid.GetLength(0); xx++)
		{
			int otherHeight = _grid[xx, y];
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		return false;
	}

	private int ScenicScore(int x, int y)
	{
		int height = _grid[x, y];
		int score = 1;
		int count = 0;
		for (int yy = y - 1; yy >= 0; yy--)
		{
			count++;
			int otherHeight = _grid[x, yy];
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int xx = x - 1; xx >= 0; xx--)
		{
			count++;
			int otherHeight = _grid[xx, y];
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int xx = x + 1; xx < _grid.GetLength(0); xx++)
		{
			count++;
			int otherHeight = _grid[xx, y];
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int yy = y + 1; yy < _grid.GetLength(1); yy++)
		{
			count++;
			int otherHeight = _grid[x, yy];
			if (otherHeight >= height) break;
		}
		score *= count;
		return score;
	}

	public override string SolvePart1()
	{
		int count = 0;
		for (int i = 0; i < _grid.GetLength(0); i++)
		{
			for (int j = 0; j < _grid.GetLength(1); j++)
			{
				bool visible = IsVisible(i, j);
				if (visible)
				{
					count++;
				}
			}
		}
		return $"{count}";
	}

	public override string SolvePart2()
	{
		int max = int.MinValue;
		for (int i = 0; i < _grid.GetLength(0); i++)
		{
			for (int j = 0; j < _grid.GetLength(1); j++)
			{
				int scenicScore = ScenicScore(i, j);
				if (scenicScore > max)
				{
					max = scenicScore;
				}
				if (scenicScore > 8)
				{
					Console.Write("");
				}
			}
		}
		return $"{max}";
	}
}
