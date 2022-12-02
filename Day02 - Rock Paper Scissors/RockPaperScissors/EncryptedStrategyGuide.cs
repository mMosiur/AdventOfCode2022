using System.Collections;
using AdventOfCode.Common.StringExtensions;

namespace AdventOfCode.Year2022.Day02.RockPaperScissors;

class EncryptedStrategyGuide : IReadOnlyCollection<RoundStrategy>
{
	private readonly List<RoundStrategy> _encryptedStrategyGuide;

	public int Count => _encryptedStrategyGuide.Count;

	private EncryptedStrategyGuide(List<RoundStrategy> encryptedStrategyGuide)
	{
		_encryptedStrategyGuide = encryptedStrategyGuide;
	}

	public EncryptedStrategyGuide(IEnumerable<RoundStrategy> encryptedStrategyGuide)
	{
		_encryptedStrategyGuide = encryptedStrategyGuide.ToList();
	}

	public static EncryptedStrategyGuide Parse(string s)
	{
		List<RoundStrategy> encryptedStrategyGuide = new();
		foreach (string line in s.EnumerateLines())
		{
			try
			{
				string[] parts = line.Split(' ', StringSplitOptions.TrimEntries);
				if (parts.Length != 2)
				{
					throw new FormatException("Line did not consists of two values.");
				}
				encryptedStrategyGuide.Add(new RoundStrategy(parts[0], parts[1]));
			}
			catch (Exception e)
			{
				throw new FormatException($"Invalid line in strategy guide: \"{line}\".", e);
			}
		}
		return new EncryptedStrategyGuide(encryptedStrategyGuide);
	}

	/// <summary>
	/// Calculate round scores assuming second column is the player's hand.
	/// </summary>
	public IEnumerable<int> CalculateScoresAssumingValues()
	{
		return _encryptedStrategyGuide.Select(r => r.CalculateScoreAssumingValue());
	}

	/// <summary>
	/// Calculate round scores assuming first column is the player's hand.
	/// </summary>
	public IEnumerable<int> CalculateScoresAssumingResults()
	{
		return _encryptedStrategyGuide.Select(r => r.CalculateScoreAssumingResult());
	}

	public IEnumerator<RoundStrategy> GetEnumerator()
	{
		return _encryptedStrategyGuide.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
