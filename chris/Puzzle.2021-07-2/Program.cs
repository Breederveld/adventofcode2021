using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_07_2
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

            var positions = strings[0].Split(',').Select(int.Parse).ToArray();
            var minSum = int.MaxValue;
            for (var target = 0; target < 1000; target++)
            {
                var sum = positions.Select(pos =>
                {
                    var steps = Math.Abs(pos - target);
                    return (int)((steps + 1) * steps / 2.0);
                }).Sum();
                if (sum < minSum)
                {
                    minSum = sum;
                }
            }

            Console.WriteLine(minSum);
            await Task.FromResult(0);
        }
    }
}