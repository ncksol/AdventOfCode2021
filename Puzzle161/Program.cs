var input = File.ReadAllText("input.txt");
input = "38006F45291200";
var inputBinary = "";
var result = 0;

foreach (var s in input)
{
    var binary = Convert.ToString(Convert.ToInt32(s.ToString(), 16), 2).PadLeft(4, '0');
    inputBinary += binary;
}

var mainPackageVersion = GetNextChunkOfDataDec(3, inputBinary);
result += mainPackageVersion;
var mainPackageTypeId = GetNextChunkOfDataDec(3, inputBinary);

if(mainPackageTypeId != 4)
{
    var lengthTypeId = GetNextChunkOfDataDec(1, inputBinary);
    if(lengthTypeId == 0)
    {
        var totalLength = GetNextChunkOfDataDec(15, inputBinary);
        var subPackets = GetNextChunkOfDataString(totalLength, inputBinary);

        while (subPackets.Length > 0)
        {
            var subPacketVersion = GetNextChunkOfDataDec(3, subPackets);
            result += subPacketVersion;
            var subPacketTypeId = GetNextChunkOfDataDec(3, subPackets);                       
        }

    }
}

Console.WriteLine(inputBinary);


int GetNextChunkOfDataDec(int chunkLength, string input)
{
    var data = Convert.ToInt32(input.Substring(0, chunkLength), 2);
    input = input.Remove(0, chunkLength);

    return data;
}

string GetNextChunkOfDataString(int chunkLength, string input)
{
    var data = input.Substring(0, chunkLength);
    input = input.Remove(0, chunkLength);

    return data;
}