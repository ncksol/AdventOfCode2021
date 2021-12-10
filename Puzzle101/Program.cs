var input = new List<string>();

var code = new List<(char start, char end, int price)>();
code.Add(('(', ')', 3));
code.Add(('[', ']', 57));
code.Add(('{', '}', 1197));
code.Add(('<', '>', 25137));

var startingChars = new Dictionary<char, char>();
startingChars.Add('(', ')');
startingChars.Add('[', ']');
startingChars.Add('{', '}');
startingChars.Add('<', '>');

var endingChars = new Dictionary<char, char>();
endingChars.Add(')', '(');
endingChars.Add(']', '[');
endingChars.Add('}', '{');
endingChars.Add('>', '<');

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var i = 0;
    while (textReader.EndOfStream == false)
    {
        input.Add(textReader.ReadLine());
    }
}
var sum = 0;
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
                sum += code.FirstOrDefault(x => x.end == c).price;
                break;
            }
        }
    }
}

Console.WriteLine(sum.ToString());