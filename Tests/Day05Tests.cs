using AdventOfCode.Year2022.Day05;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "05")]
[Trait("Day", "5")]
public class Day05Tests : BaseDayTests<Day05Solver, Day05SolverOptions>
{
	public override string DayInputsDirectory => "Day05";

	protected override Day05Solver CreateSolver(Day05SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "CMZ")]
	[InlineData("my-input.txt", "CFFHVVHNC")]
	public override void TestPart1(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "MCD")]
	[InlineData("my-input.txt", "FSZWBPTBG")]
	public override void TestPart2(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
