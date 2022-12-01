using AdventOfCode.Year2022.Day01;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "01")]
[Trait("Day", "1")]
public class Day01Tests : BaseDayTests<Day01Solver, Day01SolverOptions>
{
	public override string DayInputsDirectory => "Day01";

	protected override Day01Solver CreateSolver(Day01SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "24000")]
	[InlineData("my-input.txt", "70374")]
	public override void TestPart1(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "45000")]
	[InlineData("my-input.txt", "204610")]
	public override void TestPart2(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
