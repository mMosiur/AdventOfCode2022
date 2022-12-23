using AdventOfCode.Year2022.Day14;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "14")]
public class Day14Tests : BaseDayTests<Day14Solver, Day14SolverOptions>
{
	public override string DayInputsDirectory => "Day14";

	protected override Day14Solver CreateSolver(Day14SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "24")]
	[InlineData("my-input.txt", "858")]
	public override void TestPart1(string inputFilename, string expectedResult, Day14SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "93")]
	[InlineData("my-input.txt", "26845")]
	public override void TestPart2(string inputFilename, string expectedResult, Day14SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
