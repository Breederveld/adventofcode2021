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

            var grid = strings.Select(s => s.ToCharArray()).ToArray();
            var width = grid[0].Length;
            var height = grid.Length;

            var step = 1;
            var dir = '>';
            var hasMoved = false;
            while (true)
            {
                if (dir == '>')
                    hasMoved = false;
                var toMove = Enumerable.Range(0, width)
                    .SelectMany(x => Enumerable.Range(0, height)
                    .Select(y => (x, y))
                    .Where(t =>
                    {
                        var x = t.x;
                        var y = t.y;
                        var cucumber = grid[y][x];
                        if (cucumber != dir)
                            return false;
                        if (cucumber == '>' && x < width - 1 && grid[y][x + 1] != '.')
                            return false;
                        if (cucumber == 'v' && y < height - 1 && grid[y + 1][x] != '.')
                            return false;
                        if (cucumber == '>' && x == width - 1 && grid[y][0] != '.')
                            return false;
                        if (cucumber == 'v' && y == height - 1 && grid[0][x] != '.')
                            return false;
                        return true;
                    }))
                    .ToArray();
                if (toMove.Any())
                    hasMoved = true;
                if (dir == 'v' && !hasMoved)
                    break;
                foreach (var t in toMove)
                {
                    var x = t.x;
                    var y = t.y;
                    grid[y][x] = '.';
                    if (dir == '>')
                    {
                        if (x == width - 1)
                            grid[y][0] = '>';
                        else
                            grid[y][x + 1] = '>';
                    }
                    else
                    {
                        if (y == height - 1)
                            grid[0][x] = 'v';
                        else
                            grid[y + 1][x] = 'v';
                    }
                }

                dir = dir == '>' ? 'v' : '>';
                if (dir == '>')
                {
                    step++;
                }
            }

            Console.WriteLine(step);
            await Task.FromResult(0);
        }
    }
}