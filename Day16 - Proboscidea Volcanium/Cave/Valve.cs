namespace AdventOfCode.Year2022.Day16.Cave;

internal sealed class Valve
{
	private readonly List<ValveTunnel> _tunnels = new();

	public string Name { get; }
	public int FlowRate { get; }
	public IReadOnlyCollection<ValveTunnel> Tunnels => _tunnels;
	public int Index { get; set; } = -1;

	public Valve(string name, int flowRate)
	{
		Name = name;
		FlowRate = flowRate;
	}

	public bool AddTunnelTo(Valve other, int distance = 1) => AddTunnel(new(other, distance));

	public bool AddTunnel(ValveTunnel tunnel)
	{
		if (tunnel.To == this)
		{
			throw new ArgumentException("Cannot tunnel to self.", nameof(tunnel));
		}

		int index = _tunnels.FindIndex(vt => vt.To == tunnel.To);
		if (index < 0)
		{
			_tunnels.Add(tunnel);
			return true;
		}

		if (_tunnels[index].Distance <= tunnel.Distance)
		{
			return false;
		}

		// Shorter tunnel found, replace it.
		_tunnels[index] = tunnel;
		return true;
	}

	public bool RemoveTunnelTo(Valve other)
	{
		int index = _tunnels.FindIndex(vt => vt.To == other);
		if (index < 0)
		{
			return false;
		}

		_tunnels.RemoveAt(index);
		return true;
	}

	public override string ToString() => $"Valve {Name}";
}
