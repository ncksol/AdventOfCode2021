var crabs = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var data = textReader.ReadToEnd();
    crabs = data.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
}

Array.Sort(crabs);

var minPos = crabs.Min();
var maxPos = crabs.Max();
var curPos = minPos;

var crabMove = new Dictionary<int, int>();

while (curPos <= maxPos)
{
    for (var i = 0; i < crabs.Length; i++)
    {
        if (crabMove.ContainsKey(curPos))
            crabMove[curPos] += Math.Abs(curPos - crabs[i]);
        else
            crabMove.Add(curPos, Math.Abs(curPos - crabs[i]));
    }

    curPos++;
}

var minFuel = crabMove.MinBy(x => x.Value).Value;
Console.WriteLine(minFuel);
