using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2022.Day16;

internal record struct ValveTunnel
{
	public required Valve To { get; init; }
	public int Distance { get; init; } = 1;

	[SetsRequiredMembers]
	public ValveTunnel(Valve to, int distance = 1)
	{
		To = to;
		Distance = distance;
	}
}
