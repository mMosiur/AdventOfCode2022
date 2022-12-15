using AdventOfCode.Year2022.Day11;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "11")]
public class Day11Tests : BaseDayTests<Day11Solver, Day11SolverOptions>
{
	public override string DayInputsDirectory => "Day11";

	protected override Day11Solver CreateSolver(Day11SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "10605")]
	[InlineData("my-input.txt", "88208")]
	public override void TestPart1(string inputFilename, string expectedResult, Day11SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "2713310158")]
	[InlineData("my-input.txt", "21115867968")]
	public override void TestPart2(string inputFilename, string expectedResult, Day11SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
