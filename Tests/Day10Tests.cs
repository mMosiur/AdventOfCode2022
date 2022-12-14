using AdventOfCode.Year2022.Day10;
using Xunit;

namespace AdventOfCode.Year2022.Tests;

[Trait("Year", "2022")]
[Trait("Day", "10")]
public class Day10Tests : BaseDayTests<Day10Solver, Day10SolverOptions>
{
	public override string DayInputsDirectory => "Day10";

	protected override Day10Solver CreateSolver(Day10SolverOptions options) => new(options);

	public static IEnumerable<object[]> Part2TestCases => new[]
	{
		new object[]
		{
			"example-input.txt",
			string.Join(Environment.NewLine, new[]
			{
			"##..##..##..##..##..##..##..##..##..##..",
			"###...###...###...###...###...###...###.",
			"####....####....####....####....####....",
			"#####.....#####.....#####.....#####.....",
			"######......######......######......####",
			"#######.......#######.......#######....."
			})
		},
		new object[]
		{
			"my-input.txt",
			string.Join(Environment.NewLine, new[]
			{
			"###....##.####.###..#..#.###..####.#..#.",
			"#..#....#.#....#..#.#..#.#..#.#....#..#.",
			"###.....#.###..#..#.####.#..#.###..#..#.",
			"#..#....#.#....###..#..#.###..#....#..#.",
			"#..#.#..#.#....#.#..#..#.#.#..#....#..#.",
			"###...##..#....#..#.#..#.#..#.#.....##.."
			})
		}
	};

	[Theory]
	[InlineData("example-input.txt", "13140")]
	[InlineData("my-input.txt", "14620")]
	public override void TestPart1(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[MemberData(nameof(Part2TestCases))]
	public override void TestPart2(string inputFilename, string expectedResult, Day10SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
