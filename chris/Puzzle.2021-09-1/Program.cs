using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_09_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => st.Select(c => c - '0').ToArray()).ToArray();
            var width = ints.Length;
            var height = ints[0].Length;

            var sum = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var val = ints[x][y];
                    if (x > 0 && ints[x - 1][y] <= val)
                    {
                        continue;
                    }
                    if (y > 0 && ints[x][y - 1] <= val)
                    {
                        continue;
                    }
                    if (x < width - 1 && ints[x + 1][y] <= val)
                    {
                        continue;
                    }
                    if (y < height - 1 && ints[x][y + 1] <= val)
                    {
                        continue;
                    }
                    sum += 1 + val;
                }
            }

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}