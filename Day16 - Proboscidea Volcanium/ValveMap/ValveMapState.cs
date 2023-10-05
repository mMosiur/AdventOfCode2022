using System.Diagnostics;

namespace AdventOfCode.Year2022.Day16;

internal readonly record struct ValveMapState(ulong EncodedState)
{
	public ValveMapState() : this(0)
	{
	}

	private static uint GetIndexBitMask(int index) => (uint)(1 << index);

	public bool IsOpen(int valveIndex)
	{
		return (EncodedState & GetIndexBitMask(valveIndex)) != 0;
	}

	public bool IsOpen(Valve valve)
	{
		Debug.Assert(valve.Index >= 0);
		return IsOpen(valve.Index);
	}

	public bool IsClosed(int valveIndex)
	{
		return !IsOpen(valveIndex);
	}

	public bool IsClosed(Valve valve)
	{
		Debug.Assert(valve.Index >= 0);
		return IsClosed(valve.Index);
	}

	public ValveMapState WithOpenedValve(int valveIndex)
	{
		ulong newEncodedState = EncodedState | GetIndexBitMask(valveIndex);
		return new(newEncodedState);
	}

	public ValveMapState WithOpenedValve(Valve valve)
	{
		Debug.Assert(valve.Index >= 0);
		return WithOpenedValve(valve.Index);
	}

	public ValveMapState WithClosedValve(int valveIndex)
	{
		ulong newEncodedState = EncodedState & ~GetIndexBitMask(valveIndex);
		return new(newEncodedState);
	}

	public ValveMapState WithClosedValve(Valve valve)
	{
		Debug.Assert(valve.Index >= 0);
		return WithClosedValve(valve.Index);
	}
}
