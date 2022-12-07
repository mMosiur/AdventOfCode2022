namespace AdventOfCode.Year2022.Day07.ElfFileSystemModel;

abstract class Item
{
	public Directory? Parent { get; }
	public string Name { get; }

	protected Item(Directory? parent, string name)
	{
		ArgumentException.ThrowIfNullOrEmpty(name);
		Name = name;
		Parent = parent;
	}
}
