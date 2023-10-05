using System.Text.RegularExpressions;
using AdventOfCode.Year2022.Day16.Cave;

namespace AdventOfCode.Year2022.Day16;

internal sealed class InputReader
{
	private static readonly Regex Regex = new(@"^Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.+)$");

	public IEnumerable<Valve> ReadInput(IEnumerable<string> lines)
	{
		var valveMap = CreateValvesFromInput(lines);
		ConnectValves(valveMap);
		return valveMap.Values.Select(p => p.Item1);
	}

	private Dictionary<string, (Valve, string[])> CreateValvesFromInput(IEnumerable<string> lines)
	{
		Dictionary<string, (Valve, string[])> valveMap = new();
		foreach (string line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			var match = Regex.Match(line);
			if (!match.Success)
			{
				throw new InputException($"Invalid input line: \"{line}\"");
			}

			string name = match.Groups[1].Value;

			int flowRate = int.Parse(match.Groups[2].ValueSpan);

			Valve valve = new(name, flowRate);

			string[] tunnels = match.Groups[3].Value.Split(',', StringSplitOptions.TrimEntries).ToArray();

			valveMap.Add(name, (valve, tunnels));
		}

		return valveMap;
	}

	private static void ConnectValves(Dictionary<string, (Valve, string[])> valveMap)
	{
		foreach ((Valve valve, string[] tunnels) in valveMap.Values)
		{
			foreach (string otherValveName in tunnels)
			{
				if (!valveMap.TryGetValue(otherValveName, out var other))
				{
					throw new InvalidOperationException($"Invalid input: valve \"otherValveName\" does not exist.");
				}

				valve.AddTunnelTo(other.Item1);
			}
		}
	}
}
