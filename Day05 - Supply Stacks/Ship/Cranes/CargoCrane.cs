namespace AdventOfCode.Year2022.Day05.Ship.Cranes;

abstract class CargoCrane
{
	private CrateStacks? _crateStacks;

	protected CrateStacks Stacks => _crateStacks
		?? throw new InvalidOperationException("No crate stacks have been set.");

	protected CargoCrane()
	{
	}

	public void OperateOn(CrateStacks? crateStacks)
	{
		_crateStacks = crateStacks;
	}

	protected abstract void Rearrange(RearrangementProcedureStep step);

	public void Rearrange(RearrangementProcedure procedure)
	{
		if (_crateStacks is null)
		{
			throw new InvalidOperationException("No crate stacks have been set.");
		}
		foreach (RearrangementProcedureStep step in procedure)
		{
			Rearrange(step);
		}
	}
}
