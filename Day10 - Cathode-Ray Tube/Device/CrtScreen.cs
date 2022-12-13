using System.Text;

namespace AdventOfCode.Year2022.Day10.Device;

sealed class CrtScreen
{
	private readonly Cpu _cpu;
	private readonly Rectangle _bounds;
	private readonly char[,] _pixels;

	public CrtScreen(Cpu cpu, int width, int height)
	{
		_cpu = cpu;
		_bounds = new(new(0, height - 1), new(0, width - 1));
		_pixels = new char[height, width];
	}

	public char this[Point point]
	{
		get => _pixels[point.X, point.Y];
		private set => _pixels[point.X, point.Y] = value;
	}

	public void Draw()
	{
		foreach (Point point in _bounds.Points)
		{
			int registerDuringCycle = _cpu.ClockTick();
			bool isPointInSprite = Math.Abs(registerDuringCycle - point.Y) <= 1;
			this[point] = isPointInSprite ? '#' : '.';
		}
	}

	public override string ToString()
	{
		StringBuilder builder = new();
		for (int x = _bounds.XRange.Start; x <= _bounds.XRange.End; x++)
		{
			for (int y = _bounds.YRange.Start; y <= _bounds.YRange.End; y++)
			{
				builder.Append(this[new(x, y)]);
			}
			builder.AppendLine();
		}
		builder.Remove(builder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
		return builder.ToString();
	}
}
