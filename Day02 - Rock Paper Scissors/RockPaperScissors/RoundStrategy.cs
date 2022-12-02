using System.Diagnostics;

namespace AdventOfCode.Year2022.Day02.RockPaperScissors;

struct RoundStrategy
{
	public string Opponent { get; }
	public string Player { get; }

	private static readonly string[] _validOpponentValues = new[] { "A", "B", "C" };
	private static readonly string[] _validPlayerValues = new[] { "X", "Y", "Z" };

	public RoundStrategy(string opponent, string player)
	{
		ArgumentException.ThrowIfNullOrEmpty(opponent);
		ArgumentException.ThrowIfNullOrEmpty(player);
		opponent = opponent.Trim();
		if (!_validOpponentValues.Contains(opponent))
		{
			throw new ArgumentException($"Invalid opponent value \"{opponent}\".", nameof(opponent));
		}
		player = player.Trim();
		if (!_validPlayerValues.Contains(player))
		{
			throw new ArgumentException($"Invalid player value \"{player}\".", nameof(player));
		}
		Opponent = opponent;
		Player = player;
	}

	private static Hand OpponentHand(string value)
	{
		return value switch
		{
			"A" => Hand.Rock,
			"B" => Hand.Paper,
			"C" => Hand.Scissors,
			_ => throw new UnreachableException($"Unknown opponent value \"{value}\".")
		};
	}

	private static Hand PlayerHand(string value)
	{
		return value switch
		{
			"X" => Hand.Rock,
			"Y" => Hand.Paper,
			"Z" => Hand.Scissors,
			_ => throw new UnreachableException($"Unknown player value \"{value}\".")
		};
	}

	private static RoundResult GetRoundResult(Hand opponent, Hand player)
	{
		return (opponent, player) switch
		{
			(Hand.Rock, Hand.Rock) => RoundResult.Draw,
			(Hand.Rock, Hand.Paper) => RoundResult.Win,
			(Hand.Rock, Hand.Scissors) => RoundResult.Loss,
			(Hand.Paper, Hand.Rock) => RoundResult.Loss,
			(Hand.Paper, Hand.Paper) => RoundResult.Draw,
			(Hand.Paper, Hand.Scissors) => RoundResult.Win,
			(Hand.Scissors, Hand.Rock) => RoundResult.Win,
			(Hand.Scissors, Hand.Paper) => RoundResult.Loss,
			(Hand.Scissors, Hand.Scissors) => RoundResult.Draw,
			_ => throw new UnreachableException($"Unknown round result \"{opponent}\" vs \"{player}\".")
		};
	}

	private static int CalculateHandScore(Hand hand)
	{
		return hand switch
		{
			Hand.Rock => 1,
			Hand.Paper => 2,
			Hand.Scissors => 3,
			_ => throw new UnreachableException($"Unknown hand \"{hand}\".")
		};
	}

	private static int CalculateResultScore(RoundResult result)
	{
		return result switch
		{
			RoundResult.Loss => 0,
			RoundResult.Draw => 3,
			RoundResult.Win => 6,
			_ => throw new UnreachableException($"Unknown round result \"{result}\".")
		};
	}

	private static int CalculateScore(Hand hand, RoundResult result)
	{
		return CalculateHandScore(hand) + CalculateResultScore(result);
	}

	public int CalculateScoreAssumingValue()
	{
		Hand opponentHand = OpponentHand(Opponent);
		Hand playerHand = PlayerHand(Player);
		RoundResult roundResult = GetRoundResult(opponentHand, playerHand);
		return CalculateScore(playerHand, roundResult);
	}

	public int CalculateScoreAssumingResult()
	{
		Hand opponentHand = OpponentHand(Opponent);
		(RoundResult result, Hand playerHand) = Player switch
		{
			"X" => (RoundResult.Loss, opponentHand.GetLosingCounter()),
			"Y" => (RoundResult.Draw, opponentHand.GetDrawingCounter()),
			"Z" => (RoundResult.Win, opponentHand.GetWinningCounter()),
			_ => throw new UnreachableException($"Unknown player value \"{Player}\".")
		};
		return CalculateScore(playerHand, result);
	}
}
