namespace AdventOfCode.Year2022.Day06;

sealed class SignalMarkerLocator
{
	private readonly IReadOnlyDictionary<MarkerType, int> _markerSequenceLengths;

	public SignalMarkerLocator(IReadOnlyDictionary<MarkerType, int> markerSequenceLengths)
	{
		_markerSequenceLengths = markerSequenceLengths;
	}

	private static int FindNonRepeatingSequenceLocation(TextReader signalReader, int sequenceLength)
	{
		char[] buffer = new char[sequenceLength];
		if (signalReader.ReadBlock(buffer, 0, sequenceLength) < sequenceLength)
		{
			throw new InvalidOperationException("Signal is too short to contain a non-repeating sequence.");
		}
		int index = sequenceLength;
		while (buffer.Distinct().Count() < sequenceLength)
		{
			int nextChar = signalReader.Read();
			if (nextChar == -1)
			{
				throw new InvalidOperationException("Signal is too short to contain a non-repeating sequence.");
			}
			buffer[index % sequenceLength] = (char)nextChar;
			index++;
		}
		return index;
	}

	public int FindMarkerLocation(TextReader signalReader, MarkerType markerType)
	{
		try
		{
			int sequenceLength = _markerSequenceLengths[markerType];
			return FindNonRepeatingSequenceLocation(signalReader, sequenceLength);
		}
		catch (KeyNotFoundException e)
		{
			throw new ArgumentException($"Sequence length for marker type '{markerType}' not specified.", e);
		}
	}
}
