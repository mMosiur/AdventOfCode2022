using AdventOfCode.Year2022.Day02;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "02")]
[Trait("Day", "2")]
public class Day02Tests : BaseDayTests<Day02Solver, Day02SolverOptions>
{
	public override string DayInputsDirectory => "Day02";

	protected override Day02Solver CreateSolver(Day02SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "15")]
	[InlineData("my-input.txt", "11150")]
	public override void TestPart1(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "12")]
	[InlineData("my-input.txt", "8295")]
	public override void TestPart2(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
