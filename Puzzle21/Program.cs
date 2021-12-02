var input = new List<KeyValuePair<string, int>>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine().Split(' ');
        input.Add(new KeyValuePair<string, int>(data[0], Convert.ToInt32(data[1])));
    }
}

var hor = 0;
var dp = 0;

foreach (var move in input)
{
    switch (move.Key)
    { 
        case "forward":
            hor += move.Value;
            break;

        case "down":
            dp += move.Value;
            break;

        case "up":
            dp -= move.Value;
            break;

    }
}

Console.WriteLine(hor * dp);