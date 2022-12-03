using AdventOfCode.Year2022.Day03.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day03.Rucksacks;

sealed class RucksackAnalyzer
{
	private readonly IReadOnlyList<Rucksack> _rucksacks;

	public RucksackAnalyzer(IReadOnlyList<Rucksack> rucksacks)
	{
		_rucksacks = rucksacks;
	}

	public static int CalculateItemPriority(ItemType itemType)
	{
		return (char)itemType switch
		{
			>= 'a' and <= 'z' => itemType - 96,
			>= 'A' and <= 'Z' => itemType - 38,
			_ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, "Invalid item type.")
		};
	}

	public static ItemType FindCommonItemTypeBetweenCompartments(Rucksack rucksack)
	{
		HashSet<ItemType> set = rucksack.Compartment1.ToHashSet();
		set.IntersectWith(rucksack.Compartment2.ToHashSet());
		try
		{
			return set.Single();
		}
		catch (InvalidOperationException e)
		{
			throw new InvalidOperationException($"Rucksack does not have exactly one common item type.", e);
		}
	}

	public static ItemType FindCommonItemTypeBetweenRucksacks(IEnumerable<Rucksack> rucksacks)
	{
		HashSet<ItemType> set = rucksacks.First().Items.ToHashSet();
		foreach (Rucksack rucksack in rucksacks.Skip(1))
		{
			set.IntersectWith(rucksack.Items.ToHashSet());
		}
		try
		{
			return set.Single();
		}
		catch (InvalidOperationException e)
		{
			throw new InvalidOperationException($"Rucksacks do not have exactly one common item type.", e);
		}
	}

	public int SumCommonItemTypePrioritiesInRucksack()
	{
		return _rucksacks
			.Select(FindCommonItemTypeBetweenCompartments)
			.Select(CalculateItemPriority)
			.Sum();
	}

	public int SumCommonItemTypePrioritiesInRucksackGroupOfSize(int groupSize)
	{
		return _rucksacks
			.Group(groupSize)
			.Select(FindCommonItemTypeBetweenRucksacks)
			.Select(CalculateItemPriority)
			.Sum();
	}
}
