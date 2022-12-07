using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day07;

public sealed class Day07SolverOptions : DaySolverOptions
{
	public string RootDirectoryName { get; set; } = "/";
	public string CommandPrompt { get; set; } = "$";
	public string ChangeDirectoryCommand { get; set; } = "cd";
	public string MoveOutOfDirectoryName { get; set; } = "..";
	public string ListDirectoryCommand { get; set; } = "ls";
	public string DirectoryListingPrefix { get; set; } = "dir";

	public int PartOneDirectoryMaxTotalSize { get; set; } = 100_000;

	public int PartTwoTotalSpace { get; set; } = 70_000_000;
	public int PartTwoNeededUnusedSpace { get; set; } = 30_000_000;
}
