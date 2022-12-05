namespace AdventOfCode.Year2022.Day05.Ship;

record struct RearrangementProcedureStep
{
	public required int CrateCount { get; init; }
	public required int FromStackNumber { get; init; }
	public required int ToStackNumber { get; init; }
}
