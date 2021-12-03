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

var oxygenInput = input;

var i = 0;
while(oxygenInput.Count != 1)
{
    var ones = new List<char[]>();
    var zeroes = new List<char[]>();
    for (int j = 0; j < oxygenInput.Count; j++)
    {
        var num = oxygenInput[j][i];
        if (num == '1')
            ones.Add(oxygenInput[j]);
        else
            zeroes.Add(oxygenInput[j]);
    }

    if (zeroes.Count > ones.Count)
        oxygenInput = zeroes;
    else
        oxygenInput = ones;

    i++;
}

var co2Input = input;

i = 0;
while (co2Input.Count != 1)
{
    var ones = new List<char[]>();
    var zeroes = new List<char[]>();
    for (int j = 0; j < co2Input.Count; j++)
    {
        var num = co2Input[j][i];
        if (num == '1')
            ones.Add(co2Input[j]);
        else
            zeroes.Add(co2Input[j]);
    }

    if (ones.Count < zeroes.Count)
        co2Input = ones;
    else
        co2Input = zeroes;

    i++;
}

var co2 = Convert.ToInt32(new string(co2Input[0]), 2);
var oxygen = Convert.ToInt32(new string(oxygenInput[0]), 2);

Console.WriteLine(co2 * oxygen);