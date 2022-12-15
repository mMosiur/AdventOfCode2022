using System.Diagnostics;
using AdventOfCode.Year2022.Day13.Packets;

namespace AdventOfCode.Year2022.Day13;

sealed class PacketAnalyzer
{
	public IReadOnlyList<Packet> Packets { get; }

	public static bool IsInCorrectOrder(PacketPair packetPair)
	{
		int result = packetPair.First.CompareTo(packetPair.Second);
		return result < 0;
	}

	public PacketAnalyzer(IEnumerable<PacketPair> packetPairs)
	{
		List<Packet> packets = packetPairs.TryGetNonEnumeratedCount(out int count)
			? new(count * 2)
			: new();
		foreach ((Packet first, Packet second) in packetPairs)
		{
			packets.Add(first);
			packets.Add(second);
		}
		Packets = packets;
	}

	public PacketAnalyzer(IEnumerable<Packet> packetPairs)
	{
		Packets = packetPairs.ToList();
	}

	public int GetDecoderKey(IEnumerable<Packet> dividerPackets)
	{
		int decoderKey = 1;
		List<Packet> packets = dividerPackets.TryGetNonEnumeratedCount(out int dividerPacketsCount)
			? new(Packets.Count + dividerPacketsCount)
			: new(Packets.Count);
		packets.AddRange(Packets);
		packets.AddRange(dividerPackets);
		packets.Sort();
		foreach (Packet dividerPacket in dividerPackets)
		{
			int index = packets.IndexOf(dividerPacket) + 1; // Indices start at 1 in this puzzle
			if (index == 0)
			{
				throw new UnreachableException("Shouldn't happen.");
			}
			decoderKey *= index;
		}
		return decoderKey;
	}
}
