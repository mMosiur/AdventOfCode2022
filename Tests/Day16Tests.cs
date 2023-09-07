using AdventOfCode.Year2022.Day16;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "16")]
public class Day16Tests : BaseDayTests<Day16Solver, Day16SolverOptions>
{
	public override string DayInputsDirectory => "Day16";

	protected override Day16Solver CreateSolver(Day16SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "1651")]
	[InlineData("my-input.txt", "1751")]
	public override void TestPart1(string inputFilename, string expectedResult, Day16SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
