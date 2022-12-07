using AdventOfCode.Year2022.Day07;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "07")]
[Trait("Day", "7")]
public class Day07Tests : BaseDayTests<Day07Solver, Day07SolverOptions>
{
	public override string DayInputsDirectory => "Day07";

	protected override Day07Solver CreateSolver(Day07SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "95437")]
	[InlineData("my-input.txt", "1454188")]
	public override void TestPart1(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "24933642")]
	[InlineData("my-input.txt", "4183246")]
	public override void TestPart2(string inputFilename, string expectedResult, Day07SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
