using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_15_1
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
            var grid = strings.Select(s => s.Trim().Select(c => c - '0').ToArray()).ToArray();
            var width = grid.Length;
            var height = grid[0].Length;

            var risks = new int[width, height];

            var next = new List<(int x, int y, int risk)>();
            next.Add((0, 0, 0));

            while (next.Count > 0)
            {
                var p = next.OrderBy(t => t.risk).First();
                next.Remove(p);
                if (risks[p.x, p.y] != 0)
                {
                    continue;
                }
                risks[p.x, p.y] = p.risk;
                if (p.x > 0)
                    next.Add((p.x - 1, p.y, p.risk + grid[p.x - 1][p.y]));
                if (p.x < width - 1)
                    next.Add((p.x + 1, p.y, p.risk + grid[p.x + 1][p.y]));
                if (p.y > 0)
                    next.Add((p.x, p.y - 1, p.risk + grid[p.x][p.y - 1]));
                if (p.y < height - 1)
                    next.Add((p.x, p.y + 1, p.risk + grid[p.x][p.y + 1]));
                if (p.x == width - 1 && p.y == height - 1)
                {
                    break;
                }    
            }

            Console.WriteLine(risks[width - 1, height - 1]);
            await Task.FromResult(0);
        }
    }
}