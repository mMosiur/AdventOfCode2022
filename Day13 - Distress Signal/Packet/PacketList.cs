using System.Diagnostics;

namespace AdventOfCode.Year2022.Day13.Packet;

sealed class PacketList : PacketItem, IComparable<PacketList>
{
	public List<PacketItem> Items { get; }

	public PacketList(IEnumerable<PacketItem> items)
	{
		Items = items.ToList();
	}

	public static PacketList FromSingleItem(PacketItem item)
	{
		return new PacketList(new List<PacketItem>(1) { item });
	}

	public int CompareTo(PacketList? other)
	{
		ArgumentNullException.ThrowIfNull(other);
		for (int i = 0; i < Math.Min(Items.Count, other.Items.Count); i++)
		{
			PacketItem first = Items[i];
			PacketItem second = other.Items[i];
			if (first is PacketInteger firstInt)
			{
				if (second is PacketInteger secondInt)
				{
					int result = firstInt.CompareTo(secondInt);
					if (result != 0) return result;
				}
				else if (second is PacketList secondList)
				{
					PacketList newList = FromSingleItem(first);
					Items[i] = newList;
					int result = newList.CompareTo(secondList);
					if (result != 0) return result;
				}
				else throw new UnreachableException();
			}
			else if (first is PacketList firstList)
			{
				if (second is PacketInteger secondInt)
				{
					PacketList newList = FromSingleItem(secondInt);
					other.Items[i] = newList;
					int result = firstList.CompareTo(newList);
					if (result != 0) return result;
				}
				else if (second is PacketList secondList)
				{
					int result = firstList.CompareTo(secondList);
					if (result != 0) return result;
				}
				else throw new UnreachableException();
			}
			else throw new UnreachableException();
		}
		return Items.Count.CompareTo(other.Items.Count);
	}

	public static PacketList Parse(ReadOnlySpan<char> span, out int consumed)
	{
		ReadOnlySpan<char> OpenBracket = "[";
		ReadOnlySpan<char> CloseBracket = "]";
		ReadOnlySpan<char> Separator = ",";

		List<PacketItem> items = new();

		consumed = 0;
		ReadOnlySpan<char> s = span.TrimStart();
		if (s.Length == 0)
		{
			throw new FormatException("Expected a list.");
		}
		if (!s.StartsWith(OpenBracket))
		{
			throw new FormatException($"Expected '{OpenBracket}'.");
		}
		s = s[OpenBracket.Length..];
		consumed += OpenBracket.Length;

		while (true)
		{
			if (s.StartsWith(CloseBracket))
			{
				s = s[CloseBracket.Length..];
				consumed += CloseBracket.Length;
				return new PacketList(items);
			}
			else if (s.StartsWith(OpenBracket))
			{
				try
				{
					PacketList subList = Parse(s, out int subConsumed);
					consumed += subConsumed;
					items.Add(subList);
					s = s[subConsumed..];
				}
				catch (Exception e)
				{
					throw new FormatException($"Expected a list, got '{s}'.", e);
				}
			}
			else if (s.StartsWith(Separator))
			{
				s = s[Separator.Length..];
				consumed += Separator.Length;
			}
			else
			{
				try
				{
					PacketInteger subInt = PacketInteger.Parse(s, out int subConsumed);
					consumed += subConsumed;
					items.Add(subInt);
					s = s[subConsumed..];
				}
				catch (Exception e)
				{
					throw new FormatException($"Expected an integer, got '{s}'.", e);
				}
			}
		}
	}
}
