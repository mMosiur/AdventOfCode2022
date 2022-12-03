using System.Collections;

namespace AdventOfCode.Year2022.Day03.Rucksacks;

readonly partial struct Rucksack
{
	public readonly struct Compartment : IReadOnlyCollection<ItemType>
	{
		private readonly ItemType[] _allItems;
		private readonly int _startIndex;
		private readonly int _endIndex;

		public int Count => _endIndex - _startIndex;

		public Compartment(ItemType[] allItems, int startIndex, int endIndex)
		{
			_allItems = allItems;
			_startIndex = startIndex;
			_endIndex = endIndex;
		}

		public IEnumerator<ItemType> GetEnumerator()
		{
			for (int i = _startIndex; i < _endIndex; i++)
			{
				yield return _allItems[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
