using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_02_2
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

            var x = 0;
            var d = 0;
            var aim = 0;
            foreach (var s in strings)
            {
                var parts = s.Split(' ');
                var cnt = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        x += cnt;
                        d += cnt * aim;
                        break;
                    case "up":
                        aim -= cnt;
                        break;
                    case "down":
                        aim += cnt;
                        break;
                }
            }

            Console.WriteLine(x * d);
            await Task.FromResult(0);
        }
    }
}