var input = new List<char[]>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine();
        input.Add(data.ToCharArray());
    }
}

char[,] matrix = new char[input[0].Length,input.Count];

for (int i = 0; i < matrix.GetLength(0); i++)
{
    var a = input.Count - 1;
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        matrix[i,j] = input[a][i];
        a--;
    }
}

var gammaBinary = "";

for (int i = 0; i < matrix.GetLength(0); i++)
{
    var ones = 0;
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        if(matrix[i,j] == '1') ones++;
    }

    if(ones > matrix.GetLength(1) - ones)
        gammaBinary += '1';
    else
        gammaBinary += '0';
}

var totalBinary = new string('1', gammaBinary.Length);

var gamma = Convert.ToInt32(gammaBinary, 2);
var total = Convert.ToInt32(totalBinary, 2);
var epsilon = total - gamma;

Console.WriteLine(gamma * epsilon);
