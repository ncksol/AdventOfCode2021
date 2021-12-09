var input = new int[100][];

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var i = 0;
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine();
        input[i] = data.ToCharArray().Select(x => (char)x - '0').ToArray();
        i++;
    }
}

var sum = 0;

for (int i = 0; i < input.GetLength(0); i++)
{
    for (int j = 0; j < input[i].GetLength(0); j++)
    {
        var isLow = true;
        if (j - 1 >= 0)
        {
            isLow = input[i][j - 1] > input[i][j];
            if(!isLow) continue;
        }
        if (j + 1 < input[i].GetLength(0))
        {
            isLow = input[i][j + 1] > input[i][j];
            if (!isLow) continue;
        }
        if (i - 1 >= 0)
        {
            isLow = input[i - 1][j] > input[i][j];
            if (!isLow) continue;
        }
        if (i + 1 < input.GetLength(0))
        {
            isLow = input[i + 1][j] > input[i][j];
            if (!isLow) continue;
        }

        if (isLow)
            sum += input[i][j] + 1;
    }

}

Console.WriteLine(sum);
