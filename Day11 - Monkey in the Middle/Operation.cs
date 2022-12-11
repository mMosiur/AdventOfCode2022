namespace AdventOfCode.Year2022.Day11;

class Operation
{
	public Operand Op { get; }
	public ulong? Value { get; }

	public Operation(Operand op, ulong? value)
	{
		Op = op;
		Value = value;
	}

	public ulong Apply(ulong old)
	{
		return Op switch
		{
			Operand.Add => old + (Value ?? old),
			Operand.Multiply => old * (Value ?? old),
			_ => throw new InvalidOperationException()
		};
	}

	public ulong Apply(ulong old, ulong mod)
	{
		checked
		{
			return Op switch
			{
				Operand.Add => (old + (Value ?? old)) % mod,
				Operand.Multiply => (old * (Value ?? old)) % mod,
				_ => throw new InvalidOperationException()
			};
		}
	}

	public static Operation Parse(string operation)
	{
		string[] parts = operation.Split('=', StringSplitOptions.TrimEntries);
		if (parts.Length != 2) throw new FormatException();
		if (parts[0] != "new") throw new FormatException();
		parts = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (parts.Length != 3) throw new FormatException();
		if (parts[0] != "old") throw new FormatException();
		Operand op = parts[1].Single() switch
		{
			'+' => Operand.Add,
			'*' => Operand.Multiply,
			_ => throw new FormatException()
		};
		if (parts[2] == "old")
		{
			return new Operation(op, null);
		}
		else
		{
			return new Operation(op, ulong.Parse(parts[2]));
		}
	}

	public enum Operand
	{
		Add,
		Multiply
	}
}
