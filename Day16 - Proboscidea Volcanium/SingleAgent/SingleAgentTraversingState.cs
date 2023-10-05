using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Year2022.Day16.Cave;

namespace AdventOfCode.Year2022.Day16.SingleAgent;

internal sealed partial class SingleAgentTraverser
{
	private readonly record struct TraversingState
	{
		private readonly SingleAgentTraverser _traverser;
		public required ValveMapState ValveMapState { get; init; }
		public required int CurrentValveIndex { get; init; }
		public required int TimeLeft { get; init; }
		public required int PressureProjection { get; init; }

		public Valve CurrentValve => _traverser.ValveMap.Valves[CurrentValveIndex];
		public bool IsCurrentValveOpen => ValveMapState.IsOpen(CurrentValveIndex);
		public bool IsCurrentValveClosed => ValveMapState.IsClosed(CurrentValveIndex);

		[SetsRequiredMembers]
		public TraversingState(SingleAgentTraverser traverser, ValveMapState valveMapState, int currentValveIndex, int timeLeft, int pressureProjection)
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
			if (withValveTurn && IsCurrentValveClosed)
			{
				newPressureProjection += (TimeLeft - 1) * CurrentValve.FlowRate;
				newTimeLeft -= 1;
				newValveMapState = ValveMapState.WithOpenedValve(CurrentValveIndex);
			}

			return new(_traverser, newValveMapState, tunnel.To.Index, newTimeLeft, newPressureProjection);
		}
	}
}
