namespace AdventOfCode.Year2022.Day07.ElfFileSystemModel;

sealed class FileSystemTreeBuilder
{
	private readonly string _rootDirectoryName;
	private readonly string _commandPrompt;
	private readonly string _changeDirectoryCommand;
	private readonly string _moveOutOfDirectoryName;
	private readonly string _listDirectoryCommand;
	private readonly string _directoryListingPrefix;

	public FileSystemTreeBuilder(string rootDirectoryName, string commandPrompt, string changeDirectoryCommand, string moveOutOfDirectoryName, string listDirectoryCommand, string directoryListingPrefix)
	{
		ArgumentException.ThrowIfNullOrEmpty(rootDirectoryName);
		_rootDirectoryName = rootDirectoryName;
		_commandPrompt = commandPrompt;
		_changeDirectoryCommand = changeDirectoryCommand;
		_moveOutOfDirectoryName = moveOutOfDirectoryName;
		_listDirectoryCommand = listDirectoryCommand;
		_directoryListingPrefix = directoryListingPrefix;
	}

	public FileSystemTree BuildFromTerminalOutput(TextReader terminalOutputReader)
	{
		Directory root = Directory.CreateRoot(_rootDirectoryName);
		Directory currentDirectory = root;
		string? line;
		bool currentlyListing = false;
		while ((line = terminalOutputReader.ReadLine()) is not null)
		{
			ReadOnlySpan<char> span = line.AsSpan().Trim();
			if (span.IsEmpty)
			{
				continue;
			}
			if (span.StartsWith(_commandPrompt))
			{
				currentlyListing = false; // Turn off listing if we're in the middle of it.
				ReadOnlySpan<char> command = span[_commandPrompt.Length..].TrimStart();
				if (command.StartsWith(_changeDirectoryCommand))
				{
					ReadOnlySpan<char> directoryName = command[_changeDirectoryCommand.Length..].TrimStart();
					if (directoryName.Contains(' '))
					{
						throw new InvalidOperationException($"Unexpected argument for command '{_changeDirectoryCommand}': '{line}'.");
					}
					if (directoryName.SequenceEqual(_moveOutOfDirectoryName))
					{
						currentDirectory = currentDirectory.Parent
							?? throw new InvalidOperationException("Cannot move out of root directory.");
					}
					else if (directoryName.SequenceEqual(_rootDirectoryName))
					{
						currentDirectory = root;
					}
					else
					{
						currentDirectory = currentDirectory.GetSubdirectory(directoryName.ToString());
					}
				}
				else if (command.StartsWith(_listDirectoryCommand))
				{
					command = command[_listDirectoryCommand.Length..].TrimStart();
					if (!command.IsEmpty)
					{
						throw new InvalidOperationException($"Unexpected argument for command '{_listDirectoryCommand}': '{line}'.");
					}
					currentlyListing = true; // Start listing
				}
				else
				{
					throw new InvalidOperationException($"Unexpected command: '{line}'.");
				}
			}
			else if (currentlyListing)
			{
				if (span.StartsWith(_directoryListingPrefix))
				{
					// Directory listing
					ReadOnlySpan<char> directoryName = span[_directoryListingPrefix.Length..].TrimStart();
					if (directoryName.Contains(' '))
					{
						throw new InvalidOperationException($"Unexpected line: '{line}'.");
					}
					currentDirectory.CreateSubdirectory(directoryName.ToString());
				}
				else
				{
					// File listing
					int spaceIndex = span.IndexOf(' ');
					if (spaceIndex < 0)
					{
						throw new InvalidOperationException($"Unexpected line: '{line}'.");
					}
					ReadOnlySpan<char> fileSize = span[..spaceIndex];
					ReadOnlySpan<char> fileName = span[(spaceIndex + 1)..];
					if (!int.TryParse(fileSize, out int size))
					{
						throw new InvalidOperationException($"Unexpected line: '{line}'.");
					}
					if (fileName.Contains(' '))
					{
						throw new InvalidOperationException($"Unexpected line: '{line}'.");
					}
					currentDirectory.CreateFile(fileName.ToString(), size);
				}
			}
			else
			{
				throw new InvalidOperationException($"Unexpected line: '{line}'.");
			}
		}
		return new(root);
	}
}
