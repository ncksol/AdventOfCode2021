using System.Text.RegularExpressions;

var grid = new HashSet<(int X, int Y, int Z)>();
var instructions = File.ReadAllLines("input.txt").ToList();

var x = new List<(int Start, int End)>();

foreach (var instruction in instructions)
{
    var action = instruction.Substring(0, 2) == "on";
    var matches = Regex.Matches(instruction, @"(-?\d+)");
    var xCoords = matches.Take(2).Select(x => int.Parse(x.Value)).ToArray();
    var yCoords = matches.Skip(2).Take(2).Select(x => int.Parse(x.Value)).ToArray();
    var zCoords = matches.Skip(4).Take(2).Select(x => int.Parse(x.Value)).ToArray();

    if(PartOneFilter(xCoords, yCoords, zCoords) == false) continue;

    for (int a = xCoords[0]; a <= xCoords[1]; a++)
    {
        for (int b = yCoords[0]; b <= yCoords[1]; b++)
        {
            for (int c = zCoords[0]; c <= zCoords[1]; c++)
            {
                if(action)
                    grid.Add((a, b, c));
                else
                    grid.Remove((a, b, c));
            }
        }
    }
}

Console.WriteLine(grid.Count);


bool PartOneFilter(int[] xCoords, int[] yCoords, int[] zCoords)
{
    var filter = new Func<int[], bool>((int[] coords) =>
    {
        return coords[0] >= -50 && coords[0] <= 50 && coords[1] >= -50 && coords[1] <= 50;
    });

    return filter(xCoords) && filter(yCoords) && filter(zCoords);
}