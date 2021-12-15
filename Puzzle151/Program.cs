var input = File.ReadAllLines("input.txt");

var visited = new List<(int X, int Y)>();
var distance = new Dictionary<(int X, int Y), int>();
var queue = new Dictionary<(int X, int Y), int>();

for (int i = 0; i < input[0].Length; i++)
{
    for (int j = 0; j < input.Length; j++)
    {
        distance.Add((i, j), int.MaxValue);
        queue.Add((i, j), Convert.ToInt32(input[i][j] - '0'));
    }
}

distance[(0, 0)] = 0;

while (queue.Count > 0)
{
    var minD = distance.Where(x => queue.ContainsKey(x.Key)).MinBy(x => x.Value);
    var node = queue.First(x => x.Key == minD.Key);
    queue.Remove(node.Key);

    var childNodes = GetChildNodes(node.Key.X, node.Key.Y, queue);
    foreach (var child in childNodes)
    {
        var alt = distance[node.Key] + child.Cost;
        if (alt < distance[(child.X, child.Y)])
            distance[(child.X, child.Y)] = alt;
    }
}

(int X, int Y) traceBackNode = (input[0].Length - 1, input.Length - 1);
var totalRisk = Convert.ToInt32(input[0][0] - '0');

while (traceBackNode.X != 0 && traceBackNode.Y != 0)
{
    var childNodes = GetChildNodes(traceBackNode.X, traceBackNode.Y, distance);
    var minChild = childNodes.MinBy(x => x.Cost);
    totalRisk += Convert.ToInt32(input[minChild.X][minChild.Y] - '0');
    traceBackNode = (minChild.X, minChild.Y);
}

Console.WriteLine(totalRisk);
List<(int X, int Y, int Cost)> GetChildNodes(int i, int j, Dictionary<(int X, int Y), int> source)
{
    var childNodes = new List<(int X, int Y, int Cost)>();

    if (source.ContainsKey((i - 1, j)))
        childNodes.Add((i - 1, j, source[(i - 1, j)]));
    if (source.ContainsKey((i, j - 1)))
        childNodes.Add((i, j - 1, source[(i, j - 1)]));
    if (source.ContainsKey((i + 1, j)))
        childNodes.Add((i + 1, j, source[(i + 1, j)]));
    if (source.ContainsKey((i, j + 1)))
        childNodes.Add((i, j + 1, source[(i, j + 1)]));

    return childNodes;
}
