var input = new List<string[]>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine();
        var digits = data.Split("|", StringSplitOptions.RemoveEmptyEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        input.Add(digits);
    }
}

var one = 0;
var four = 0;
var seven = 0;
var eight = 0;

foreach (var line in input)
{
    one += line.Count(x => x.Length == 2);
    four += line.Count(x => x.Length == 4);
    seven += line.Count(x => x.Length == 3);
    eight += line.Count(x => x.Length == 7);
}

var sum = one + four + seven + eight;

Console.WriteLine(sum);