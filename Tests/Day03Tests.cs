using AdventOfCode.Year2022.Day03;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "03")]
[Trait("Day", "3")]
public class Day03Tests : BaseDayTests<Day03Solver, Day03SolverOptions>
{
	public override string DayInputsDirectory => "Day03";

	protected override Day03Solver CreateSolver(Day03SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "157")]
	[InlineData("my-input.txt", "8109")]
	public override void TestPart1(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "70")]
	[InlineData("my-input.txt", "2738")]
	public override void TestPart2(string inputFilename, string expectedResult, Day03SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
