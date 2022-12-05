using System.Collections;

namespace AdventOfCode.Year2022.Day05.Ship;

sealed class RearrangementProcedure : IReadOnlyList<RearrangementProcedureStep>
{
	private readonly List<RearrangementProcedureStep> _steps;

	public int Count => _steps.Count;

	public RearrangementProcedure(IEnumerable<RearrangementProcedureStep> steps)
	{
		_steps = steps.ToList();
	}

	public RearrangementProcedureStep this[int index] => _steps[index];


	public IEnumerator<RearrangementProcedureStep> GetEnumerator() => _steps.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
