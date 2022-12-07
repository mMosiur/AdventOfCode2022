using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2022.Day07;

public sealed class Day07Solver : DaySolver
{
	public override int Year => 2022;
	public override int Day => 7;
	public override string Title => "No Space Left On Device";

	public Day07Solver(Day07SolverOptions options) : base(options)
	{
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
		List<Directory> allDirectories = new();
		Directory? topLevelDirectory = null;
		Directory? currentDirectory = null;
		foreach (string line in InputLines)
		{
			if (line.StartsWith("$ cd"))
			{
				string dirName = line[5..].Trim();
				if (dirName == "..")
				{
					currentDirectory = currentDirectory?.Parent ?? throw new InvalidOperationException();
					continue;
				}
				else
				{
					if (currentDirectory is null)
					{
						currentDirectory = new Directory(null, dirName);
						topLevelDirectory = currentDirectory;
						allDirectories.Add(currentDirectory);
					}
					else
					{
						currentDirectory = currentDirectory.GetSubdirectory(dirName);
					}
				}
			}
			else if (line.StartsWith("$ ls"))
			{
			}
			else if (line.StartsWith("dir"))
			{
				string dirName = line[4..].Trim();
				Directory subdirectory = new(currentDirectory, dirName);
				allDirectories.Add(subdirectory);
				if (currentDirectory is null)
				{
					throw new InvalidOperationException();
				}
				currentDirectory.AddItem(subdirectory);
			}
			else
			{
				string[] parts = line.Split(' ');
				Debug.Assert(parts.Length == 2);
				long size = long.Parse(parts[0]);
				string fileName = parts[1];
				if (currentDirectory is null) throw new InvalidOperationException();
				File file = new(currentDirectory, fileName, size);
				currentDirectory.AddItem(file);
			}
		}
		long result = allDirectories.Select(d => d.CalculateSize()).Where(s => s <= 100000).Sum();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		List<Directory> allDirectories = new();
		const long totalSpace = 70_000_000;
		const long neededSpace = 30_000_000;
		Directory? topLevelDirectory = null;
		Directory? currentDirectory = null;
		foreach (string line in InputLines)
		{
			if (line.StartsWith("$ cd"))
			{
				string dirName = line[5..].Trim();
				if (dirName == "..")
				{
					currentDirectory = currentDirectory?.Parent ?? throw new InvalidOperationException();
					continue;
				}
				else
				{
					if (currentDirectory is null)
					{
						currentDirectory = new Directory(null, dirName);
						topLevelDirectory = currentDirectory;
						allDirectories.Add(currentDirectory);
					}
					else
					{
						currentDirectory = currentDirectory.GetSubdirectory(dirName);
					}
				}
			}
			else if (line.StartsWith("$ ls"))
			{
			}
			else if (line.StartsWith("dir"))
			{
				string dirName = line[4..].Trim();
				Directory subdirectory = new(currentDirectory, dirName);
				allDirectories.Add(subdirectory);
				if (currentDirectory is null)
				{
					throw new InvalidOperationException();
				}
				currentDirectory.AddItem(subdirectory);
			}
			else
			{
				string[] parts = line.Split(' ');
				Debug.Assert(parts.Length == 2);
				long size = long.Parse(parts[0]);
				string fileName = parts[1];
				if (currentDirectory is null) throw new InvalidOperationException();
				File file = new(currentDirectory, fileName, size);
				currentDirectory.AddItem(file);
			}
		}
		long unusedSpace = totalSpace - topLevelDirectory!.CalculateSize();
		long spaceToFree = neededSpace - unusedSpace;
		IEnumerable<long> sizes = allDirectories.Select(d => d.CalculateSize());
		IEnumerable<long> bigEnoughSizes = sizes.Where(s => s >= spaceToFree);
		IEnumerable<long> orderedSizes = bigEnoughSizes.Order();
		long result = orderedSizes.First();
		return $"{result}";
	}
}

abstract class Item
{
	public Directory? Parent { get; }
	public string Name { get; }

	public Item(Directory? parent, string name)
	{
		Parent = parent;
		Name = name;
	}
}

class Directory : Item
{
	private readonly List<Directory> _directories = new();
	private readonly List<File> _files = new();

	public IReadOnlyCollection<Directory> Directories => _directories;
	public IReadOnlyCollection<File> Files => _files;

	public Directory GetSubdirectory(string name)
	{
		return _directories.Single(item => item.Name == name);
	}

	public void AddItem(Item item)
	{
		switch (item)
		{
			case Directory directory:
				_directories.Add(directory);
				break;
			case File file:
				_files.Add(file);
				break;
			default:
				throw new UnreachableException();
		}
	}

	public long CalculateSize()
	{
		checked
		{
			return _directories.Sum(item => item.CalculateSize()) + _files.Sum(item => item.Size);
		}
	}

	public Directory(Directory? parent, string name) : base(parent, name)
	{
	}
}

class File : Item
{
	public long Size { get; }

	public File(Directory parent, string name, long size) : base(parent, name)
	{
		Size = size;
	}
}
