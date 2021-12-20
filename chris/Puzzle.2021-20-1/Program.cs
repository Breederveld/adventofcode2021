using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").Select(s => s.Trim()).ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();

            var width = strings[2].Length;
            var height = strings.Length - 2;
            var rounds = 2;
            var around = rounds + 4;
            var enhanced = strings[0].Select(c => c == '#' ? 1 : 0).ToArray();
            var grid = strings.Skip(2).SelectMany(s => (new string('.', around) + s + new string('.', around)).Select(c => c == '#' ? 1 : 0)).ToArray();

            width += around * 2;
            height += around * 2;
            grid = new int[(width * around)].Concat(grid).Concat(new int[width * around]).ToArray();
            var sum0 = grid.Count(i => i == 1);

            var enhance = new Func<int, int>(curr =>
            {
                var pos = (grid[curr - width - 1] << 8) + (grid[curr - width] << 7) + (grid[curr - width + 1] << 6) + (grid[curr - 1] << 5)
                    + (grid[curr] << 4) + (grid[curr + 1] << 3) + (grid[curr + width - 1] << 2) + (grid[curr + width] << 1) + (grid[curr + width + 1]);
                return enhanced[pos];
            });
            for (var round = 0; round < rounds; round++)
            {
                var offset = around - round - 1;
                offset = 1;
                grid = Enumerable.Range(0, height).SelectMany(y => Enumerable.Range(0, width)
                    .Select(x =>
                    {
                        if (x < offset || y < offset || x >= width - offset || y >= height - offset)
                        {
                            return (round + 1) % 2;
                        }
                        var curr = y * width + x;
                        return enhance(curr);
                    }))
                    .ToArray();
                Console.WriteLine();
                for (var y = 0; y < height; y++)
                {
                    Console.WriteLine(new string(grid.Skip(y * width).Take(width).Select(i => i == 1 ? '#' : '.').ToArray()));
                }
            }

            var sum = grid.Count(i => i == 1);
            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}