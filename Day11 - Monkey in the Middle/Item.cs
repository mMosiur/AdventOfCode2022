namespace AdventOfCode.Year2022.Day11;

class Item : ICloneable
{
	protected ulong _worryLevel;

	public virtual ulong WorryLevel
	{
		get => _worryLevel;
		set => _worryLevel = value;
	}

	public Item(ulong worryLevel)
	{
		WorryLevel = worryLevel;
	}

	public virtual Item Clone() => new(WorryLevel);
	object ICloneable.Clone() => Clone();
}
