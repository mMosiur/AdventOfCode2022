using System.Collections;
using System.Diagnostics;

namespace AdventOfCode.Year2022.Day15;

sealed class MultiRange : IEnumerable<int>
{
	private readonly List<Range> _ranges = new();

	public IReadOnlyList<Range> SubRanges => _ranges;
	public int Count => _ranges.Sum(r => r.Count);

	public MultiRange()
	{
	}

	public MultiRange(IEnumerable<int> values) : this()
	{
		ArgumentNullException.ThrowIfNull(values);
		foreach (int value in values)
		{
			Add(value);
		}
	}

	public MultiRange(IEnumerable<Range> ranges) : this()
	{
		ArgumentNullException.ThrowIfNull(ranges);
		foreach (Range range in ranges)
		{
			Add(range);
		}
	}

	private static (Range? LowerPart, Range? UpperPart) SplitByRemoving(Range range, Range other)
	{
		Range? lowerPart = null, upperPart = null;
		if (other.Start > range.Start)
		{
			lowerPart = new Range(range.Start, other.Start - 1);
		}
		if (other.End < range.End)
		{
			upperPart = new Range(other.End + 1, range.End);
		}
		return (lowerPart, upperPart);
	}

	private static (Range? LowerPart, Range? UpperPart) SplitByRemoving(Range range, int value)
	{
		Range? lowerPart = null, upperPart = null;
		if (value > range.Start)
		{
			lowerPart = new Range(range.Start, value - 1);
		}
		if (value < range.End)
		{
			upperPart = new Range(value + 1, range.End);
		}
		return (lowerPart, upperPart);
	}

	public void Add(int value) => Add(new Range(value, value));

	public void Add(Range range)
	{
		if (_ranges.Count == 0)
		{
			_ranges.Add(range);
			return;
		}
		int startAfterIndex = -1;
		while (startAfterIndex + 1 < _ranges.Count && _ranges[startAfterIndex + 1].End < range.Start) startAfterIndex++;
		int endBeforeIndex = _ranges.Count;
		while (endBeforeIndex - 1 >= 0 && _ranges[endBeforeIndex - 1].Start > range.End) endBeforeIndex--;
		int overlapCount = endBeforeIndex - startAfterIndex - 1;
		int newRangeIndex = startAfterIndex + 1;
		if (overlapCount == 0)
		{
			_ranges.Insert(newRangeIndex, range);
			return;
		}
		for (int i = newRangeIndex; i < endBeforeIndex; i++)
		{
			Range other = _ranges[i];
			range = new Range(Math.Min(range.Start, other.Start), Math.Max(range.End, other.End));
		}
		_ranges.RemoveRange(newRangeIndex, overlapCount);
		_ranges.Insert(newRangeIndex, range);
	}

	public void Remove(int value)
	{
		if (_ranges.Count == 0)
		{
			return;
		}
		int removeIndex = 0;
		while (removeIndex < _ranges.Count && _ranges[removeIndex].End < value) removeIndex++;
		if (removeIndex >= _ranges.Count || _ranges[removeIndex].Start > value)
		{
			return;
		}
		Range range = _ranges[removeIndex];
		(Range? newLowerPart, Range? newUpperPart) = SplitByRemoving(range, value);
		switch (newLowerPart.HasValue, newUpperPart.HasValue)
		{
			case (false, false):
				_ranges.RemoveAt(removeIndex);
				break;
			case (false, true):
				_ranges[removeIndex] = newUpperPart!.Value;
				break;
			case (true, false):
				_ranges[removeIndex] = newLowerPart!.Value;
				break;
			case (true, true):
				_ranges[removeIndex] = newLowerPart!.Value;
				_ranges.Insert(removeIndex + 1, newUpperPart!.Value);
				break;
		}
	}

	public void Remove(Range range)
	{
		if (_ranges.Count == 0)
		{
			return;
		}
		int removeFrom = 0;
		while (removeFrom < _ranges.Count && _ranges[removeFrom].End < range.Start) removeFrom++;
		int removeTo = _ranges.Count - 1;
		while (removeTo >= 0 && _ranges[removeTo].Start > range.End) removeTo--;
		int overlapCount = removeTo - removeFrom + 1;
		if (overlapCount == 0)
		{
			return;
		}
		if (overlapCount == 1)
		{
			Range other = _ranges[removeFrom];
			(Range? newLowerPart, Range? newUpperPart) = SplitByRemoving(other, range);
			switch (newLowerPart.HasValue, newUpperPart.HasValue)
			{
				case (false, false):
					_ranges.RemoveAt(removeFrom);
					break;
				case (false, true):
					_ranges[removeFrom] = newUpperPart!.Value;
					break;
				case (true, false):
					_ranges[removeFrom] = newLowerPart!.Value;
					break;
				case (true, true):
					_ranges[removeFrom] = newLowerPart!.Value;
					_ranges.Insert(removeFrom + 1, newUpperPart!.Value);
					break;
			}
		}
		else // OverlapCount >= 2
		{
			if (overlapCount > 2)
			{
				_ranges.RemoveRange(removeFrom + 1, overlapCount - 2);
				removeTo = removeFrom + 1;
			}
			Range first = _ranges[removeFrom];
			Range last = _ranges[removeTo];
			(Range? newLowerPart, Range? trash1) = SplitByRemoving(first, range);
			if (trash1 is not null) throw new UnreachableException("There should be no upper part of that removal");
			(Range? trash2, Range? newUpperPart) = SplitByRemoving(last, range);
			if (trash2 is not null) throw new UnreachableException("There should be no upper part of that removal");
			switch (newLowerPart.HasValue, newUpperPart.HasValue)
			{
				case (false, false):
					_ranges.RemoveRange(removeFrom, 2);
					break;
				case (false, true):
					_ranges[removeFrom] = newUpperPart!.Value;
					_ranges.RemoveAt(removeTo);
					break;
				case (true, false):
					_ranges[removeFrom] = newLowerPart!.Value;
					_ranges.RemoveAt(removeTo);
					break;
				case (true, true):
					_ranges[removeFrom] = newLowerPart!.Value;
					_ranges[removeTo] = newUpperPart!.Value;
					break;
			}
		}
	}

	public IEnumerator<int> GetEnumerator() => _ranges.SelectMany(r => r).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
