using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

var elements = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

var template = "";
var rules = new List<(string Location, string Element)>();

var file = new FileInfo("input.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    template = textReader.ReadLine();

    while (textReader.EndOfStream == false)
    {
        var line = textReader.ReadLine();
        if (line.Contains("->"))
        {
            var rule = line.Split("->", StringSplitOptions.RemoveEmptyEntries);
            rules.Add((rule[0].Trim(), rule[1].Trim()));
        }
    }
}

var step = 0;
var stopWatch = new Stopwatch();
TimeSpan prevRun = new TimeSpan(0, 0, 0);

while (step < 40)
{
    var insertions = new Dictionary<int, (string Location, string Element)>();

    stopWatch.Start();

    //var sw = new Stopwatch();
    //sw.Start();
    foreach (var rule in rules)
    {
        var indices = IndexOfAll(template, rule.Location);

        foreach (var index in indices.Where(x => x != -1))
        {
            insertions.Add(index + 1, (rule.Location, rule.Element));
        }
    }
    //sw.Stop();
    //Console.WriteLine($"step {step}. find insertions run {sw.Elapsed.TotalSeconds}s. Num insertions: {insertions.Count}");

    //sw.Restart();
    var builder = new StringBuilder(template);
    foreach (var item in insertions.OrderByDescending(x => x.Key))
    {
        builder.Insert(item.Key, item.Value.Element);
    }

    //sw.Stop();
    //Console.WriteLine($"step {step}. build new string run {sw.Elapsed.TotalSeconds}s");

    template = builder.ToString();


    stopWatch.Stop();
    var curRun = stopWatch.Elapsed;

    Console.WriteLine($"step {step} done. Running time {curRun.TotalSeconds}s. Increase of {Math.Round((curRun.TotalSeconds / prevRun.TotalSeconds) * 100)}%{Environment.NewLine}");

    step++;
    prevRun = curRun;
    stopWatch.Reset();
}

var max = 0;
var min = int.MaxValue;

var aa = template.GroupBy(x => x).OrderBy(x => x.Count());

var result = aa.Last().Count() - aa.First().Count();
Console.WriteLine(result);

/*
var step = 0;
var stopWatch = new Stopwatch();
TimeSpan prevRun = new TimeSpan(0, 0, 0);

while (step < 40)
{
    stopWatch.Start();
    for (int i = 0; i < template.Length - 1; i++)
    {
        var sb = new StringBuilder(template);

        var rule = rules.FirstOrDefault(x => x.Location == $"{sb[i]}{sb[i+1]}");
        if (rule == default) continue;

        sb.Insert(i+1, rule.Element);
        template = sb.ToString();
        
        //template = template.Insert(i + 1, rule.Element);
        i++;
    }

    stopWatch.Stop();
    var curRun = stopWatch.Elapsed;

    Console.WriteLine($"step {step} done. Running time {curRun.TotalSeconds}s. Increase of {Math.Round((curRun.TotalSeconds / prevRun.TotalSeconds) * 100)}%");

    step++;
    prevRun = curRun;
    stopWatch.Reset();
}

var max = 0;
var min = int.MaxValue;

var aa = template.GroupBy(x => x).OrderBy(x => x.Count());

var result = aa.Last().Count() - aa.First().Count();
Console.WriteLine(result);
*/
IEnumerable<int> IndexOfAll(string str, string value)
{
    var indices = new List<int>();
    for (int i = 0; i < str.Length-1; i++)
    {
        var window = str.Substring(i, 2);
        if (window == value)
            indices.Add(i);
    }

    return indices;
}