namespace AdventOfCode.Year2022.Day10.Device;

readonly struct Instruction
{
	public OperationCode Operation { get; }
	public int Argument { get; }

	public Instruction(OperationCode operation, int argument)
	{
		Operation = operation;
		Argument = argument;
	}

	public static Instruction Parse(string line)
	{
		ReadOnlySpan<char> span = line.AsSpan().Trim();
		int index = span.IndexOf(' ');
		index = index < 0 ? span.Length : index; // Set index to end if not found
		ReadOnlySpan<char> operationSpan = span[..index];
		OperationCode operation = ParseOperation(operationSpan);
		int argument = default;
		if (index < span.Length)
		{
			ReadOnlySpan<char> argumentSpan = span[(index + 1)..];
			argument = int.Parse(argumentSpan);
		}
		return new Instruction(operation, argument);
	}

	private static OperationCode ParseOperation(ReadOnlySpan<char> s)
	{
		return s switch
		{
			"noop" => OperationCode.Noop,
			"addx" => OperationCode.AddX,
			_ => throw new FormatException($"Unknown operation \"{s}\".")
		};
	}

	public enum OperationCode
	{
		Noop,
		AddX,
	}
}
