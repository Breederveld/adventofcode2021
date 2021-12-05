using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_05_2
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

            var lines = strings
                .Select(s =>
                {
                    var p = s.Split(" -> ");
                    var from = p[0].Split(',').Select(int.Parse).ToArray();
                    var to = p[1].Split(',').Select(int.Parse).ToArray();
                    return (from, to);
                })
                .ToArray();

            var size = lines.Max(l => Math.Max(Math.Max(l.from[0], l.from[1]), Math.Max(l.to[0], l.to[1]))) + 1;
            var grid = new int[size, size];
            foreach ((var from, var to) in lines)
            {
                var minX = Math.Min(from[0], to[0]);
                var maxX = Math.Max(from[0], to[0]);
                var minY = Math.Min(from[1], to[1]);
                var maxY = Math.Max(from[1], to[1]);
                if (from[0] == to[0])
                {
                    for (int i = minY; i <= maxY; i++)
                    {
                        grid[from[0], i]++;
                    }
                }
                if (from[1] == to[1])
                {
                    for (int i = minX; i <= maxX; i++)
                    {
                        grid[i, from[1]]++;
                    }
                }
                if (maxX - minX == maxY - minY)
                {
                    var dirX = Math.Sign(to[0] - from[0]);
                    var dirY = Math.Sign(to[1] - from[1]);
                    for (int i = 0; i <= maxX - minX; i++)
                    {
                        grid[from[0] + i * dirX, from[1] + i * dirY]++;
                    }
                }
            }
            var cnt = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (grid[x, y] > 1)
                    {
                        cnt++;
                    }
                }
            }

            Console.WriteLine(cnt);
            await Task.FromResult(0);
        }
    }
}