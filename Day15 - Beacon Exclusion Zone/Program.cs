using System.Diagnostics;
using AdventOfCode;
using AdventOfCode.Year2022.Day15;

try
{
	string? filepath = args.Length switch
	{
		0 => null,
		1 => args[0],
		3 => args[0],
		_ => throw new CommandLineException(
			$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath>]\"" +
			$" or \"dotnet run <input filepath> <part one row y> <part two max coords>\"."
		)
	};

	int? part1RowY = null;
	int? part2MaxCoords = null;
	if (args.Length == 3)
	{
		if (!int.TryParse(args[1], out int part1RowYValue))
		{
			throw new CommandLineException($"Could not parse \"{args[1]}\" as an integer.");
		}
		part1RowY = part1RowYValue;

		if (!int.TryParse(args[2], out int part2MaxCoordsValue))
		{
			throw new CommandLineException($"Could not parse \"{args[2]}\" as an integer.");
		}
		part2MaxCoords = part2MaxCoordsValue;
	}

	Day15Solver solver = new(options =>
	{
		options.InputFilepath = filepath ?? options.InputFilepath;
		options.Part1RowY = part1RowY ?? options.Part1RowY;
		options.Part2MaxCoordinates = part2MaxCoords ?? options.Part2MaxCoordinates;
	});

	Console.WriteLine($"--- Day {solver.Day}: {solver.Title} ---");

	Console.Write("Part one: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);

	Console.Write("Part two: ");
	string part2 = solver.SolvePart2();
	Console.WriteLine(part2);
}
catch (AdventOfCodeException e)
{
	string errorPrefix = e switch
	{
		CommandLineException => "Command line error",
		InputException => "Input error",
		DaySolverException => "Day solver error",
		_ => throw new UnreachableException($"Unknown exception type \"{e.GetType()}\".")
	};

	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"{errorPrefix}: {e.Message}");
	Console.ResetColor();
	Environment.Exit(1);
}
