using System.Linq;

var input = new List<KeyValuePair<List<string>, string[]>>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    while (textReader.EndOfStream == false)
    {
        var data = textReader.ReadLine();
        var line = data.Split("|", StringSplitOptions.RemoveEmptyEntries);
        var signal = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        var output = line[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        input.Add(new KeyValuePair<List<string>, string[]>(signal, output));
    }
}

var sum = 0;
var decode = new Dictionary<string, int>();

foreach (var line in input)
{
    var code = line.Key.First(x => x.Length == 2);
    decode.Add(code, 1);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 4);
    decode.Add(code, 4);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 3);
    decode.Add(code, 7);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 7);
    decode.Add(code, 8);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 5 && GetCodeByDigit(1).All(y => x.Contains(y)));
    decode.Add(code, 3);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 6 && !GetCodeByDigit(1).All(y => x.Contains(y)));
    decode.Add(code, 6);
    line.Key.Remove(code);

    var c = GetCodeByDigit(8).First(x => GetCodeByDigit(6).Contains(x) == false);
    var f = GetCodeByDigit(1).First(x => x != c);

    code = line.Key.First(x => x.Length == 5 && x.Contains(c) && !x.Contains(f));
    decode.Add(code, 2);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 5 && !x.Contains(c) && x.Contains(f));
    decode.Add(code, 5);
    line.Key.Remove(code);

    code = line.Key.First(x => x.Length == 6 && GetCodeByDigit(4).All(y => x.Contains(y)));
    decode.Add(code, 9);
    line.Key.Remove(code);

    decode.Add(line.Key.First(), 0);

    var outputValue = "";
    foreach (var digitCode in line.Value)
    {
        var digit = GetDigitByCode(digitCode);
        outputValue += digit.ToString();
    }

    sum += int.Parse(outputValue);

    decode.Clear();
}

Console.WriteLine(sum);

//986163

string GetCodeByDigit(int digit)
{
    return decode.First(x => x.Value == digit).Key;
}

int GetDigitByCode(string code)
{
    return decode.First(x => code.Length == x.Key.Length && code.All(y => x.Key.Contains(y))).Value;
}
