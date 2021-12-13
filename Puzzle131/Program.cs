var input = new List<(int X, int Y)>();
var input2 = new List<string>();

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var i = 0;
    while (textReader.EndOfStream == false)
    {
        var line = textReader.ReadLine();
        if(line.Contains(','))
        {
            var dot = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            input.Add((dot[0], dot[1]));
        }
        else if(line.Contains("fold along"))
        {
            input2.Add(line);
        }
    }
}

bool[,] dots = new bool[input.Max(d => d.X)+1, input.Max(d => d.Y)+1];

foreach (var dot in input)
{
    dots[dot.X, dot.Y] = true;
}

var folds = new List<(string Key, int Value)>();
foreach(var line in input2)
{
    var fold1 = line.Replace("fold along ", string.Empty);
    var split = fold1.Split("=", StringSplitOptions.RemoveEmptyEntries);
    folds.Add((split[0], int.Parse(split[1])));
}

bool[,] buffer = dots;

foreach (var fold in folds)
{
    bool[,] bufferCopy;

    if(fold.Key == "x")
    {
        bufferCopy = new bool[fold.Value, buffer.GetLength(1)];

        for (int i = 0; i < buffer.GetLength(0); i++)
        {
            for (int j = 0; j < buffer.GetLength(1); j++)
            {
                if(i == fold.Value) continue;

                var shifted = i > fold.Value ? Math.Abs(i - fold.Value*2) : i;
                bufferCopy[shifted, j] = buffer[i, j] ? buffer[i, j] : bufferCopy[shifted, j];
            }
        }
    }
    else
    {
        bufferCopy = new bool[buffer.GetLength(0), fold.Value];

        for (int i = 0; i < buffer.GetLength(0); i++)
        {
            for (int j = 0; j < buffer.GetLength(1); j++)
            {
                if(j == fold.Value) continue;

                var shifted = j > fold.Value ? Math.Abs(j - fold.Value * 2) : j;
                bufferCopy[i, shifted] = buffer[i, j] ? buffer[i,j] : bufferCopy[i, shifted];
            }
        }
    }

    buffer = new bool[bufferCopy.GetLength(0), bufferCopy.GetLength(1)];
    buffer = bufferCopy;

    Print(buffer);
    Console.WriteLine("");
}


void Print(bool[,] output)
{
    for (int i = 0; i < output.GetLength(1); i++)
    {
        var line = "";
        for (int j = 0; j < output.GetLength(0); j++)
        {
            line += output[j, i] ? '#' : '.';
        }
        Console.WriteLine(line);
    }
}