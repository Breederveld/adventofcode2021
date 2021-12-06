using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_06_2
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

            var table = new double[256];
            var tableFish = Enumerable.Range(0, 9).ToDictionary(i => i, i => i == 0 ? (double)1 : 0);
            for (var i = 0; i < 256; i++)
            {
                table[i] = tableFish.Values.Sum();
                var newFish = tableFish.ContainsKey(0) ? tableFish[0] : 0;
                tableFish = Enumerable.Range(0, 8).ToDictionary(i => i, i => i == 6 ? tableFish[0] + tableFish[7] : tableFish[i + 1]);
                tableFish[8] = newFish;
            }

            var total = fish.Sum(f => table[256 - f]);

            Console.WriteLine(total);
            await Task.FromResult(0);
        }
    }
}