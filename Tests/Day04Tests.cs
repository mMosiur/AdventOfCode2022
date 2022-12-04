using AdventOfCode.Year2022.Day04;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "04")]
[Trait("Day", "4")]
public class Day04Tests : BaseDayTests<Day04Solver, Day04SolverOptions>
{
	public override string DayInputsDirectory => "Day04";

	protected override Day04Solver CreateSolver(Day04SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "2")]
	[InlineData("my-input.txt", "538")]
	public override void TestPart1(string inputFilename, string expectedResult, Day04SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "4")]
	[InlineData("my-input.txt", "792")]
	public override void TestPart2(string inputFilename, string expectedResult, Day04SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
