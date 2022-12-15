using AdventOfCode.Year2022.Day15;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "15")]
public class Day15Tests : BaseDayTests<Day15Solver, Day15SolverOptions>
{
	public override string DayInputsDirectory => "Day15";

	protected override Day15Solver CreateSolver(Day15SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "26", 10)]
	[InlineData("my-input.txt", "5166077", 2000000)]
	public void TestPart1CustomOptions(string inputFilename, string expectedResult, int part1RowY)
	{
		Day15SolverOptions options = new()
		{
			Part1RowY = part1RowY
		};
		base.TestPart1(inputFilename, expectedResult, options);
	}

	[Theory]
	[InlineData("my-input.txt", "5166077")]
	public override void TestPart1(string inputFilename, string expectedResult, Day15SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "56000011", 0, 20)]
	[InlineData("my-input.txt", "13071206703981", 0, 4_000_000)]
	public void TestPart2CustomOptions(string inputFilename, string expectedResult, int minCoordinateValue, int maxCoordinateValue)
	{
		Day15SolverOptions options = new()
		{
			Part2MinCoordinates = minCoordinateValue,
			Part2MaxCoordinates = maxCoordinateValue
		};
		base.TestPart2(inputFilename, expectedResult, options);
	}

	[Theory]
	[InlineData("my-input.txt", "13071206703981")]
	public override void TestPart2(string inputFilename, string expectedResult, Day15SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
