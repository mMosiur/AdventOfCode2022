using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2022.Day03.Rucksacks;

readonly struct ItemType : IEquatable<ItemType>
{
	public required char Id { get; init; }

	[SetsRequiredMembers]
	public ItemType(char id)
	{
		Id = id;
	}

	public static implicit operator char(ItemType item) => item.Id;
	public static implicit operator ItemType(char c) => new(c);

	public bool Equals(ItemType other) => Id == other.Id;

	public override bool Equals(object? obj) => obj is ItemType itemType && Equals(itemType);

	public override int GetHashCode() => HashCode.Combine(Id);
}
