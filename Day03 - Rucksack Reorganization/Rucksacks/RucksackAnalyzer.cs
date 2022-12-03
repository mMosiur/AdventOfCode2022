namespace AdventOfCode.Year2022.Day03.Rucksacks;

class RucksackAnalyzer
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

	public int SumCommonItemTypesInRucksackPriorities()
	{
		return _rucksacks
			.Select(FindCommonItemTypeBetweenCompartments)
			.Select(CalculateItemPriority)
			.Sum();
	}
}
