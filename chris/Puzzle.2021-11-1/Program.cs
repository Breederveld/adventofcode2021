using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_11_1
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

            var octopuses = strings.Select(s => s.Select(c => c - '0').ToArray()).ToArray();
            var width = octopuses.Length;
            var height = octopuses[0].Length;
            var neighbours = new[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

            var flashed = new Queue<(int, int)>();
            var sum = 0;
            var inc = new Action<int, int>((x, y) =>
            {
                octopuses[x][y]++;
                if (octopuses[x][y] == 10)
                {
                    flashed.Enqueue((x, y));
                    sum++;
                }
            });

            for (var step = 0; step < 100; step++)
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        inc(x, y);
                    }
                }

                while (flashed.Count > 0)
                {
                    (var x, var y) = flashed.Dequeue();
                    foreach (var neighbour in neighbours)
                    {
                        var xx = x + neighbour.Item1;
                        var yy = y + neighbour.Item2;
                        if (xx < 0 || yy < 0 || xx >= width || yy >= height)
                        {
                            continue;
                        }
                        inc(xx, yy);
                    }
                }

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        if (octopuses[x][y] > 9)
                        {
                            octopuses[x][y] = 0;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}