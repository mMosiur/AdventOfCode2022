namespace AdventOfCode.Year2022.Day11;

sealed class Monkey : ICloneable
{
	public ushort Index { get; }
	public IList<Item> Items { get; }
	public ItemInspectOperation Operation { get; }
	public byte TestDivisor { get; }
	public ushort TestIfTrue { get; }
	public ushort TestIfFalse { get; }

	public Monkey(ushort index, IList<Item> startingItems, ItemInspectOperation operation, byte testDivisor, ushort testIfTrue, ushort testIfFalse)
	{
		Index = index;
		Items = startingItems;
		Operation = operation;
		TestDivisor = testDivisor;
		TestIfTrue = testIfTrue;
		TestIfFalse = testIfFalse;
	}
	public Monkey Clone(Func<Item, Item> itemCloneSelector)
	{
		IList<Item> newItems = Items.Select(itemCloneSelector).ToList();
		return new Monkey(Index, newItems, Operation, TestDivisor, TestIfTrue, TestIfFalse);
	}
	public Monkey Clone() => Clone((i) => i.Clone());
	object ICloneable.Clone() => Clone();

	public delegate ulong ItemInspectOperation(ulong item);
}
