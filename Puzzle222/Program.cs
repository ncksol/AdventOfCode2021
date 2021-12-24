using System.Text.RegularExpressions;

var grid = new HashSet<(int X, int Y, int Z)>();
var instructions = File.ReadAllLines("input.txt").ToList();

var gridX = new List<(int? Start, int? End)>();
var gridY = new List<(int? Start, int? End)>();
var gridZ = new List<(int? Start, int? End)>();

foreach (var instruction in instructions)
{
    var action = instruction.Substring(0, 2) == "on";
    var matches = Regex.Matches(instruction, @"(-?\d+)");
    (int Start, int End) xCoords = (matches.Take(2).Select(x => int.Parse(x.Value)).ElementAt(0), matches.Take(2).Select(x => int.Parse(x.Value)).ElementAt(1));
    (int Start, int End) yCoords = (matches.Skip(2).Take(2).Select(x => int.Parse(x.Value)).ElementAt(0), matches.Skip(2).Take(2).Select(x => int.Parse(x.Value)).ElementAt(1));
    (int Start, int End) zCoords = (matches.Skip(4).Take(2).Select(x => int.Parse(x.Value)).ElementAt(0), matches.Skip(4).Take(2).Select(x => int.Parse(x.Value)).ElementAt(1));

    if (PartOneFilter(xCoords, yCoords, zCoords) == false) continue;

    Work(gridX, action, xCoords);
    Work(gridY, action, yCoords);
    Work(gridZ, action, zCoords);

    /*if (action)
    {
        //new outside start and end
        if (gridX.Count == 0 || gridX.All(c => c.Start > coords.End) || gridX.All(c => c.End < coords.Start))
        {
            gridX.Add((coords.Start, coords.End));
        }
        else
        {
            var startInside = gridX.FindIndex(c => c.Start > coords.Start && c.Start < coords.End);
            if (startInside != -1)
                gridX[startInside] = (coords.Start, gridX[startInside].End);
            var endInside = gridX.FindIndex(c => c.End < coords.End && c.End > coords.Start);
            if (endInside != -1)
                gridX[endInside] = (gridX[endInside].Start, coords.End);
        }
    }
    else
    {
        var allInside = gridX.FindIndex(c => c.Start < coords.Start && c.End > coords.End);
        if (allInside != -1)
        {
            var item = gridX[allInside];
            gridX[allInside] = (item.Start, coords.Start);
            gridX.Add((coords.End, item.End));
        }
        else
        {
            var startInside = gridX.FindIndex(c => c.Start > coords.Start && c.Start < coords.End);
            if (startInside != -1)
                gridX[startInside] = (coords.End, gridX[startInside].End);
            var endInside = gridX.FindIndex(c => c.End < coords.End && c.End > coords.Start);
            if (endInside != -1)
                gridX[endInside] = (gridX[endInside].Start, coords.Start);
        }
    }*/


    grid.Clear();

    for (var x = gridX[0].Start; x <= gridX[0].End; x++)
    {
        for (var y = gridY[0].Start; y <= gridY[0].End; y++)
        {
            for (var z = gridZ[0].Start; z <= gridZ[0].End; z++)
            {
                grid.Add((x.Value, y.Value, z.Value));
            }
        }
    }

}

foreach (var xLine in gridX)
{
    for (var x = xLine.Start; x <= xLine.End; x++)
    {
        foreach (var yLine in gridY)
        {
            for (var y = yLine.Start; y <= yLine.End; y++)
            {
                foreach (var zLine in gridZ)
                {
                    for (var z = zLine.Start; z <= zLine.End; z++)
                    {
                        grid.Add((x.Value, y.Value, z.Value));
                    }
                }
            }
        }
    }
}

Console.WriteLine(grid.Count);

void Work(List<(int? Start, int? End)> grid, bool action, (int Start, int End) coords)
{
    if (action)
    {
        if (grid.Count == 0 || grid.All(c => c.Start > coords.End) || grid.All(c => c.End < coords.Start))
        {
            grid.Add((coords.Start, coords.End));
        }
        else
        {
            var startInside = grid.FindIndex(c => c.Start > coords.Start && c.Start < coords.End);
            if (startInside != -1)
                grid[startInside] = (coords.Start, grid[startInside].End);
            var endInside = grid.FindIndex(c => c.End < coords.End && c.End > coords.Start);
            if (endInside != -1)
                grid[endInside] = (grid[endInside].Start, coords.End);
        }
    }
    else
    {
        var allInside = grid.FindIndex(c => c.Start < coords.Start && c.End > coords.End);
        if (allInside != -1)
        {
            var item = grid[allInside];
            grid[allInside] = (item.Start, coords.Start);
            grid.Add((coords.End, item.End));
        }
        else
        {
            var startInside = grid.FindIndex(c => c.Start > coords.Start && c.Start < coords.End);
            if (startInside != -1)
                grid[startInside] = (coords.End, grid[startInside].End);
            var endInside = grid.FindIndex(c => c.End < coords.End && c.End > coords.Start);
            if (endInside != -1)
                grid[endInside] = (grid[endInside].Start, coords.Start);
        }
    }
}


bool PartOneFilter((int Start, int End) xCoords, (int Start, int End) yCoords, (int Start, int End) zCoords)
{
    var filter = new Func<(int Start, int End), bool>(((int Start, int End) coords) =>
    {
        return coords.Start >= -50 && coords.Start <= 50 && coords.End >= -50 && coords.End <= 50;
    });

    return filter(xCoords) && filter(yCoords) && filter(zCoords);
}