using CodeTech.Core.Mathematics;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_03_2
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

            var oxygen = strings;
            for (int i = 0; i < strings[0].Length; i++)
            {
                var j = oxygen.Select(s => s[i]).GroupBy(x => x).OrderByDescending(grp => grp.Count()).ThenBy(grp => grp.Key == '1' ? 0 : 1).First().Key;
                oxygen = oxygen.Where(o => o[i] == j).ToArray();
            }
            var co2 = strings;
            for (int i = 0; i < strings[0].Length; i++)
            {
                var j = co2.Select(s => s[i]).GroupBy(x => x).OrderBy(grp => grp.Count()).ThenByDescending(grp => grp.Key == '1' ? 0 : 1).First().Key;
                co2 = co2.Where(o => o[i] == j).ToArray();
            }

            var oxygenI = BinarySequences.GetIntFromBinary(oxygen[0].Select(i => i == '1'));
            var co2I = BinarySequences.GetIntFromBinary(co2[0].Select(i => i == '1'));
            Console.WriteLine(oxygenI * co2I);
            await Task.FromResult(0);
        }
    }
}