namespace AdventOfCode.Year2022.Day05.Ship.Cranes;

sealed class CrateMover9000 : CargoCrane
{
	public CrateMover9000() : base()
	{
	}

	protected override void Rearrange(RearrangementProcedureStep step)
	{
		for (int i = 0; i < step.CrateCount; i++)
		{
			Crate crate = Stacks[step.FromStackNumber].Pop();
			Stacks[step.ToStackNumber].Push(crate);
		}
	}
}
