var input = File.ReadAllText("input.txt");
//input = "9C0141080250320F1802104A08";
var inputBinary = "";
var packageVersionSum = 0;

foreach (var s in input)
{
    var binary = Convert.ToString(Convert.ToInt32(s.ToString(), 16), 2).PadLeft(4, '0');
    inputBinary += binary;
}

long mainPackageResult = 0;

var mainPackageVersion = GetNextChunkOfDataDec(3, ref inputBinary);
packageVersionSum += mainPackageVersion;
var mainPackageTypeId = GetNextChunkOfDataDec(3, ref inputBinary);

if (mainPackageTypeId != 4)
    mainPackageResult = ProcessOperatorPacket(ref inputBinary, mainPackageTypeId);
else
    ProcessLiteralValuePacket(ref inputBinary);

Console.WriteLine(mainPackageResult);


int GetNextChunkOfDataDec(int chunkLength, ref string input)
{
    var data = Convert.ToInt32(input.Substring(0, chunkLength), 2);
    input = input.Remove(0, chunkLength);

    return data;
}

string GetNextChunkOfDataString(int chunkLength, ref string input)
{
    var data = input.Substring(0, chunkLength);
    input = input.Remove(0, chunkLength);

    return data;
}

long ProcessOperatorPacket(ref string input, int operatorPacketTypeId)
{
    long result = 0;
    var literals = new List<long>();
    var lengthTypeId = GetNextChunkOfDataDec(1, ref input);
    if (lengthTypeId == 0)
    {
        var totalLength = GetNextChunkOfDataDec(15, ref input);
        var subPackets = GetNextChunkOfDataString(totalLength, ref input);

        while (subPackets.Length > 0)
        {
            literals.Add(ProcessSubpackets(ref subPackets));
        }
    }
    else if (lengthTypeId == 1)
    {
        var subPacketsCount = GetNextChunkOfDataDec(11, ref input);

        while (subPacketsCount > 0)
        {
            literals.Add(ProcessSubpackets(ref input));

            subPacketsCount--;
        }
    }

    switch (operatorPacketTypeId)
    {
        case (0):
            result = literals.Sum();
            break;

        case (1):
            result = literals.Aggregate((x, y) => x * y);
            break;

        case (2):
            result = literals.Min();
            break;

        case(3):
            result = literals.Max();
            break;

        case(5):
            result = literals[0] > literals[1] ? 1 : 0;
            break;

        case(6):
            result = literals[0] < literals[1] ? 1 : 0;
            break;

        case(7):
            result = literals[0] == literals[1] ? 1 : 0;
            break;

        default:
            break;
    }

    return result;
}

long ProcessLiteralValuePacket(ref string input)
{
    var groupEndIndicator = GetNextChunkOfDataString(1, ref input);
    var packetValue = GetNextChunkOfDataString(4, ref input);
    while (groupEndIndicator != "0")
    {
        groupEndIndicator = GetNextChunkOfDataString(1, ref input);
        packetValue += GetNextChunkOfDataString(4, ref input);
    }

    return Convert.ToInt64(packetValue, 2);
}

long ProcessSubpackets(ref string input)
{
    long returnValue = 0;

    var subPacketVersion = GetNextChunkOfDataDec(3, ref input);
    packageVersionSum += subPacketVersion;
    var subPacketTypeId = GetNextChunkOfDataDec(3, ref input);

    if (subPacketTypeId != 4)
        returnValue = ProcessOperatorPacket(ref input, subPacketTypeId);
    else
        returnValue = ProcessLiteralValuePacket(ref input);

    return returnValue;
}