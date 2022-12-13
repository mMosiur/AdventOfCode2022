namespace AdventOfCode.Year2022.Day10.Device;

sealed class Cpu
{
	private readonly Instruction[] _program;
	private int _registerX;
	private int _instructionPointer;
	private int _clockCycle;
	private Instruction? _currentlyExecutingInstruction;
	private int _timeToExecute;

	private Instruction CurrentInstruction => _program[_instructionPointer];
	public int RegisterX => _registerX;
	public int ClockCycle => _clockCycle;
	public int SignalStrength => _registerX * _clockCycle;

	public Cpu(IEnumerable<Instruction> program, int startingRegisterX)
	{
		_program = program.ToArray();
		_instructionPointer = -1;
		_clockCycle = 0;
		_registerX = startingRegisterX;
		_currentlyExecutingInstruction = null;
		_timeToExecute = -1;
	}

	private static int GetTimeToExecuteForOperation(Instruction.OperationCode operation)
	{
		return operation switch
		{
			Instruction.OperationCode.Noop => 1,
			Instruction.OperationCode.AddX => 2,
			_ => throw new ArgumentException($"Unknown operation '{operation}'.", nameof(operation))
		};
	}

	private int StartNextInstruction()
	{
		_instructionPointer++;
		return StartExecution(CurrentInstruction);
	}

	private int StartExecution(Instruction instruction)
	{
		if (_currentlyExecutingInstruction is not null)
		{
			throw new InvalidOperationException("Cannot schedule execution when another instruction is already executing.");
		}
		_currentlyExecutingInstruction = instruction;
		_timeToExecute = GetTimeToExecuteForOperation(instruction.Operation);
		return _timeToExecute;
	}

	private void FinishExecution()
	{
		if (_currentlyExecutingInstruction is null)
		{
			throw new InvalidOperationException("Cannot finish execution when no instruction is executing.");
		}
		if (_timeToExecute > 0)
		{
			throw new InvalidOperationException("Cannot finish execution when the instruction is not done executing.");
		}
		switch (_currentlyExecutingInstruction.Value.Operation)
		{
			case Instruction.OperationCode.Noop:
				break;
			case Instruction.OperationCode.AddX:
				_registerX += _currentlyExecutingInstruction.Value.Argument;
				break;
		}
		_currentlyExecutingInstruction = null;
		_timeToExecute = -1;
	}

	/// <summary>
	/// Ticks the clock once and returns the register X value during the tick.
	/// </summary>
	public int ClockTick()
	{
		_clockCycle++;
		// Start of the cycle
		if (_currentlyExecutingInstruction is null)
		{
			StartNextInstruction();
		}
		// During the cycle
		int registerValueDuringCycle = _registerX;
		// End of the cycle
		_timeToExecute--;
		if (_timeToExecute <= 0)
		{
			FinishExecution();
		}
		return registerValueDuringCycle;
	}
}
