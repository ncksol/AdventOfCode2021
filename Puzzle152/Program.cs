var visited = new List<(int X, int Y)>();
var distance = new Dictionary<(int X, int Y), int>();
var queue = new Dictionary<(int X, int Y), int>();

var input = File.ReadAllLines("input.txt")
    .SelectMany((line, y) =>
        line.Select((c, x) => ((x, y), new Node(x, y, c - '0'))))
    .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

var width = (int)Math.Sqrt(input.Count);

var quintupleInput =
    Enumerable.Range(0, 5).SelectMany(i =>
        Enumerable.Range(0, 5).SelectMany(j =>
            input.Select(kvp =>
            {
                (int x, int y) newKey = (kvp.Key.x + width * i, kvp.Key.y + width * j);
                var newRisk = (kvp.Value.Risk + i + j - 1) % 9 + 1;
                return (newKey, new Node(newKey.x, newKey.y, newRisk));
            })))
    .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);


foreach (var item in quintupleInput)
{
    distance.Add((item.Key.x, item.Key.y), int.MaxValue);
    queue.Add((item.Key.x, item.Key.y), item.Value.Risk);
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

(int X, int Y) traceBackNode = quintupleInput.Last().Key;
var totalRisk = quintupleInput.First().Value.Risk;

while (traceBackNode.X != 0 || traceBackNode.Y != 0)
{
    var childNodes = GetChildNodes(traceBackNode.X, traceBackNode.Y, distance);
    var minChild = childNodes.MinBy(x => x.Cost);
    totalRisk += quintupleInput[(minChild.X, minChild.Y)].Risk;
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

class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Risk { get; set; }
    public Node(int x, int y, int risk)
    {
        X = x;
        Y = y;
        Risk = risk;
    }
}
