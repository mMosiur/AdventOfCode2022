using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2022.Day16;

internal sealed class ValveMapTraverser
{
	public ValveMap ValveMap { get; }
	public int TimeLeftInMinutes { get; }

	public ValveMapTraverser(ValveMap valveMap, int timeLeftInMinutes)
	{
		ValveMap = valveMap;
		TimeLeftInMinutes = timeLeftInMinutes;
	}

	public int Traverse()
	{
		TraversingQueue queue = new();
		Dictionary<TraversingMemo, int> visited = new();
		queue.EnqueueIfValid(CreateStartingState(ValveMap.StartingValve.Index, TimeLeftInMinutes));
		int maxPressure = 0;
		while (queue.TryDequeue(out var state))
		{
			var memo = state.ToMemo();
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

	private sealed class TraversingQueue
	{
		private readonly Queue<TraversingState> _queue = new();

		public int Count => _queue.Count;

		public bool EnqueueIfValid(TraversingState state)
		{
			if (state.TimeLeft < 0) return false;
			Enqueue(state);
			return true;
		}

		public void Enqueue(TraversingState state)
		{
			_queue.Enqueue(state);
		}

		public TraversingState Dequeue()
		{
			return _queue.Dequeue();
		}

		public TraversingState Peek()
		{
			return _queue.Peek();
		}

		public bool TryDequeue(out TraversingState state)
		{
			return _queue.TryDequeue(out state);
		}
	}

	private readonly record struct TraversingState
	{
		private readonly ValveMapTraverser _traverser;
		public required ValveMapState ValveMapState { get; init; }
		public required int CurrentValveIndex { get; init; }
		public required int TimeLeft { get; init; }
		public required int PressureProjection { get; init; }

		public Valve CurrentValve => _traverser.ValveMap.Valves[CurrentValveIndex];
		public bool IsCurrentValveOpen => ValveMapState.IsOpen(CurrentValveIndex);
		public bool IsCurrentValveClosed => ValveMapState.IsClosed(CurrentValveIndex);

		[SetsRequiredMembers]
		public TraversingState(ValveMapTraverser traverser, ValveMapState valveMapState, int currentValveIndex, int timeLeft, int pressureProjection)
		{
			_traverser = traverser;
			ValveMapState = valveMapState;
			CurrentValveIndex = currentValveIndex;
			TimeLeft = timeLeft;
			PressureProjection = pressureProjection;
		}

		public TraversingState AfterMove(ValveTunnel tunnel, bool withValveTurn)
		{
			int newPressureProjection = PressureProjection;
			int newTimeLeft = TimeLeft - tunnel.Distance;
			ValveMapState newValveMapState = ValveMapState;
			if (withValveTurn && ValveMapState.IsClosed(CurrentValveIndex))
			{
				newPressureProjection += (TimeLeft - 1) * CurrentValve.FlowRate;
				newTimeLeft -= 1;
				newValveMapState = ValveMapState.WithOpenedValve(CurrentValveIndex);
			}

			return new(_traverser, newValveMapState, tunnel.To.Index, newTimeLeft, newPressureProjection);
		}

		public TraversingMemo ToMemo() => new(ValveMapState.EncodedState, CurrentValveIndex);
	}

	private readonly record struct TraversingMemo(ulong EncodedValveMapState, int ValveIndex);
}
