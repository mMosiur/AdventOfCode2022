namespace AdventOfCode.Year2022.Day07.ElfFileSystemModel;

sealed class FileSystemTree
{
	public Directory Root { get; }

	public FileSystemTree(Directory root)
	{
		ArgumentNullException.ThrowIfNull(root);
		if (root.Parent is not null)
		{
			throw new ArgumentException("Root directory must not have a parent.", nameof(root));
		}
		Root = root;
	}
}
