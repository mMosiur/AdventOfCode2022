namespace AdventOfCode.Year2022.Day16;

internal sealed class ValveMap
{
	private readonly List<Valve> _valves;
	public Valve StartingValve { get; }

	public IReadOnlyList<Valve> Valves => _valves;

	public ValveMap(IEnumerable<Valve> valves, string startingValveName)
	{
		_valves = valves.ToList();
		StartingValve = _valves.First(v => v.Name == startingValveName);
	}

	private void GenerateIndexes()
	{
		for(int i = 0; i < _valves.Count; i++)
		{
			_valves[i].Index = i;
		}
	}

	public void Optimize()
	{
		var zeroFlowRateValves = _valves.Where(v => v.FlowRate == 0 && v != StartingValve).ToList();
		foreach (Valve zeroValve in zeroFlowRateValves)
		{
			_valves.Remove(zeroValve);
			var zeroTunnels = zeroValve.Tunnels;
			foreach (ValveTunnel zeroTunnel in zeroTunnels)
			{
				var otherValve = zeroTunnel.To;
				otherValve.RemoveTunnelTo(zeroValve);
				var extendedTunnels = zeroTunnels.Where(t => t.To != otherValve).Select(t => t with { Distance = t.Distance + zeroTunnel.Distance });
				foreach(var extendedTunnel in extendedTunnels)
				{
					otherValve.AddTunnel(extendedTunnel);
				}
			}
		}
		GenerateIndexes();
	}
}
