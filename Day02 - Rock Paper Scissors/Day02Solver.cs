using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 2;
	public override string Title => "Rock Paper Scissors";

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(new Day02SolverOptions())
	{
	}

	private int CalculateScore1(string s)
	{
		string[] parts = s.Split();
		string elf = parts[0] switch
		{
			"A" => "rock",
			"B" => "paper",
			"C" => "scissors",
			_ => throw new InputException($"Unknown elf type \"{parts[0]}\".")
		};
		string mine = parts[1] switch
		{
			"X" => "rock",
			"Y" => "paper",
			"Z" => "scissors",
			_ => throw new InputException($"Unknown mine type \"{parts[1]}\".")
		};
		int score = mine switch
		{
			"rock" => 1,
			"paper" => 2,
			"scissors" => 3,
			_ => throw new UnreachableException($"Unknown mine type \"{mine}\".")
		};
		score += (mine, elf) switch
		{
			("rock", "rock") => 3,
			("rock", "paper") => 0,
			("rock", "scissors") => 6,
			("paper", "rock") => 6,
			("paper", "paper") => 3,
			("paper", "scissors") => 0,
			("scissors", "rock") => 0,
			("scissors", "paper") => 6,
			("scissors", "scissors") => 3,
			_ => throw new UnreachableException($"Unknown mine type \"{mine}\" and elf type \"{elf}\".")
		};
		return score;
	}

	private int CalculateScore2(string s)
	{
		string[] parts = s.Split();
		string elf = parts[0] switch
		{
			"A" => "rock",
			"B" => "paper",
			"C" => "scissors",
			_ => throw new InputException($"Unknown elf type \"{parts[0]}\".")
		};
		string mine = (elf, parts[1]) switch
		{
			("rock", "X") => "scissors",
			("paper", "X") => "rock",
			("scissors", "X") => "paper",
			("rock", "Y") => "rock",
			("paper", "Y") => "paper",
			("scissors", "Y") => "scissors",
			("rock", "Z") => "paper",
			("paper", "Z") => "scissors",
			("scissors", "Z") => "rock",
			_ => throw new InputException($"Unknown mine type \"{parts[1]}\".")
		};
		int score = mine switch
		{
			"rock" => 1,
			"paper" => 2,
			"scissors" => 3,
			_ => throw new UnreachableException($"Unknown mine type \"{mine}\".")
		};
		score += (mine, elf) switch
		{
			("rock", "rock") => 3,
			("rock", "paper") => 0,
			("rock", "scissors") => 6,
			("paper", "rock") => 6,
			("paper", "paper") => 3,
			("paper", "scissors") => 0,
			("scissors", "rock") => 0,
			("scissors", "paper") => 6,
			("scissors", "scissors") => 3,
			_ => throw new UnreachableException($"Unknown mine type \"{mine}\" and elf type \"{elf}\".")
		};
		return score;
	}

	public override string SolvePart1()
	{
		int sum = InputLines.Select(CalculateScore1).Sum();
		return $"{sum}";
	}

	public override string SolvePart2()
	{
		int sum = InputLines.Select(CalculateScore2).Sum();
		return $"{sum}";
	}
}
