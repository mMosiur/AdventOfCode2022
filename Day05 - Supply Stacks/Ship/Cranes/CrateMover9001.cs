namespace AdventOfCode.Year2022.Day05.Ship.Cranes;

sealed class CrateMover9001 : CargoCrane
{
	private readonly Stack<Crate> _intermediateStack = new();

	public CrateMover9001() : base()
	{
	}

	protected override void Rearrange(RearrangementProcedureStep step)
	{
		for (int i = 0; i < step.CrateCount; i++)
		{
			Crate crate = Stacks[step.FromStackNumber].Pop();
			_intermediateStack.Push(crate);
		}
		for (int i = 0; i < step.CrateCount; i++)
		{
			Crate crate = _intermediateStack.Pop();
			Stacks[step.ToStackNumber].Push(crate);
		}
	}
}
