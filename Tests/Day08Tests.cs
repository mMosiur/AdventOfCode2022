using AdventOfCode.Year2022.Day08;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "08")]
[Trait("Day", "8")]
public class Day08Tests : BaseDayTests<Day08Solver, Day08SolverOptions>
{
	public override string DayInputsDirectory => "Day08";

	protected override Day08Solver CreateSolver(Day08SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "21")]
	[InlineData("my-input.txt", "1703")]
	public override void TestPart1(string inputFilename, string expectedResult, Day08SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "8")]
	[InlineData("my-input.txt", "496650")]
	public override void TestPart2(string inputFilename, string expectedResult, Day08SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
