using AdventOfCode.Year2022.Day06;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public class Day06Tests : BaseDayTests<Day06Solver, Day06SolverOptions>
{
	public override string DayInputsDirectory => "Day06";

	protected override Day06Solver CreateSolver(Day06SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "7")]
	[InlineData("example-input-2.txt", "5")]
	[InlineData("example-input-3.txt", "6")]
	[InlineData("example-input-4.txt", "10")]
	[InlineData("example-input-5.txt", "11")]
	[InlineData("my-input.txt", "1876")]
	public override void TestPart1(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-1.txt", "19")]
	[InlineData("example-input-2.txt", "23")]
	[InlineData("example-input-3.txt", "23")]
	[InlineData("example-input-4.txt", "29")]
	[InlineData("example-input-5.txt", "26")]
	[InlineData("my-input.txt", "2202")]
	public override void TestPart2(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
