using CodeTech.Core.Mathematics;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_03_1
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

            var gamma = Enumerable.Range(0, strings[0].Length)
                .Select(i => strings.Select(s => s[i]).GroupBy(x => x).OrderByDescending(grp => grp.Count()).First().Key)
                .ToArray();
            var delta = Enumerable.Range(0, strings[0].Length)
                .Select(i => strings.Select(s => s[i]).GroupBy(x => x).OrderBy(grp => grp.Count()).First().Key)
                .ToArray();

            var gammaI = BinarySequences.GetIntFromBinary(gamma.Select(i => i == '1'));
            var deltaI = BinarySequences.GetIntFromBinary(delta.Select(i => i == '1'));
            Console.WriteLine(gammaI * deltaI);
            await Task.FromResult(0);
        }
    }
}