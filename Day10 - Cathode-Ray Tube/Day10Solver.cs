using AdventOfCode.Abstractions;
using AdventOfCode.Common.SpanExtensions;

namespace AdventOfCode.Year2022.Day10;

public sealed class Day10Solver : DaySolver
{
	private readonly List<Instruction> _input;

	public override int Year => 2022;
	public override int Day => 10;
	public override string Title => "UNKNOWN";

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		_input = InputLines.Select(line =>
			line.AsSpan().TrySplitInTwo(' ',
				out ReadOnlySpan<char> operation,
				out ReadOnlySpan<char> argument
			) ? new Instruction(operation.ToString(), int.Parse(argument)) : new Instruction(line.ToString(), null)
		).ToList();
	}

	public Day10Solver(Action<Day10SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day10Solver() : this(new Day10SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int sum = 0;
		int[] targets = new int[] { 20, 60, 100, 140, 180, 220 };
		Cpu cpu = new(_input, 1);
		while (cpu.ClockCycle < 230)
		{
			cpu.ClockTick();
			if (targets.Contains(cpu.ClockCycle))
			{
				sum += cpu.SignalStrength;
			}
		}
		return $"{sum}";
	}

	public override string SolvePart2()
	{
		Cpu cpu = new(_input, 1);
		CrtScreen screen = new(cpu, 40, 6);
		screen.Draw();
		return screen.ToString();
	}
}
