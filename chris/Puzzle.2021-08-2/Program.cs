using CodeTech.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_08_2
{
    class Program
    {
        public static Dictionary<int, int[]> Numbers = new Dictionary<int, int[]>
        {
            [0] = "012456".Select((x, idx) => x - '0').ToArray(),
            [1] = "25".Select((x, idx) => x - '0').ToArray(),
            [2] = "02346".Select((x, idx) => x - '0').ToArray(),
            [3] = "02356".Select((x, idx) => x - '0').ToArray(),
            [4] = "1235".Select((x, idx) => x - '0').ToArray(),
            [5] = "01356".Select((x, idx) => x - '0').ToArray(),
            [6] = "013456".Select((x, idx) => x - '0').ToArray(),
            [7] = "025".Select((x, idx) => x - '0').ToArray(),
            [8] = "0123456".Select((x, idx) => x - '0').ToArray(),
            [9] = "012356".Select((x, idx) => x - '0').ToArray(),
        };
        public static Dictionary<int, int> NumbersBinary = Numbers.ToDictionary(kv => kv.Key, kv => ToBinary(kv.Value));
        public static Dictionary<int, int> NumbersLookup = NumbersBinary.ToDictionary(kv => kv.Value, kv => kv.Key);

        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();

            var signalOutputs = strings.Select(s => new SignalOutput(s)).ToArray();
            var sum = signalOutputs.Sum(so => so.GetValue());

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

            public int GetValue()
            {
                var sum = 0;
                foreach (var wires in Enumerable.Range(0, 7).AllPermutations())
                {
                    var binary = Wires
                        .Select(digit => ToBinary(digit.Select(i => wires[i])))
                        .ToArray();
                    if (binary.All(NumbersLookup.ContainsKey))
                    {
                         binary = Digits
                            .Select(digit => ToBinary(digit.Select(i => wires[i])))
                            .ToArray();
                        sum = Digits
                            .Select(digit => ToBinary(digit.Select(i => wires[i])))
                            .Aggregate(0, (acc, bin) => acc * 10 + NumbersLookup[bin]);
                        break;
                    }
                }

                return sum;
            }
        }

        private static int ToBinary(IEnumerable<int> value) => value.Sum(i => 1 << i);
    }
}