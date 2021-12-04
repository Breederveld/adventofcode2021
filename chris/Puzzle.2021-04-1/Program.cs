using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_04_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();

            var numbers = strings[0].Split(',').Select(s => int.Parse(s)).ToArray();

            var boards = new List<Board>();
            var y = 0;
            var newBoard = new Board();
            foreach (var s in strings.Skip(2))
            {
                if (s == String.Empty)
                {
                    boards.Add(newBoard);
                    newBoard = new Board();
                    y = 0;
                }
                else
                {
                    var nrs = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
                    for (var x = 0; x < 5; x++)
                    {
                        newBoard.Numbers[x, y] = int.Parse(nrs[x]);
                    }
                    y++;
                }
            }
            boards.Add(newBoard);

            var score = 0;
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.Mark(number))
                    {
                        score = board.Score() * number;
                        break;
                    }
                }
                if (score != 0)
                {
                    break;
                }
            }

            Console.WriteLine(score);
            await Task.FromResult(0);
        }

        private class Board
        {
            public int[,] Numbers { get; set; } = new int[5, 5];
            public bool[,] Marked { get; set; } = new bool[5, 5];

            public bool Mark(int number)
            {
                for (var x = 0; x < 5; x++)
                {
                    for (var y = 0; y < 5; y++)
                    {
                        if (Numbers[x, y] == number)
                        {
                            Marked[x, y] = true;
                            if (Enumerable.Range(0, 5).All(i => Marked[x, i]))
                            {
                                return true;
                            }
                            if (Enumerable.Range(0, 5).All(i => Marked[i, y]))
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
                return false;
            }

            public int Score()
            {
                var sum = 0;
                for (var x = 0; x < 5; x++)
                {
                    for (var y = 0; y < 5; y++)
                    {
                        if (!Marked[x, y])
                        {
                            sum += Numbers[x, y];
                        }
                    }
                }
                return sum;
            }
        }
    }
}