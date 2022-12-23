using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Year2022.Day13.Packets;

namespace AdventOfCode.Year2022.Day13;

static class InputParser
{
	public static IReadOnlyList<PacketPair> Parse(IEnumerable<string> inputLines)
	{
		return inputLines.SplitWithSeparator(string.Empty).Select<IReadOnlyList<string>, PacketPair>(group =>
		{
			IEnumerator<string> it = group.GetEnumerator();
			if (!it.MoveNext())
			{
				throw new InputException("Expected a line.");
			}
			Packet firstPacket = PacketList.Parse(it.Current, out int consumed);
			if (consumed != it.Current.Length)
			{
				throw new InputException($"Expected a first list, got '{it.Current}'.");
			}
			if (!it.MoveNext())
			{
				throw new InputException("Expected a second line.");
			}
			Packet secondPacket = PacketList.Parse(it.Current, out consumed);
			if (consumed != it.Current.Length)
			{
				throw new InputException($"Expected a second list, got '{it.Current}'.");
			}
			if (it.MoveNext())
			{
				throw new InputException("Expected only two lines.");
			}
			return new(firstPacket, secondPacket);
		}).ToList();
	}
}
