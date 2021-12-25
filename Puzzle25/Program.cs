var str = @"
v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>";

str = File.ReadAllText("input.txt");

var array = str.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);
var input = new Dictionary<(int X, int Y), char>();
for (int i = 0; i < array.Length; i++)
{
    for (int j = 0; j < array[i].Length; j++)
    {
        input.Add((j, i), array[i][j]);
    }
}

int maxX = array[0].Length;
int maxY = array.Length;

var step = 0;

while(true)
{
    step++;

    var eastMovers = input.Where(p => p.Value == '>' && CanMoveEast(p.Key)).ToList();
    MoveEast(eastMovers);

    var southMovers = input.Where(p => p.Value == 'v' && CanMoveSouth(p.Key)).ToList();    
    MoveSouth(southMovers);

    if(eastMovers.Count == 0 && southMovers.Count == 0)
        break;
}

Console.WriteLine($"{step}");

bool CanMoveEast((int X, int Y) key)
{
    var eastCellCoord = key.X + 1 == maxX ? 0 : key.X + 1;
    var eastCell = input[(eastCellCoord, key.Y)];

    return eastCell == '.';
}

bool CanMoveSouth((int X, int Y) key)
{
    var nextCellCoord = key.Y + 1 == maxY ? 0 : key.Y + 1;
    var nextCell = input[(key.X, nextCellCoord)];

    return nextCell == '.';
}

void MoveEast(List<KeyValuePair<(int X, int Y), char>> movers)
{
    foreach (var mover in movers)
    {
        input[mover.Key] = '.';

        var nextCellCoord = (mover.Key.X + 1 == maxX ? 0 : mover.Key.X + 1, mover.Key.Y);
        input[nextCellCoord] = mover.Value;
    }
}

void MoveSouth(List<KeyValuePair<(int X, int Y), char>> movers)
{
    foreach (var mover in movers)
    {
        input[mover.Key] = '.';

        var nextCellCoord = (mover.Key.X, mover.Key.Y + 1 == maxY ? 0 : mover.Key.Y + 1);
        input[nextCellCoord] = mover.Value;
    }
}