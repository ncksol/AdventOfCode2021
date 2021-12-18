var input = "x=201..230, y=-99..-65";
//input = "x=20..30, y=-10..-5";

var split = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
var xSplit = split[0].Trim().Remove(0, 2).Split("..", StringSplitOptions.RemoveEmptyEntries);
var ySplit = split[1].Trim().Remove(0, 2).Split("..", StringSplitOptions.RemoveEmptyEntries);

(int Min, int Max) xTarget = (int.Parse(xSplit[0]), int.Parse(xSplit[1]));
(int Min, int Max) yTarget = (int.Parse(ySplit[0]), int.Parse(ySplit[1]));

var highestY = -1;

var minXVelocity = CalculateMinXVelocity();
var testYVelocity = 1;

var prevTryHighestY = -1;

while (true)
{
    var tryHighestY = 0;
    
    (int X, int Y) probeVelocity = (minXVelocity, testYVelocity);

    (int X, int Y) probeLocation = (0, 0);
    var previousProbeLocation = probeLocation;

    while (true)
    {
        probeLocation = (probeLocation.X + probeVelocity.X, probeLocation.Y + probeVelocity.Y);
        if (probeLocation.X > xTarget.Max)
        {
            tryHighestY = -1;
            break;
        }

        if (probeLocation.X < xTarget.Min && probeLocation.X <= previousProbeLocation.X)
        {
            tryHighestY = -1;
            break;
        }

        if(probeLocation.Y < yTarget.Min && probeVelocity.Y <= 0)
        {
            tryHighestY = -1;
            break;
        }

        if (probeLocation.Y > tryHighestY)
            tryHighestY = probeLocation.Y;

        if (probeLocation.X >= xTarget.Min && probeLocation.X <= xTarget.Max && probeLocation.Y >= yTarget.Min && probeLocation.Y <= yTarget.Max)
            break;

        if (probeVelocity.X > 0)
            probeVelocity.X--;
        else if (probeVelocity.X < 0)
            probeVelocity.X++;

        probeVelocity.Y--;

        previousProbeLocation = probeLocation;
    }

    if(tryHighestY > highestY)
        highestY = tryHighestY;
    else if(tryHighestY < prevTryHighestY && prevTryHighestY > 0 || testYVelocity > 1000)
        break;

    testYVelocity++;
}

Console.WriteLine(highestY);

int CalculateMinXVelocity()
{
    var a = 0.5;
    var b = 0.5;
    var c = xTarget.Min * -1;

    var d = Math.Pow(b, 2) - 4 * a * c;
    var x = (b * -1 + Math.Sqrt(d)) / (2 * a);

    return Convert.ToInt32(Math.Ceiling(x));
}