var input = new List<int>(new int[] { 3, 4, 3, 1, 2 });

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var data = textReader.ReadToEnd();
    input = data.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
}

var fishes = new long[9];

foreach (var fish in input)
{
    fishes[fish]++;
}


for (int i = 0; i < 256; i++)
{
    var buffer = new long[9];
    for (int a = 0; a < fishes.Length; a++)
    {
        if(a == 0)
        {
            buffer[6] += fishes[a];
            buffer[8] += fishes[a];
        }
        else
        {
            buffer[a-1] += fishes[a];
        }
    }
    fishes = buffer;
}

var sum = fishes.Sum();

Console.WriteLine(sum);