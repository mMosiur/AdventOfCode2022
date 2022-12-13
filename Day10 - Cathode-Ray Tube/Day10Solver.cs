using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day10;

public sealed class Day10Solver : DaySolver
{
	private readonly List<Instruction> _instructions;

	public override int Year => 2022;
	public override int Day => 10;
	public override string Title => "Cathode-Ray Tube";

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		_instructions = InputLines.Select(Instruction.Parse).ToList();
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
		Cpu cpu = new(_instructions, 1);
		while (cpu.ClockCycle < 220)
		{
			int registerValue = cpu.ClockTick();
			if (targets.Contains(cpu.ClockCycle))
			{
				sum += registerValue * cpu.ClockCycle;
			}
		}
		return $"{sum} ({sum == 14620})";
	}

	public override string SolvePart2()
	{
		Cpu cpu = new(_instructions, 1);
		CrtScreen screen = new(cpu, 40, 6);
		screen.Draw();
		return screen.ToString();
	}
}
