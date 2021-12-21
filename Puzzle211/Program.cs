int currentRoll = 1;
int rollCount = 0;
int result = 0;

var score1 = new List<int>();

var pos1 = new List<(int Position, int Score)> { (2, 0) };
var pos2 = 5;

var isSecond = false;

var p1Wins = 0;

var rolls = RollQuantumDie();

var res = Game(2, 0, 5, 0, true);

Console.WriteLine($"{res.Win1} {res.Win2}");




(long Win1, long Win2) Game(int pos1, int sco1, int pos2, int sco2, bool p1Turn)
{
    long win1 = 0;
    long win2 = 0;

    foreach (var roll in rolls)
    {
        int pos1c = pos1, sco1c = sco1, pos2c = pos2, sco2c = sco2;
        var gameOver = false;

        if (p1Turn)
        {
            pos1c += roll.Roll;
            if (pos1c > 10) pos1c -= 10;

            sco1c += pos1c;
            if (sco1c >= 21)
            {
                win1 += roll.Count;
                gameOver = true;
            }
        }
        else
        {
            pos2c += roll.Roll;
            if (pos2c > 10) pos2c -= 10;

            sco2c += pos2c;
            if (sco2c >= 21)
            {
                win2 += roll.Count;
                gameOver = true;
            }
        }

        if (gameOver == false)
        {
            var res = Game(pos1c, sco1c, pos2c, sco2c, !p1Turn);
            win1 += res.Win1 * roll.Count;
            win2 += res.Win2 * roll.Count;
        }
    }


    return (win1, win2);
}


/*
long win1 = 0;
long win2 = 0;

foreach (var roll in rolls)
{
    int pos1c = pos1, sco1c = sco1, pos2c = pos2, sco2c = sco2;
    var gameOver = false;

    if (p1Turn)
    {
        pos1c += roll.Roll;
        if (pos1c > 10) pos1c -= 10;

        sco1c += pos1c;
        if (sco1c >= 21)
        {
            win1 += roll.Count;
            gameOver = true;
        }
    }
    else
    {
        pos2c += roll.Roll;
        if (pos2c > 10) pos2c -= 10;

        sco2c += pos2c;
        if (sco2c >= 21)
        {
            win2 += roll.Count;
            gameOver = true;
        }
    }

    if (gameOver == false)
    {
        var res = Game(pos1c, sco1c, pos2c, sco2c, !p1Turn);
        win1 += res.Win1;
        win2 += res.Win2;
    }
}*/

/*
while (true)
{
    var newPositions = new List<(int Position, int Score)>();
    var p1RollSum = RollQuantumDie();
    foreach (var roll in p1RollSum)
    {        
        foreach (var curPos in pos1)
        {
            var newPos = TranslatePosition(curPos.Position + roll);
            newPositions.Add((newPos, curPos.Score + newPos));
        }
    }

    p1Wins += newPositions.Count(x => x.Score >= 21);
    pos1 = newPositions.Where(x => x.Score < 21).ToList();
}*/

/*
while(true)
{
    var p1RollSum = RollDie();
    pos1 = TranslatePosition(pos1 + p1RollSum);
    score1 += pos1;
    if(score1 >= 1000)
    {
        result = score2 * rollCount;
        break;
    }

    var p2RollSum = RollDie();
    pos2 = TranslatePosition(pos2 + p2RollSum);
    score2 += pos2;
    if (score2 >= 1000)
    {
        result = score1 * rollCount;
        break;
    }
}*/
/*
Console.WriteLine($"score1: {score1}");
Console.WriteLine($"score2: {score2}");
Console.WriteLine($"rollCount: {rollCount}");
Console.WriteLine($"answer: {result}");
*/
int RollDie()
{
    rollCount += 3;
    return Enumerable.Range(1, 3).Select(x => currentRoll++).Sum();
}

int TranslatePosition(int value)
{
    value = value % 10;
    return value == 0 ? 10 : value;
}

List<(int Roll, int Count)> RollQuantumDie()
{
    var rolls = new List<int>();
    for (int i = 1; i <= 3; i++)
    {
        for (int j = 1; j <= 3; j++)
        {
            for (int a = 1; a <= 3; a++)
            {
                rolls.Add(i + j + a);
                

            }
        }
    }

    return rolls.GroupBy(x => x).Select(x => (x.Key, x.Count())).ToList();
}
