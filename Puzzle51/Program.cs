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

var filteredInput = input.Where(x => (x.Item1.X == x.Item2.X) || (x.Item1.Y == x.Item2.Y) ).ToList();

var checkGrid = new Dictionary<Point, int>();

foreach (var vent in filteredInput)
{
    if(vent.Item1.X == vent.Item2.X)
    {
        if(vent.Item1.Y < vent.Item2.Y)
        {
            for (int i = vent.Item1.Y; i <= vent.Item2.Y; i++)
            {
                var p = new Point(vent.Item1.X, i);

                if (checkGrid.ContainsKey(p))
                    checkGrid[p]++;
                else
                    checkGrid.Add(p, 1);
            }
        }
        else
        {
            for (int i = vent.Item2.Y; i <= vent.Item1.Y; i++)
            {
                var p = new Point(vent.Item1.X, i);

                if (checkGrid.ContainsKey(p))
                    checkGrid[p]++;
                else
                    checkGrid.Add(p, 1);
            }
        }
    }
    else if (vent.Item1.Y == vent.Item2.Y)
    {
        if(vent.Item1.X < vent.Item2.X)
        { 
            for (int i = vent.Item1.X; i <= vent.Item2.X; i++)
            {
                var p = new Point(i, vent.Item1.Y);

                if (checkGrid.ContainsKey(p))
                    checkGrid[p]++;
                else
                    checkGrid.Add(p, 1);
            }
        }
        else
        {
            for (int i = vent.Item2.X; i <= vent.Item1.X; i++)
            {
                var p = new Point(i, vent.Item1.Y);

                if (checkGrid.ContainsKey(p))
                    checkGrid[p]++;
                else
                    checkGrid.Add(p, 1);
            }
        }
    }
}

var a = checkGrid.Where(x => x.Value > 1).Count();

Console.WriteLine();