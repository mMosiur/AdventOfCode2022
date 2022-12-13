using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Year2022.Day13.Packet;

namespace AdventOfCode.Year2022.Day13;

public sealed class Day13Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 13;
	public override string Title => "Distress Signal";

	private readonly IReadOnlyList<(PacketList, PacketList)> _input;

	public Day13Solver(Day13SolverOptions options) : base(options)
	{
		_input = InputLines.SplitWithSeparator(string.Empty).Select(two =>
		{
			IEnumerator<string> it = two.GetEnumerator();
			if (!it.MoveNext())
			{
				throw new InputException("Expected a line.");
			}
			PacketList first = PacketList.Parse(it.Current, out int consumed);
			if (consumed != it.Current.Length)
			{
				throw new InputException($"Expected a first list, got '{it.Current}'.");
			}
			if (!it.MoveNext())
			{
				throw new InputException("Expected a second line.");
			}
			PacketList second = PacketList.Parse(it.Current, out consumed);
			if (consumed != it.Current.Length)
			{
				throw new InputException($"Expected a second list, got '{it.Current}'.");
			}
			if (it.MoveNext())
			{
				throw new InputException("Expected only two lines.");
			}
			return (first, second);
		}).ToList();
	}

	public Day13Solver(Action<Day13SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day13Solver() : this(new Day13SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int sum = 0;
		for (int i = 0; i < _input.Count; i++)
		{
			(PacketList first, PacketList second) = _input[i];
			if (first.CompareTo(second) < 0)
			{
				sum += i + 1;
			}
		}
		return $"{sum}";
	}

	public override string SolvePart2()
	{
		IReadOnlyList<PacketList> dividerPackets = new List<PacketList>(2)
		{
			PacketList.Parse("[[2]]", out _),
			PacketList.Parse("[[6]]", out _),
		};

		List<PacketList> packets = new((_input.Count * 2) + 2);
		foreach ((PacketList first, PacketList second) in _input)
		{
			packets.Add(first);
			packets.Add(second);
		}
		packets.AddRange(dividerPackets);
		packets.Sort();
		int decoderKey = 1;
		foreach (PacketList dividerPacket in dividerPackets)
		{
			int index = packets.IndexOf(dividerPacket) + 1;
			if (index == 0)
			{
				throw new UnreachableException();
			}
			decoderKey *= index;
		}
		return $"{decoderKey}";
	}
}
