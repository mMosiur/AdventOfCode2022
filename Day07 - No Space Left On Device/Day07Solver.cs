using AdventOfCode.Abstractions;
using AdventOfCode.Year2022.Day07.ElfFileSystemModel;

namespace AdventOfCode.Year2022.Day07;

public sealed class Day07Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 7;
	public override string Title => "No Space Left On Device";

	private readonly int _partOneDirectoryMaxTotalSize;
	private readonly int _partTwoTotalSpace;
	private readonly int _partTwoNeededUnusedSpace;
	private readonly Lazy<FileSystemTree> _tree;

	private FileSystemTree Tree => _tree.Value;

	public Day07Solver(Day07SolverOptions options) : base(options)
	{
		_partOneDirectoryMaxTotalSize = options.PartOneDirectoryMaxTotalSize;
		_partTwoTotalSpace = options.PartTwoTotalSpace;
		_partTwoNeededUnusedSpace = options.PartTwoNeededUnusedSpace;
		_tree = new(() =>
		{
			try
			{
				FileSystemTreeBuilder builder = new(
					rootDirectoryName: "/",
					commandPrompt: "$",
					changeDirectoryCommand: "cd",
					moveOutOfDirectoryName: "..",
					listDirectoryCommand: "ls",
					directoryListingPrefix: "dir"
				);
				using TextReader reader = new StringReader(Input);
				return builder.BuildFromTerminalOutput(reader);
			}
			catch (Exception e)
			{
				throw new InputException("Invalid input.", e);
			}
		});
	}

	public Day07Solver(Action<Day07SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day07Solver() : this(new Day07SolverOptions())
	{
	}


	public override string SolvePart1()
	{
		int result = Tree.Root.ListDirectories(recurse: true)
			.Select(d => d.CalculateSize())
			.Where(s => s <= _partOneDirectoryMaxTotalSize)
			.Sum();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int treeUsedSpace = Tree.Root.CalculateSize();
		int unusedSpace = _partTwoTotalSpace - treeUsedSpace;
		int spaceToFree = _partTwoNeededUnusedSpace - unusedSpace;
		if (spaceToFree <= 0)
		{
			throw new DaySolverException("No directory needed to be deleted.");
		}
		try
		{
			int result = Tree.Root.ListDirectories(recurse: true)
				.Select(d => d.CalculateSize())
				.Where(s => s >= spaceToFree)
				.Min();
			return $"{result}";
		}
		catch (InvalidOperationException e)
		{
			throw new DaySolverException("No directory large enough found.", e);
		}
	}
}
