using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_01_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();
            var last = 0;
            var cnt = 0;
            for (int j = 2; j < ints.Length; j++)
            {
                var i = ints[j] + ints[j - 1] + ints[j - 2];
                if (i > last)
                {
                    cnt++;
                }
                last = i;
            }

            Console.WriteLine(cnt - 1);
            await Task.FromResult(0);
        }
    }
}