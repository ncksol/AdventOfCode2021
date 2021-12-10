var input = new List<string>();

var code = new List<(char start, char end, int price)>();
code.Add(('(', ')', 1));
code.Add(('[', ']', 2));
code.Add(('{', '}', 3));
code.Add(('<', '>', 4));

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        input.Add(textReader.ReadLine());
    }
}

var scores = new List<long>();
var corrupted = new List<string>();
foreach (var line in input)
{
    var stack = new Stack<char>();
    for (int i = 0; i < line.Length; i++)
    {
        var c = line[i];
        if (code.Any(x => x.start == c))
            stack.Push(c);
        else
        {
            var prevC = stack.Pop();
            if (code.Any(x => x.start == prevC && x.end == c) == false)
            {
                stack.Clear();
                corrupted.Add(line);
                break;
            }
        }
    }

    long score = 0;
    while (stack.Count > 0)
    {
        var c = stack.Pop();
        score = score * 5 + code.First(x => x.start == c).price;
    }

    if (score > 0)
        scores.Add(score);
}

scores.Sort();
var final = scores[scores.Count/2];

Console.WriteLine(final);