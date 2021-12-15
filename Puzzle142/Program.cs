var input = File.ReadAllLines("input.txt");

var template = input[0];
var insertionRules = input.Skip(2).ToArray();

var dictionary = new Dictionary<string, double>();
var charCount = new Dictionary<string, double>();
for (var i = 0; i < template.Length; i++)
{
    charCount = IncreaseDictionary(charCount, template.Substring(i, 1), 1);

    if (i + 1 >= template.Length) continue;
    dictionary = IncreaseDictionary(dictionary, template.Substring(i, 2), 1);
}

for (var i = 0; i < 40; i++)
{
    var bufferDict = new Dictionary<string, double>(dictionary);
    foreach (var rule in insertionRules)
    {
        var ruleSplit = rule.Split(" -> ");
        if (!dictionary.ContainsKey(ruleSplit[0])) continue;
        bufferDict[ruleSplit[0]] -= dictionary[ruleSplit[0]];
        var rule1 = ruleSplit[0].ToCharArray()[0] + ruleSplit[1];
        var rule2 = ruleSplit[1] + ruleSplit[0].ToCharArray()[1];

        bufferDict = IncreaseDictionary(bufferDict, rule1, dictionary[ruleSplit[0]]);
        bufferDict = IncreaseDictionary(bufferDict, rule2, dictionary[ruleSplit[0]]);
        charCount = IncreaseDictionary(charCount, ruleSplit[1], dictionary[ruleSplit[0]]);
    }
    dictionary = new Dictionary<string, double>(bufferDict);
}
Console.WriteLine($"{charCount.Values.Max() - charCount.Values.Min()}");

Dictionary<string, double> IncreaseDictionary(Dictionary<string, double> dict, string key, double value)
{
    if (!dict.ContainsKey(key))
        dict.Add(key, 0);

    dict[key] += value;

    return dict;
}