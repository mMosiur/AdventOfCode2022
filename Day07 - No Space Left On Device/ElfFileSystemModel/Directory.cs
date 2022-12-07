using System.Diagnostics;

namespace AdventOfCode.Year2022.Day07.ElfFileSystemModel;

sealed class Directory : Item
{
	private readonly Dictionary<string, Item> _items = new();

	private Directory(Directory? parent, string name) : base(parent, name)
	{
	}

	public static Directory CreateRoot(string rootDirectoryName)
	{
		ArgumentException.ThrowIfNullOrEmpty(rootDirectoryName);
		return new(null, rootDirectoryName);
	}

	public int CalculateSize()
	{
		return _items.Values.Sum(i => i switch
		{
			Directory directory => directory.CalculateSize(),
			File file => file.Size,
			_ => throw new UnreachableException("Unexpected item type.")
		});
	}

	public Item GetItem(string name)
	{
		if (_items.TryGetValue(name, out Item? item))
		{
			return item;
		}
		throw new KeyNotFoundException($"Directory '{Name}' does not contain child item '{name}'.");
	}

	public Directory GetSubdirectory(string name)
	{
		Item item = GetItem(name);
		if (item is Directory directory)
		{
			return directory;
		}
		throw new InvalidOperationException($"Item '{name}' exists in '{Name}', but is not a directory.");
	}

	public File GetFile(string name)
	{
		Item item = GetItem(name);
		if (item is File file)
		{
			return file;
		}
		throw new InvalidOperationException($"Item '{name}' exists in '{Name}', but is not a file.");
	}

	public Directory CreateSubdirectory(string name)
	{
		if (_items.ContainsKey(name))
		{
			throw new ArgumentException($"Directory '{Name}' already contains child item '{name}'.", nameof(name));
		}
		Directory subdirectory = new(this, name);
		_items.Add(name, subdirectory);
		return subdirectory;
	}

	public File CreateFile(string name, int size)
	{
		if (_items.ContainsKey(name))
		{
			throw new ArgumentException($"Directory '{Name}' already contains child item '{name}'.", nameof(name));
		}
		File file = new(this, name, size);
		_items.Add(name, file);
		return file;
	}

	public IEnumerable<Directory> ListDirectories(bool recurse = false)
	{
		foreach (Item item in _items.Values)
		{
			if (item is not Directory directory)
			{
				continue;
			}
			yield return directory;
			if (!recurse)
			{
				continue;
			}
			foreach (Directory subdirectory in directory.ListDirectories(true))
			{
				yield return subdirectory;
			}
		}
	}
}
