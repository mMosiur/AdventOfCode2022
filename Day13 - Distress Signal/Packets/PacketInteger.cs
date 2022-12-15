namespace AdventOfCode.Year2022.Day13.Packets;

sealed class PacketInteger : PacketItem, IComparable<PacketInteger>
{
	public int Value { get; }

	public PacketInteger(int value)
	{
		Value = value;
	}

	public int CompareTo(PacketInteger? other)
	{
		ArgumentNullException.ThrowIfNull(other);
		return Value.CompareTo(other.Value);
	}

	public static PacketInteger Parse(ReadOnlySpan<char> span, out int consumed)
	{
		consumed = 0;
		ReadOnlySpan<char> s = span.TrimStart();
		if (s.Length == 0)
		{
			throw new FormatException();
		}
		consumed += span.Length - s.Length;
		int end = s[0] == '-' ? 1 : 0;
		while (end < s.Length && char.IsDigit(s[end]))
		{
			end++;
		}
		s = s[..end];
		try
		{
			int value = int.Parse(s);
			consumed += s.Length;
			return new PacketInteger(value);
		}
		catch (Exception e)
		{
			throw new FormatException($"Bad integer format: '{span}'.", e);
		}
	}
}
