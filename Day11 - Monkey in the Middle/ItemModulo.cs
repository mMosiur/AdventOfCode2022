namespace AdventOfCode.Year2022.Day11;

sealed class ItemModulo : Item
{
	private readonly uint _modulus = uint.MaxValue;

	public override ulong WorryLevel
	{
		get => _worryLevel;
		set => _worryLevel = value % _modulus;
	}

	public ItemModulo(ulong worryLevel, uint modulus) : base(worryLevel)
	{
		if (modulus == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(modulus), modulus, "Modulo must be greater than 0.");
		}
		_modulus = modulus;
		_worryLevel %= modulus;
	}

	public ItemModulo(Item item, uint modulus) : this(item.WorryLevel, modulus)
	{
	}

	public override Item Clone() => new ItemModulo(WorryLevel, _modulus);
}
