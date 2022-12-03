namespace AdventOfCode.Year2022.Day03.Rucksacks;

readonly partial struct Rucksack
{
	private readonly ItemType[] _items;

	public IReadOnlyCollection<ItemType> Items => _items;
	public Compartment Compartment1 => new(_items, 0, _items.Length / 2);
	public Compartment Compartment2 => new(_items, _items.Length / 2, _items.Length);

	public Rucksack(string items)
	{
		ArgumentException.ThrowIfNullOrEmpty(items);
		if (items.Length % 2 != 0)
		{
			throw new ArgumentException("Rucksack needs even number of items.", nameof(items));
		}
		_items = items.Select(c => (ItemType)c).ToArray();
	}

	public static Rucksack Parse(string s)
	{
		ArgumentException.ThrowIfNullOrEmpty(s);
		s = s.Trim();
		return new Rucksack(s);
	}
}
