using AdventOfCode.Year2022.Day13;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "13")]
public class Day13Tests : BaseDayTests<Day13Solver, Day13SolverOptions>
{
	public override string DayInputsDirectory => "Day13";

	protected override Day13Solver CreateSolver(Day13SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "13")]
	[InlineData("my-input.txt", "6568")]
	public override void TestPart1(string inputFilename, string expectedResult, Day13SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "140")]
	[InlineData("my-input.txt", "19493")]
	public override void TestPart2(string inputFilename, string expectedResult, Day13SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
