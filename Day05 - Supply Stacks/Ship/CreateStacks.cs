using System.Collections;

namespace AdventOfCode.Year2022.Day05.Ship;

sealed class CrateStacks : IEnumerable<Stack<Crate>>, ICloneable
{
	private readonly Dictionary<int, Stack<Crate>> _crateStacks;

	public CrateStacks(Dictionary<int, Stack<Crate>> crateStacks)
	{
		_crateStacks = crateStacks;
	}

	public Stack<Crate> this[int number] => _crateStacks[number];

	private static Stack<Crate> CloneStack(Stack<Crate> stack)
	{
		Crate[] arr = new Crate[stack.Count];
		stack.CopyTo(arr, 0);
		Array.Reverse(arr);
		return new Stack<Crate>(arr);
	}

	public CrateStacks Clone()
	{
		return new(_crateStacks.ToDictionary(
			kvp => kvp.Key,
			kvp => CloneStack(kvp.Value)
		));
	}

	object ICloneable.Clone() => Clone();

	public IEnumerator<Stack<Crate>> GetEnumerator() => _crateStacks.Values.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
