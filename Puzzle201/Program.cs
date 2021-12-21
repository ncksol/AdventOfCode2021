var algo = "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#";

var image = @"
#..#.
#....
##..#
..#..
..###";

var lines = image.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
var inputArray = new int[lines.Length][];

for (int i = 0; i < lines.Length; i++)
{
    inputArray[i] = lines[i].Select(x => x == '#' ? 1 : 0).ToArray();
}

var outputArray = new int[inputArray.Length+2][];
for (int i = 0; i < outputArray.Length; i++)
{
    outputArray[i] = new int[inputArray[i].Length];
    for (int j = 0; j < outputArray[i].Length; j++)
    {

    }
}