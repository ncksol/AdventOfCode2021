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

//input = new List<int> { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263};

var count = 0;

var i = 2;
while (i + 1 < input.Count)
{
    var num1 = input[i - 2] + input[i - 1] + input[i];
    var num2 = input[i - 1] + input[i] + input[i + 1];

    if (num2 > num1)
        count++;

    i++;
}

Console.WriteLine(count.ToString());