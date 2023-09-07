using AdventOfCode.Common.EnumerableExtensions;

namespace AdventOfCode.Year2022.Day16;

internal sealed class ValveMapStateEncoder
{
	public static ulong BaseState => 0;

	private readonly ValveMap _valveMap;
	private readonly int _valveCount;

	public ValveMapStateEncoder(ValveMap valveMap)
	{
		_valveMap = valveMap;
		_valveCount = valveMap.Valves.Count;
	}

	private static uint GetBitMask(int index) => (uint)(1 << index);

	private void AssignIndexIfMissing(Valve valve)
	{
		if (valve.Index >= 0) return;
		valve.Index = _valveMap.Valves.WithIndex().First(v => v.Item == valve).Index;
	}

	public bool IsOpen(ulong currentState, int valveIndex)
	{
		return (currentState & GetBitMask(valveIndex)) != 0;
	}

	public bool IsOpen(ulong currentState, Valve valve)
	{
		AssignIndexIfMissing(valve);
		return IsOpen(currentState, valve.Index);
	}

	public bool IsClosed(ulong currentState, int valveIndex)
	{
		return !IsOpen(currentState, valveIndex);
	}

	public bool IsClosed(ulong currentState, Valve valve)
	{
		AssignIndexIfMissing(valve);
		return IsClosed(currentState, valve.Index);
	}

	public ulong OpenValve(ulong currentState, int valveIndex)
	{
		return currentState | GetBitMask(valveIndex);
	}

	public ulong OpenValve(ulong currentState, Valve valve)
	{
		AssignIndexIfMissing(valve);
		return OpenValve(currentState, valve.Index);
	}

	public bool AreAllValvesOpen(ulong currentState)
	{
		return currentState == (ulong)(1 << _valveCount) - 1;
	}
}
