using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2022.Day13.Packets;

readonly struct PacketPair
{
	public required Packet First { get; init; }
	public required Packet Second { get; init; }

	public void Deconstruct(out Packet first, out Packet second)
	{
		first = First;
		second = Second;
	}

	[SetsRequiredMembers]
	public PacketPair(Packet first, Packet second)
	{
		ArgumentNullException.ThrowIfNull(first);
		ArgumentNullException.ThrowIfNull(second);
		First = first;
		Second = second;
	}
}
