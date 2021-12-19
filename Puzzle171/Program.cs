var input = "x=201..230, y=-99..-65";
//input = "x=20..30, y=-10..-5";

var split = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
var xSplit = split[0].Trim().Remove(0, 2).Split("..", StringSplitOptions.RemoveEmptyEntries);
var ySplit = split[1].Trim().Remove(0, 2).Split("..", StringSplitOptions.RemoveEmptyEntries);

(int Min, int Max) xTarget = (int.Parse(xSplit[0]), int.Parse(xSplit[1]));
(int Min, int Max) yTarget = (int.Parse(ySplit[0]), int.Parse(ySplit[1]));

var yLocations = new Dictionary<(int X, int Y), int>();

for (int i = 1; i < xTarget.Max+1; i++)
{
    var testXVelocity = i;
    var previousHighestY = -1;
    for (int j = yTarget.Min; j < Math.Abs(yTarget.Min); j++)
    {
        int? tryHighestY = null;

        var testYVelocity = j;

        (int X, int Y) probeVelocity = (testXVelocity, testYVelocity);
        (int X, int Y) probeLocation = (0, 0);

        tryHighestY = MoveProbe(probeLocation, probeVelocity, tryHighestY);

        if (tryHighestY != null)
            yLocations.Add((testXVelocity, testYVelocity), tryHighestY.Value);

        if ((tryHighestY < previousHighestY && previousHighestY > 0) || testYVelocity > 1000)
            break;
    }
}

var maxYLocation = yLocations.MaxBy(x => x.Value);
Console.WriteLine($"Highest Location: {maxYLocation.Value} at Velocity: ({maxYLocation.Key.X},{maxYLocation.Key.Y})");
Console.WriteLine($"Total valid Velocities: {yLocations.Count}");

int? MoveProbe((int X, int Y) probeLocation, (int X, int Y) probeVelocity, int? highestY)
{
    var previousProbeLocation = probeLocation;
    probeLocation = (probeLocation.X + probeVelocity.X, probeLocation.Y + probeVelocity.Y);

    if(IsValidProbeLocation(probeLocation, probeVelocity, previousProbeLocation) == false)
        return null;

    if(highestY == null || highestY.Value < probeLocation.Y)
        highestY = probeLocation.Y;
    
    if (IsFinalProbeLocation(probeLocation))
    {
        return highestY;
    }

    if (probeVelocity.X > 0)
        probeVelocity.X--;
    else if (probeVelocity.X < 0)
        probeVelocity.X++;

    probeVelocity.Y--;

    return MoveProbe(probeLocation, probeVelocity, highestY);
}

bool IsValidProbeLocation((int X, int Y) probeLocation, (int X, int Y) probeVelocity, (int X, int Y) previousProbeLocation)
{
    if (probeLocation.X > xTarget.Max)
    {
        return false;
    }

    if (probeLocation.X < xTarget.Min && probeLocation.X <= previousProbeLocation.X)
    {
        return false;
    }

    if (probeLocation.Y < yTarget.Min && probeVelocity.Y <= 0)
    {
        return false;
    }

    return true;
}

bool IsFinalProbeLocation((int X, int Y) probeLocation)
{
    return probeLocation.X >= xTarget.Min && probeLocation.X <= xTarget.Max && probeLocation.Y >= yTarget.Min && probeLocation.Y <= yTarget.Max;
}
