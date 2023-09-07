using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2022.Day16;

internal sealed class ValveMapTraverser
{
	public ValveMap ValveMap { get; }
	public ValveMapStateEncoder Encoder { get; }
	public int TimeInMinutes { get; }

	public ValveMapTraverser(ValveMap valveMap, int timeInMinutes)
	{
		ValveMap = valveMap;
		Encoder = new(valveMap);
		TimeInMinutes = timeInMinutes;
	}

	public int Traverse()
	{
		ValveMapQueue queue = new();
		Dictionary<TraversingMemo, int> visited = new();
		queue.Enqueue(CreateStartingState(ValveMap.StartingValve.Index, TimeInMinutes));
		int maxPressure = 0;
		while (queue.TryDequeue(out var state))
		{
			var memo = state.ToMemo();
			if (visited.TryGetValue(memo, out int previousPressure))
			{
				if (previousPressure >= state.PressureProjection) continue;
			}

			visited[memo] = state.PressureProjection;
			maxPressure = Math.Max(maxPressure, state.PressureProjection);
			if (state.TimeLeft == 0) continue;
			foreach (var tunnel in state.CurrentValve.Tunnels)
			{
				queue.Enqueue(state.AfterMove(tunnel, false));
				if (state.IsCurrentValveClosed)
				{
					queue.Enqueue(state.AfterMove(tunnel, true));
				}
			}
		}

		return maxPressure;
	}

	private TraversingState CreateStartingState(int startingValveIndex, int timeInMinutes)
	{
		// Start with all valves that have flow rate of 0 opened
		ulong encodedState = ValveMap.Valves
			.Where(v => v.FlowRate == 0)
			.Aggregate(ValveMapStateEncoder.BaseState, (current, valve) => Encoder.OpenValve(current, valve));
		return new(this, encodedState, startingValveIndex, timeInMinutes, 0);
	}

	private sealed class ValveMapQueue
	{
		// private readonly PriorityQueue<TraversingState, int> _queue = new();
		private readonly Queue<TraversingState> _queue = new();

		public int Count => _queue.Count;

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
		public required ulong CurrentEncodedState { get; init; }
		public required int CurrentValveIndex { get; init; }
		public required int TimeLeft { get; init; }
		public required int PressureProjection { get; init; }

		public Valve CurrentValve => _traverser.ValveMap.Valves[CurrentValveIndex];
		public bool IsCurrentValveOpen => _traverser.Encoder.IsOpen(CurrentEncodedState, CurrentValveIndex);
		public bool IsCurrentValveClosed => _traverser.Encoder.IsClosed(CurrentEncodedState, CurrentValveIndex);

		[SetsRequiredMembers]
		public TraversingState(ValveMapTraverser traverser, ulong currentEncodedState, int currentValveIndex, int timeInMinutes, int pressureProjection)
		{
			_traverser = traverser;
			CurrentEncodedState = currentEncodedState;
			CurrentValveIndex = currentValveIndex;
			TimeLeft = timeInMinutes;
			PressureProjection = pressureProjection;
		}

		public TraversingState AfterMove(ValveTunnel tunnel, bool withValveTurn)
		{
			int newPressureProjection = PressureProjection;
			int newTimeLeft = TimeLeft - tunnel.Distance;
			ulong newEncodedState = CurrentEncodedState;
			if (withValveTurn && _traverser.Encoder.IsClosed(CurrentEncodedState, CurrentValveIndex))
			{
				newPressureProjection += (TimeLeft - 1) * CurrentValve.FlowRate;
				newTimeLeft -= 1;
				newEncodedState = _traverser.Encoder.OpenValve(CurrentEncodedState, CurrentValveIndex);
			}

			return new(_traverser, newEncodedState, tunnel.To.Index, newTimeLeft, newPressureProjection);
		}

		public TraversingMemo ToMemo() => new(CurrentEncodedState, CurrentValveIndex);
	}

	private readonly record struct TraversingMemo(ulong EncodedState, int ValveIndex);
}
