namespace AdventOfCode.Year2022.Day13.Packets;

sealed class Packet : IComparable<Packet>
{
	public PacketList Root { get; }

	public static implicit operator Packet(PacketList packetList) => new(packetList);

	public Packet(PacketList root)
	{
		ArgumentNullException.ThrowIfNull(root);
		Root = root;
	}

	public int CompareTo(Packet? other)
	{
		ArgumentNullException.ThrowIfNull(other);
		return Root.CompareTo(other.Root);
	}

	public static Packet Parse(string input)
	{
		ArgumentNullException.ThrowIfNull(input);
		try
		{
			PacketList packetList = PacketList.Parse(input, out int consumed);
			if (consumed != input.Length)
			{
				throw new FormatException($"Input string was not fully consumed.");
			}
			return new Packet(packetList);
		}
		catch (SystemException e)
		{
			throw new FormatException("Could not parse packet.", e);
		}
	}
}
