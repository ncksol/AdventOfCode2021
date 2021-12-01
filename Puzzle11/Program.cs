var input = new List<int>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var num = Convert.ToInt32(textReader.ReadLine());
        input.Add(num);
    }
}

var count = 0;

for (int i = 0; i < input.Count; i++)
{
    if (i > 0 && input[i] > input[i - 1])
        count++;
}

Console.WriteLine(count.ToString());