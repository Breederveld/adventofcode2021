using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_06_1
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

            var fish = strings[0].Split(',').Select(int.Parse).ToArray();

            for (var i = 0; i < 80; i++)
            {
                var newFish = fish.Count(f => f == 0);
                fish = fish
                    .Select(f => f == 0 ? 6 : f - 1)
                    .Concat(Enumerable.Range(0, newFish).Select(_ => 8))
                    .ToArray();
            }

            Console.WriteLine(fish.Length);
            await Task.FromResult(0);
        }
    }
}