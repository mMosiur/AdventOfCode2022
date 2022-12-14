using System.Text.RegularExpressions;
using AdventOfCode.Common.Geometry;

namespace AdventOfCode.Year2022.Day15;

readonly struct SensorInfo
{
	private static readonly Regex _inputLineRegex = new(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$", RegexOptions.Compiled);

	public Point Location { get; }
	public Point ClosestBeaconLocation { get; }
	public int ClosestBeaconDistance { get; }

	public SensorInfo(Point location, Point closesBeaconLocation)
	{
		Location = location;
		ClosestBeaconLocation = closesBeaconLocation;
		ClosestBeaconDistance = MathG.ManhattanDistance(Location, ClosestBeaconLocation);
	}

	public int DistanceTo(Point point)
	{
		return MathG.ManhattanDistance(Location, point);
	}

	public static SensorInfo Parse(string s)
	{
		try
		{
			Match match = _inputLineRegex.Match(s);
			if (!match.Success)
			{
				throw new FormatException("Invalid line format.");
			}
			Point location = new()
			{
				X = int.Parse(match.Groups[1].ValueSpan),
				Y = int.Parse(match.Groups[2].ValueSpan)
			};
			Point closestBeaconLocation = new()
			{
				X = int.Parse(match.Groups[3].ValueSpan),
				Y = int.Parse(match.Groups[4].ValueSpan)
			};
			return new SensorInfo(location, closestBeaconLocation);
		}
		catch (SystemException e)
		{
			throw new FormatException("Invalid input line.", e);
		}
	}
}
