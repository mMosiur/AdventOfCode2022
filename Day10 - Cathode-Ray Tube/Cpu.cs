namespace AdventOfCode.Year2022.Day10;

class Cpu
{
	private readonly int _baseX;
	private readonly Instruction[] _program;
	private int _x;
	private int _instructionPointer;
	private int _clockCycle;

	private Instruction CurrentInstruction => _program[_instructionPointer];
	public int RegisterX => _x;
	public int ClockCycle => _clockCycle;
	public int SignalStrength => _x * _clockCycle;

	public Cpu(IEnumerable<Instruction> program, int baseX)
	{
		_program = program.ToArray();
		_instructionPointer = -1;
		_clockCycle = 1;
		_baseX = baseX;
		_x = baseX;
	}

	public void Reset()
	{
		_x = _baseX;
		_instructionPointer = -1;
		_clockCycle = 1;
	}

	private (int Time, Instruction Instruction)? Executing;

	public void ClockTick()
	{
		if (Executing is null)
		{
			_instructionPointer++;
			int timeToExecute = CurrentInstruction.Operation switch
			{
				"noop" => 1,
				"addx" => 2,
				_ => throw new InvalidOperationException()
			};
			Executing = (timeToExecute, CurrentInstruction);
		}
		Executing = Executing.Value with { Time = Executing.Value.Time - 1 };
		if (Executing.Value.Time == 0)
		{
			Instruction instruction = Executing.Value.Instruction;
			switch (instruction.Operation)
			{
				case "noop":
					break;
				case "addx":
					_x += instruction.Argument!.Value;
					break;
			}
			Executing = null;
		}
		_clockCycle++;
	}
}
