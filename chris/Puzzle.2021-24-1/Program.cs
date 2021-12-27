using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").Select(s => s.Trim()).ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();

            var first = Enumerable.Range(0, 14).Select(i => int.Parse(strings[i * 18 + 4].Split(' ').Last())).ToArray();
            var second = Enumerable.Range(0, 14).Select(i => int.Parse(strings[i * 18 + 5].Split(' ').Last())).ToArray();
            var third = Enumerable.Range(0, 14).Select(i => int.Parse(strings[i * 18 + 15].Split(' ').Last())).ToArray();

            var zOptions = new Stack<(int z, int[] inputs)>();
            zOptions.Push((z: 0, inputs: new int[0]));
            var inputOptions = Enumerable.Range(1, 9).ToArray();
            var max = 0d;
            while (zOptions.Count > 0)
            {
                (var z, var inputs) = zOptions.Pop();
                var depth = 13 - inputs.Length;
                if (depth == -1)
                {
                    if (z == 0)
                    {
                        var val = 0d;
                        foreach (var i in inputs.Reverse())
                        {
                            val = val * 10 + i;
                        }
                        max = val;
                        break;
                    }
                    continue;
                }

                var newOptions = new List<(int, int[])>();
                foreach (var inp in inputOptions)
                {
                    for (int i = first[depth] - 1; i >= 0; i--)
                    {
                        var z1 = z * first[depth] + i;
                        if (z1 % 26 + second[depth] == inp)
                        {
                            zOptions.Push((z1, inputs.Concat(new[] { inp }).ToArray()));
                        }
                    }

                    var z0 = z - third[depth] - inp;
                    if (z0 % 26 == 0)
                    {
                        z0 = z0 / 26 * first[depth];

                        for (int i = first[depth] - 1; i >= 0; i--)
                        {
                            if ((z0 + i) % 26 + second[depth] != inp)
                            {
                                zOptions.Push((z0 + i, inputs.Concat(new[] { inp }).ToArray()));
                            }
                        }
                    }
                }
            }

            Console.WriteLine(max.ToString());
            await Task.FromResult(0);
        }
    }
}