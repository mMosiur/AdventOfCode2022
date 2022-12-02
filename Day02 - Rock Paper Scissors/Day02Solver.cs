using AdventOfCode.Abstractions;
using AdventOfCode.Year2022.Day02.RockPaperScissors;

namespace AdventOfCode.Year2022.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 2;
	public override string Title => "Rock Paper Scissors";

	private readonly EncryptedStrategyGuide _encryptedStrategyGuide;

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
		try
		{
			_encryptedStrategyGuide = EncryptedStrategyGuide.Parse(Input);
		}
		catch (FormatException e)
		{
			throw new InputException(e.Message);
		}
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(new Day02SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int result = _encryptedStrategyGuide.CalculateScoresAssumingValues().Sum();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int result = _encryptedStrategyGuide.CalculateScoresAssumingResults().Sum();
		return $"{result}";
	}
}
