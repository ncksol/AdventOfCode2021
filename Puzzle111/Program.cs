var input = new int[10][];

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var i = 0;
    while (textReader.EndOfStream == false)
    {
        input[i] = textReader.ReadLine().Select(x => int.Parse(x.ToString())).ToArray();
        i++;
    }
}
var flashes = 0;
var counter = 0;
var primed = new Stack<(int i, int j)>();
var flashed = new List<(int i, int j)>();

while (counter < 2000)
{
    for (int i = 0; i < input.Length; i++)
    {
        for (int j = 0; j < input[0].Length; j++)
        {
            input[i][j]++;
            if(input[i][j] > 9)
                primed.Push((i, j));
        }
    }

    while (primed.Count > 0)
    {
        var octopus = primed.Pop();
        flashes++;
        flashed.Add(octopus);

        var nw = (octopus.i - 1, octopus.j - 1);
        var n = (octopus.i, octopus.j - 1);
        var ne = (octopus.i + 1, octopus.j - 1);
        var e = (octopus.i + 1, octopus.j);
        var se = (octopus.i + 1, octopus.j + 1);
        var s = (octopus.i, octopus.j + 1);
        var sw = (octopus.i - 1, octopus.j + 1);
        var w = (octopus.i - 1, octopus.j);

        FlashOctopus(nw);
        FlashOctopus(n);
        FlashOctopus(ne);
        FlashOctopus(e);
        FlashOctopus(se);
        FlashOctopus(s);
        FlashOctopus(sw);
        FlashOctopus(w);
    }

    if(flashed.Count == input.Length * input[0].Length)
        Console.WriteLine($"All octopus flash at step: {counter+1}");

    foreach (var octopus in flashed)
    {
        input[octopus.i][octopus.j] = 0;
    }

    counter++;
    flashed.Clear();
}

//Console.WriteLine(flashes);

void FlashOctopus((int i, int j) octopus)
{
    if(flashed.Contains(octopus)) return;
    if(primed.Contains(octopus)) return;

    var i = octopus.i;
    var j = octopus.j;

    if (i < 0 || i >= input.Length || j < 0 || j >= input[0].Length) return;

    input[i][j]++;
    if (input[i][j] > 9)
        primed.Push((i, j));
}

void Print()
{
    Console.WriteLine();
    for (int i = 0; i < input.Length; i++)
    {
        for (int j = 0; j < input[0].Length; j++)
        {
            if(input[i][j] == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write(input[i][j]);

            Console.ResetColor();
        }
        Console.WriteLine();
    }
}