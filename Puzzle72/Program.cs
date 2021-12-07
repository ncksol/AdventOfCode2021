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
            crabMove[curPos] += curPos < crabs[i] ? Calculate(curPos, crabs[i]) : Calculate(crabs[i], curPos);
        else
            crabMove.Add(curPos, curPos < crabs[i] ? Calculate(curPos, crabs[i]) : Calculate(crabs[i], curPos));
    }

    curPos++;
}

var minFuel = crabMove.MinBy(x => x.Value).Value;
Console.WriteLine(minFuel);


int Calculate(int start, int end)
{
    var sum = 0;
    var i = 0;
    while(start < end)
    {
        sum += i + 1;
        i++;
        start++;
    }

    return sum;
}