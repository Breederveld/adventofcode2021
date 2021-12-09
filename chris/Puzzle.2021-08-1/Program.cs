using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_08_1
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

            var signalOutputs = strings.Select(s => new SignalOutput(s)).ToArray();
            var sum = signalOutputs.Sum(so => so.Digits.Count(a => a.Length == 2 || a.Length == 3 || a.Length == 4 || a.Length == 7));

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }

        private class SignalOutput
        {
            public SignalOutput(string line)
            {
                var parts = line.Split('|');
                Wires = parts[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => s.Select(c => c - 'a').ToArray())
                    .ToArray();
                Digits = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => s.Select(c => c - 'a').ToArray())
                    .ToArray();
            }

            public int[][] Wires { get; set; }
            public int[][] Digits { get; set; }
        }
    }
}