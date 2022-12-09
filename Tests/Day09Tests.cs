using AdventOfCode.Year2022.Day09;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "09")]
[Trait("Day", "9")]
public class Day09Tests : BaseDayTests<Day09Solver, Day09SolverOptions>
{
	public override string DayInputsDirectory => "Day09";

	protected override Day09Solver CreateSolver(Day09SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "13")]
	[InlineData("my-input.txt", "6470")]
	public override void TestPart1(string inputFilename, string expectedResult, Day09SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-1.txt", "1")]
	[InlineData("example-input-2.txt", "36")]
	[InlineData("my-input.txt", "2658")]
	public override void TestPart2(string inputFilename, string expectedResult, Day09SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
