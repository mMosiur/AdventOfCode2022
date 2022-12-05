using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day05;

public sealed class Day05Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 5;
	public override string Title => "Supply Stacks";

	public Day05Solver(Day05SolverOptions options) : base(options)
	{
	}

	private Stack<char>[] PrepStacks()
	{
		Stack<char>[] stacks = new Stack<char>[9];
		stacks[0] = new();
		stacks[0].Push('B');
		stacks[0].Push('G');
		stacks[0].Push('S');
		stacks[0].Push('C');
		stacks[1] = new();
		stacks[1].Push('T');
		stacks[1].Push('M');
		stacks[1].Push('W');
		stacks[1].Push('H');
		stacks[1].Push('J');
		stacks[1].Push('N');
		stacks[1].Push('V');
		stacks[1].Push('G');
		stacks[2] = new();
		stacks[2].Push('M');
		stacks[2].Push('Q');
		stacks[2].Push('S');
		stacks[3] = new();
		stacks[3].Push('B');
		stacks[3].Push('S');
		stacks[3].Push('L');
		stacks[3].Push('T');
		stacks[3].Push('W');
		stacks[3].Push('N');
		stacks[3].Push('M');
		stacks[4] = new();
		stacks[4].Push('J');
		stacks[4].Push('Z');
		stacks[4].Push('F');
		stacks[4].Push('T');
		stacks[4].Push('V');
		stacks[4].Push('G');
		stacks[4].Push('W');
		stacks[4].Push('P');
		stacks[5] = new();
		stacks[5].Push('C');
		stacks[5].Push('T');
		stacks[5].Push('B');
		stacks[5].Push('G');
		stacks[5].Push('Q');
		stacks[5].Push('H');
		stacks[5].Push('S');
		stacks[6] = new();
		stacks[6].Push('T');
		stacks[6].Push('J');
		stacks[6].Push('P');
		stacks[6].Push('B');
		stacks[6].Push('W');
		stacks[7] = new();
		stacks[7].Push('G');
		stacks[7].Push('D');
		stacks[7].Push('C');
		stacks[7].Push('Z');
		stacks[7].Push('F');
		stacks[7].Push('T');
		stacks[7].Push('Q');
		stacks[7].Push('M');
		stacks[8] = new();
		stacks[8].Push('N');
		stacks[8].Push('S');
		stacks[8].Push('H');
		stacks[8].Push('B');
		stacks[8].Push('P');
		stacks[8].Push('F');
		return stacks;
	}


	public Day05Solver(Action<Day05SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day05Solver() : this(new Day05SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		Regex regex = new(@"\s*move (\d+) from (\d+) to (\d+)\s*");
		Stack<char>[] stacks = PrepStacks();
		foreach (string line in InputLines.Skip(10))
		{
			Match match = regex.Match(line);
			int count = int.Parse(match.Groups[1].ValueSpan);
			int from = int.Parse(match.Groups[2].ValueSpan) - 1;
			int to = int.Parse(match.Groups[3].ValueSpan) - 1;
			for (int i = 0; i < count; i++)
			{
				stacks[to].Push(stacks[from].Pop());
			}
		}
		StringBuilder builder = new();
		for (int i = 0; i < 9; i++)
		{
			builder.Append(stacks[i].Peek());
		}
		return builder.ToString();
	}

	public override string SolvePart2()
	{
		Regex regex = new(@"\s*move (\d+) from (\d+) to (\d+)\s*");
		Stack<char>[] stacks = PrepStacks();
		foreach (string line in InputLines.Skip(10))
		{
			Match match = regex.Match(line);
			int count = int.Parse(match.Groups[1].ValueSpan);
			int from = int.Parse(match.Groups[2].ValueSpan) - 1;
			int to = int.Parse(match.Groups[3].ValueSpan) - 1;
			Stack<char> temp = new();
			for (int i = 0; i < count; i++)
			{
				temp.Push(stacks[from].Pop());
			}
			for (int i = 0; i < count; i++)
			{
				stacks[to].Push(temp.Pop());
			}
		}
		StringBuilder builder = new();
		for (int i = 0; i < 9; i++)
		{
			builder.Append(stacks[i].Peek());
		}
		return builder.ToString();
	}
}
