var input = File.ReadAllLines("input.txt");
//input = new string[] {"[1,1]", "[2,2]", "[3,3]", "[4,4]", "[5,5]"};

var isNumberTwo = true;

if(isNumberTwo == false)
    NumberOne();
else
    NumberTwo();

void NumberOne()
{
    var inputArray = input.First().Select(x => Convert.ToString(x)).ToList();

    foreach (var line in input.Skip(1))
    {
        var i = 0;
        var leftNumberIndex = -1;
        var openBracketCount = 0;
        var splitNumberIndex = -1;

        inputArray = Addition(inputArray, line.Select(x => Convert.ToString(x)).ToList());
        while (i < inputArray.Count)
        {
            int number = int.MinValue;
            var c = inputArray[i];
            if (c == "[")
                openBracketCount++;
            else if (int.TryParse(c, out number))
                leftNumberIndex = i;
            else if (c == "]")
                openBracketCount--;

            if (openBracketCount >= 5 && inputArray[i + 4] == "]")
            {
                Explode(inputArray, i + 1, leftNumberIndex);
                i = 0;
                openBracketCount = 0;
                leftNumberIndex = -1;
                splitNumberIndex = -1;
                continue;
            }
            else if (splitNumberIndex == -1 && int.TryParse(c, out var v) && v >= 10)
            {
                splitNumberIndex = i;
            }

            i++;

            if (i == inputArray.Count - 1 && splitNumberIndex != -1)
            {
                Split(inputArray, splitNumberIndex);
                i = 0;
                openBracketCount = 0;
                leftNumberIndex = -1;
                splitNumberIndex = -1;
            }
        }
    }

    Console.WriteLine(String.Join(null, inputArray));
    Console.WriteLine(Magnitude(inputArray));

}

void NumberTwo()
{
    var maxMagnitude = int.MinValue;
    var pairs = Recombine(input);

    foreach (var item in pairs)
    {
        var i = 0;
        var leftNumberIndex = -1;
        var openBracketCount = 0;
        var splitNumberIndex = -1;

        var inputArray = Addition(item.Number1, item.Number2);
        while (i < inputArray.Count)
        {
            int number = int.MinValue;
            var c = inputArray[i];
            if (c == "[")
                openBracketCount++;
            else if (int.TryParse(c, out number))
                leftNumberIndex = i;
            else if (c == "]")
                openBracketCount--;

            if (openBracketCount >= 5 && inputArray[i + 4] == "]")
            {
                Explode(inputArray, i + 1, leftNumberIndex);
                i = 0;
                openBracketCount = 0;
                leftNumberIndex = -1;
                splitNumberIndex = -1;
                continue;
            }
            else if (splitNumberIndex == -1 && int.TryParse(c, out var v) && v >= 10)
            {
                splitNumberIndex = i;
            }

            i++;

            if (i == inputArray.Count - 1 && splitNumberIndex != -1)
            {
                Split(inputArray, splitNumberIndex);
                i = 0;
                openBracketCount = 0;
                leftNumberIndex = -1;
                splitNumberIndex = -1;
            }
        }

        var magnitude = Magnitude(inputArray);
        if(magnitude > maxMagnitude) maxMagnitude = magnitude;
    }

    Console.WriteLine(maxMagnitude);
}

void Explode(List<string> inputArray, int explodeIndex, int leftNumberIndex)
{
    var k = explodeIndex + 4;
    var rightNumberIndex = -1;
    while (rightNumberIndex == -1 && k < inputArray.Count)
    {
        var c = inputArray[k];
        if (int.TryParse(c, out _))
            rightNumberIndex = k;

        k++;
    }

    (int L, int R) explodingNumber = (int.Parse(inputArray[explodeIndex]), int.Parse(inputArray[explodeIndex + 2]));

    if (leftNumberIndex != -1)
    {
        var leftNumber = int.Parse(inputArray[leftNumberIndex]);
        leftNumber += explodingNumber.L;
        inputArray[leftNumberIndex] = leftNumber.ToString();
    }

    if (rightNumberIndex != -1)
    {
        var rightNumber = int.Parse(inputArray[rightNumberIndex]);
        rightNumber += explodingNumber.R;
        inputArray[rightNumberIndex] = rightNumber.ToString();
    }

    inputArray[explodeIndex - 1] = "0";

    inputArray.RemoveRange(explodeIndex, 4);
}

void Split(List<string> inputArray, int splitNumberIndex)
{
    var num = int.Parse(inputArray[splitNumberIndex]);

    var leftNumber = (int)Math.Floor((double)num / 2);
    var rightNumber = (int)Math.Ceiling((double)num / 2);

    inputArray.RemoveAt(splitNumberIndex);
    inputArray.InsertRange(splitNumberIndex, new string[] { "[", leftNumber.ToString(), ",", rightNumber.ToString(), "]" });
}

List<string> Addition(List<string> number1, List<string> number2)
{
    var result = number1.ToList();
    result.Add(",");
    result.AddRange(number2);

    result.Insert(0, "[");
    result.Add("]");

    return result;
}

int Magnitude(List<string> number)
{
    var i = 0;
    while (i < number.Count && number.Count != 1)
    {
        var c = number[i];
        if (c == "[" && number[i + 4] == "]")
        {
            var left = int.Parse(number[i + 1]);
            var right = int.Parse(number[i + 3]);
            var magnitude = left * 3 + right * 2;

            number.RemoveRange(i, 5);
            number.Insert(i, magnitude.ToString());
            i = 0;
        }
        else i++;
    }

    return int.Parse(number.First());
}

List<(List<string> Number1, List<string> Number2)> Recombine(string[] list)
{
    var result = new List<(List<string> Number1, List<string> Number2)>();

    for (int i = 0; i < list.Length; i++)
    {
        for (int j = 0; j < list.Length; j++)
        {
            if(i == j) continue;

            result.Add((list[i].Select(x => Convert.ToString(x)).ToList(), list[j].Select(x => Convert.ToString(x)).ToList()));
        }
    }

    return result;
}
