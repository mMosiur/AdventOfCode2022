using System.Diagnostics;

namespace AdventOfCode.Year2022.Day02.RockPaperScissors;

enum Hand
{
	Rock = 1,
	Paper = 2,
	Scissors = 3
}

static class HandExtensions
{
	public static Hand GetWinningCounter(this Hand hand)
	{
		return hand switch
		{
			Hand.Rock => Hand.Paper,
			Hand.Paper => Hand.Scissors,
			Hand.Scissors => Hand.Rock,
			_ => throw new UnreachableException($"Unknown hand \"{hand}\".")
		};
	}

	public static Hand GetLosingCounter(this Hand hand)
	{
		return hand switch
		{
			Hand.Rock => Hand.Scissors,
			Hand.Paper => Hand.Rock,
			Hand.Scissors => Hand.Paper,
			_ => throw new UnreachableException($"Unknown hand \"{hand}\".")
		};
	}

	public static Hand GetDrawingCounter(this Hand hand)
	{
		return hand switch
		{
			Hand.Rock => Hand.Rock,
			Hand.Paper => Hand.Paper,
			Hand.Scissors => Hand.Scissors,
			_ => throw new UnreachableException($"Unknown hand \"{hand}\".")
		};
	}
}
