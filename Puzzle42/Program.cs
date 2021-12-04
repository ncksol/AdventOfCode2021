var numbers = new int[] { 18, 99, 39, 89, 0, 40, 52, 72, 61, 77, 69, 51, 30, 83, 20, 65, 93, 88, 29, 22, 14, 82, 53, 41, 76, 79, 46, 78, 56, 57, 24, 36, 38, 11, 50, 1, 19, 26, 70, 4, 54, 3, 84, 33, 15, 21, 9, 58, 64, 85, 10, 66, 17, 43, 31, 27, 2, 5, 95, 96, 16, 97, 12, 34, 74, 67, 86, 23, 49, 8, 59, 45, 68, 91, 25, 48, 13, 28, 81, 94, 92, 42, 7, 37, 75, 32, 6, 60, 63, 35, 62, 98, 90, 47, 87, 73, 44, 71, 55, 80 };
var boards = new List<int[,]>();

var file = new FileInfo("TextFile1.txt");
using (var textReader = new StreamReader(file.OpenRead()))
{
    var data = textReader.ReadToEnd();
    var lines = data.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    var boardRow = 0;
    var values = new int[5, 5];
    for (int i = 0; i < lines.Length; i++)
    {
        var boardNumbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (boardRow == 0)
        {
            values = new int[5, 5];
        }

        for (int j = 0; j < boardNumbers.Length; j++)
        {
            values[boardRow, j] = Int32.Parse(boardNumbers[j]);
        }

        if (boardRow == 4)
        {
            boards.Add(values);
            boardRow = 0;
        }
        else
            boardRow++;
    }
}


var checkBoards = new List<int[,]>();
for (int i = 0; i < boards.Count; i++)
{
    checkBoards.Add(new int[5, 5]);
}

var winningIndex = -1;
var winningNumber = -1;
foreach (var number in numbers)
{
    for (int i = 0; i < boards.Count; i++)
    {
        var (x, y) = FindNumber(boards[i], number);
        if (x >= 0 && y >= 0)
        {
            checkBoards[i][x, y] = 1;
        }

        var win = FindWinningBoard(checkBoards[i]);
        if (win)
        {
            if(boards.Count == 1)
            { 
                winningIndex = i;
                winningNumber = number;
                break;
            }
            else
            {
                boards.RemoveAt(i);
                checkBoards.RemoveAt(i);
                i=-1;
            }
        }
    }

    if(winningIndex != -1)
        break;
}

var sum = 0;

var winningBoard = boards[winningIndex];
var checkBoard = checkBoards[winningIndex];

for (int i = 0; i < winningBoard.GetLength(0); i++)
{
    for (int j = 0; j < winningBoard.GetLength(1); j++)
    {
        if (checkBoard[i, j] == 0)
            sum += winningBoard[i, j];
    }
}

Console.WriteLine(sum * winningNumber);


(int x, int y) FindNumber(int[,] board, int number)
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (board[i, j] == number)
            {
                return (i, j);
            }
        }
    }

    return (-1, -1);
}


bool FindWinningBoard(int[,] board)
{
    var i = 0;
    var j = 0;

    while (j < 5)
    {
        if (board[i, j] == 0)
        {
            j++;
            continue;
        }

        var winner = true;
        while (i < 5)
        {
            if (board[i, j] == 0)
            {
                i = 0;
                j++;
                winner = false;
                break;
            }

            i++;
        }

        if (winner)
            return true;
    }


    i = 0;
    j = 0;

    while (i < 5)
    {
        if (board[i, j] == 0)
        {
            i++;
            continue;
        }

        var winner = true;
        while (j < 5)
        {
            if (board[i, j] == 0)
            {
                j = 0;
                i++;
                winner = false;
                break;
            }

            j++;
        }

        if (winner)
            return true;
    }

    return false;
}