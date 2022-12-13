using AdventOfCode.Abstractions;
using AdventOfCode.Year2022.Day10.Device;

namespace AdventOfCode.Year2022.Day10;

public sealed class Day10Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 10;
	public override string Title => "Cathode-Ray Tube";

	private readonly List<Instruction> _instructions;

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
		IReadOnlyList<int> targetCycles = new int[] { 20, 60, 100, 140, 180, 220 };

		Cpu cpu = new(_instructions, startingRegisterX: 1);
		int sum = 0;
		foreach (int targetCycle in targetCycles)
		{
			int registerValue = cpu.RegisterX;
			while (cpu.ClockCycle < targetCycle)
			{
				registerValue = cpu.ClockTick();
			}
			int signalStrength = registerValue * targetCycle;
			sum += signalStrength;
		}
		return $"{sum}";
	}

	public override string SolvePart2()
	{
		Cpu cpu = new(_instructions, startingRegisterX: 1);
		CrtScreen screen = new(cpu, width: 40, height: 6);
		screen.Draw();
		return screen.ToString();
	}
}
