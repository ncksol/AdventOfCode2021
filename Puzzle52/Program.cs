using System.Drawing;

var input = new List<Tuple<Point, Point>>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine();
        var coords = data.Split("->", StringSplitOptions.RemoveEmptyEntries);
        var p1 = coords[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
        var p2 = coords[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

        var vent = new Tuple<Point, Point>(new Point(Int32.Parse(p1[0]), Int32.Parse(p1[1])), new Point(Int32.Parse(p2[0]), Int32.Parse(p2[1])));
        input.Add(vent);
    }
}

var checkGrid = new Dictionary<(int, int), int>();

foreach (var vent in input)
{
    (int x1, int y1, int x2, int y2) = (vent.Item1.X, vent.Item1.Y, vent.Item2.X, vent.Item2.Y);
    int xStep = x1 == x2 ? 0 : x1 > x2 ? -1 : 1;
    int yStep = y1 == y2 ? 0 : y1 > y2 ? -1 : 1;

    (int x, int y) = (x1, y1);
    
    do
    {
        AddPoint(x, y);
        if ((x, y) == (x2, y2)) break;
        x += xStep; y += yStep;
    } while (true);
}

var a = checkGrid.Count(x => x.Value > 1);

Console.WriteLine();


void AddPoint(int x, int y)
{
    if (checkGrid.ContainsKey((x, y)))
        checkGrid[(x, y)]++;
    else
        checkGrid.Add((x, y), 1);
}