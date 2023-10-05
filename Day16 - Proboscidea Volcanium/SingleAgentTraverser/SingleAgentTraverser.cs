namespace AdventOfCode.Year2022.Day16;

internal sealed partial class SingleAgentTraverser
{
	public ValveMap ValveMap { get; }
	public int TimeLeftInMinutes { get; }

	public SingleAgentTraverser(ValveMap valveMap, int timeLeftInMinutes)
	{
		ValveMap = valveMap;
		TimeLeftInMinutes = timeLeftInMinutes;
	}

	public int Traverse()
	{
		TraversingQueue queue = new();
		Dictionary<TraversingMemo, int> visited = new();
		queue.EnqueueIfValid(CreateStartingState(ValveMap.StartingValve.Index, TimeLeftInMinutes));
		int maxPressure = int.MinValue;
		while (queue.TryDequeue(out var state))
		{
			var memo = TraversingMemo.FromTraversingState(state);
			if (visited.TryGetValue(memo, out int previousPressure) && previousPressure >= state.PressureProjection) continue;

			visited[memo] = state.PressureProjection;
			maxPressure = Math.Max(maxPressure, state.PressureProjection);
			if (state.TimeLeft == 0) continue;
			foreach (var tunnel in state.CurrentValve.Tunnels)
			{
				EnqueueValidMoves(state, tunnel, queue);
			}
		}

		return maxPressure;
	}

	private static void EnqueueValidMoves(TraversingState state, ValveTunnel tunnel, TraversingQueue queue)
	{
		queue.EnqueueIfValid(state.AfterMove(tunnel, false));
		if (state.IsCurrentValveClosed)
		{
			queue.EnqueueIfValid(state.AfterMove(tunnel, true));
		}
	}

	private TraversingState CreateStartingState(int startingValveIndex, int timeLeft)
	{
		// Start with all valves that have flow rate of 0 opened
		var state = new ValveMapState();
		state = ValveMap.Valves
			.Where(v => v.FlowRate == 0)
			.Aggregate(state, (current, valve) => current.WithOpenedValve(valve));
		return new(this, state, startingValveIndex, timeLeft, 0);
	}

	private readonly record struct TraversingMemo(ulong EncodedValveMapState, int ValveIndex)
	{
		public static TraversingMemo FromTraversingState(TraversingState traversingState)
			=> new(traversingState.ValveMapState.EncodedState, traversingState.CurrentValveIndex);
	}
}
