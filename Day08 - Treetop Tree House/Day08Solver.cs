using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day08;

public sealed class Day08Solver : DaySolver
{
	private readonly Forest _forest;

	public override int Year => 2022;
	public override int Day => 8;
	public override string Title => "Treetop Tree House";

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_forest = Forest.Parse(Input);
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
		byte height = _forest[x, y].Height;
		bool visible = true;
		for (int yy = y - 1; yy >= 0; yy--)
		{
			int otherHeight = _forest[x, yy].Height;
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		visible = true;
		for (int yy = y + 1; yy < _forest.Height; yy++)
		{
			int otherHeight = _forest[x, yy].Height;
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
			int otherHeight = _forest[xx, y].Height;
			if (otherHeight >= height)
			{
				visible = false;
				break;
			}
		}
		if (visible) return true;
		visible = true;
		for (int xx = x + 1; xx < _forest.Width; xx++)
		{
			int otherHeight = _forest[xx, y].Height;
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
		int height = _forest[x, y].Height;
		int score = 1;
		int count = 0;
		for (int yy = y - 1; yy >= 0; yy--)
		{
			count++;
			int otherHeight = _forest[x, yy].Height;
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int xx = x - 1; xx >= 0; xx--)
		{
			count++;
			int otherHeight = _forest[xx, y].Height;
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int xx = x + 1; xx < _forest.Width; xx++)
		{
			count++;
			int otherHeight = _forest[xx, y].Height;
			if (otherHeight >= height) break;
		}
		score *= count;
		count = 0;
		for (int yy = y + 1; yy < _forest.Height; yy++)
		{
			count++;
			int otherHeight = _forest[x, yy].Height;
			if (otherHeight >= height) break;
		}
		score *= count;
		return score;
	}

	public override string SolvePart1()
	{
		int count = 0;
		for (int i = 0; i < _forest.Width; i++)
		{
			for (int j = 0; j < _forest.Height; j++)
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
		for (int i = 0; i < _forest.Width; i++)
		{
			for (int j = 0; j < _forest.Height; j++)
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
