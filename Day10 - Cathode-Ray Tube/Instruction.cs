namespace AdventOfCode.Year2022.Day10;

struct Instruction
{
	public string Operation { get; set; }
	public int? Argument { get; set; }

	public Instruction(string operation, int? argument)
	{
		Operation = operation;
		Argument = argument;
	}
}
