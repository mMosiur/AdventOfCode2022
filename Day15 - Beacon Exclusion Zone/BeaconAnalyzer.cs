namespace AdventOfCode.Year2022.Day15;

sealed class BeaconAnalyzer
{
	private readonly long _tuningFrequencyXMultiplier;

	public BeaconAnalyzer(long tuningFrequencyXMultiplier)
	{
		_tuningFrequencyXMultiplier = tuningFrequencyXMultiplier;
	}

	public long CalculateTuningFrequencyAt(Point position)
	{
		return (position.X * _tuningFrequencyXMultiplier) + position.Y;
	}
}
