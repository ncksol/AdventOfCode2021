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

var basins = new List<int>();
int basinSize = 0;
var lowPoints = GetLowPoints();
while(lowPoints.Count > 0)
{
    basinSize = 0;
    var point = lowPoints.First();
    floodFill(point.x, point.y);
    basins.Add(basinSize);
}

var sum = basins.OrderByDescending(x => x).Take(3).Aggregate((x,y) => x*y);

Console.WriteLine(sum);


void floodFill(int i, int j)
{
    if(i < 0 || j < 0 || i >= input.Length || j >= input[0].Length) return;
    if(input[i][j] == 9) return;

    basinSize++;
    input[i][j] = 9;
    lowPoints.Remove((i,j));

    floodFill(i+1, j);
    floodFill(i-1, j);
    floodFill(i, j-1);
    floodFill(i, j+1);
}

List<(int x, int y)> GetLowPoints()
{
    var lowPoints = new List<(int x, int y)>();

    for (int i = 0; i < input.GetLength(0); i++)
    {
        for (int j = 0; j < input[i].GetLength(0); j++)
        {
            var isLow = true;
            if (j - 1 >= 0)
            {
                isLow = input[i][j - 1] > input[i][j];
                if (!isLow) continue;
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
                lowPoints.Add((i,j));
        }

    }

    return lowPoints;
}