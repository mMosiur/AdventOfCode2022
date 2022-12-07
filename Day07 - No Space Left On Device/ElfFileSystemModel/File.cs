namespace AdventOfCode.Year2022.Day07.ElfFileSystemModel;

sealed class File : Item
{
	public int Size { get; }

	public File(Directory parent, string name, int size) : base(parent, name)
	{
		ArgumentNullException.ThrowIfNull(parent);
		if (size < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(size), size, "File size must be non-negative.");
		}
		Size = size;
	}
}
