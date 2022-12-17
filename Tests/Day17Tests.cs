using AdventOfCode.Year2022.Day17;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "17")]
public class Day17Tests : BaseDayTests<Day17Solver, Day17SolverOptions>
{
	public override string DayInputsDirectory => "Day17";

	protected override Day17Solver CreateSolver(Day17SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "3068")]
	public override void TestPart1(string inputFilename, string expectedResult, Day17SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
