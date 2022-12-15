using AdventOfCode.Year2022.Day12;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "12")]
public class Day12Tests : BaseDayTests<Day12Solver, Day12SolverOptions>
{
	public override string DayInputsDirectory => "Day12";

	protected override Day12Solver CreateSolver(Day12SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "31")]
	[InlineData("my-input.txt", "504")]
	public override void TestPart1(string inputFilename, string expectedResult, Day12SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "29")]
	[InlineData("my-input.txt", "500")]
	public override void TestPart2(string inputFilename, string expectedResult, Day12SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
