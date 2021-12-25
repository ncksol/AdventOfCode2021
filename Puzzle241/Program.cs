var validNumbers = new List<string>();

for (long w1 = 9; w1 > 0; w1--)
{
    long z1 = w1 + 8;
    for (long w2 = 9; w2 > 0; w2--)
    {
        long z2 = z1 * 26 + w2 + 13;
        for (long w3 = 9; w3 > 0; w3--)
        {
            long z3 = z2 * 26 + w3 + 2;
            for (long w4 = 9; w4 > 0; w4--)
            {
                long z4 = 0;
                if (z3 % 26 == w4)
                    z4 = z3 / 26;
                else
                    continue;

                for (long w5 = 9; w5 > 0; w5--)
                {
                    long z5 = z4 * 26 + w5 + 11;

                    for (long w6 = 9; w6 > 0; w6--)
                    {
                        long z6 = z5 * 26 + w6 + 4;

                        for (long w7 = 9; w7 > 0; w7--)
                        {
                            long z7 = z6 * 26 + w7 + 13;

                            for (long w8 = 9; w8 > 0; w8--)
                            {
                                long z8 = 0;
                                if ((z7 % 26) - 8 == w8)
                                    z8 = z7 / 26;
                                else
                                    continue;

                                for (long w9 = 9; w9 > 0; w9--)
                                {
                                    long z9 = 0;
                                    if ((z8 % 26) - 9 == w9)
                                        z9 = z8 / 26;
                                    else
                                        continue;

                                    for (long w10 = 9; w10 > 0; w10--)
                                    {
                                        long z10 = z9 * 26 + w10 + 1;

                                        for (long w11 = 9; w11 > 0; w11--)
                                        {
                                            long z11 = 0;
                                            if ((z10 % 26) == w11)
                                                z11 = z10 / 26;
                                            else
                                                continue;

                                            for (long w12 = 9; w12 > 0; w12--)
                                            {
                                                long z12 = 0;
                                                if ((z11 % 26) - 5 == w12)
                                                    z12 = z11 / 26;
                                                else
                                                    continue;

                                                for (long w13 = 9; w13 > 0; w13--)
                                                {
                                                    long z13 = 0;
                                                    if ((z12 % 26) - 6 == w13)
                                                        z13 = z12 / 26;
                                                    else
                                                        continue;

                                                    for (long w14 = 9; w14 > 0; w14--)
                                                    {
                                                        long z14 = 0;
                                                        if ((z13 % 26) - 12 == w14)
                                                            z14 = z13 / 26;
                                                        else
                                                            continue;

                                                        var number = String.Join(null, w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12, w13, w14);
                                                        if (z14 == 0)
                                                        {
                                                            validNumbers.Add(number);                                                            
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

Console.WriteLine(validNumbers.Min());


/*var registers = new Dictionary<string, int>();
registers.Add("w", 0);
registers.Add("x", 0);
registers.Add("y", 0);
registers.Add("z", 0);

var program = File.ReadAllLines("input.txt");

var input = 13579246899999;
var inputCount = 14;

//input = 2;
//inputCount = 1;

foreach (var line in program)
{
    Execute(line);
}

Console.WriteLine($"");

void Execute(string line)
{
    var operation = line.Substring(0, 3);
    var vars = line.Substring(4).Split(' ', StringSplitOptions.RemoveEmptyEntries);
    int a, b = 0;
    a = registers[vars[0]];
    if (vars.Length > 1)
    {
        if (registers.ContainsKey(vars[1]))
            b = registers[vars[1]];
        else if (int.TryParse(vars[1], out b) == false)
            return;
    }

    switch (operation)
    {
        case ("add"):
            {
                a += b;
                break;
            }

        case ("mul"):
            {
                a *= b;
                break;
            }

        case ("div"):
            {
                a /= b;
                break;
            }

        case ("mod"):
            {
                a %= b;
                break;
            }

        case ("eql"):
            {
                a = a == b ? 1 : 0;
                break;
            }

        case ("inp"):
            {
                a = (int)((input / (Math.Pow(10, inputCount - 1))) % 10);
                inputCount--;
                break;
            }
    }

    registers[vars[0]] = a;
}*/