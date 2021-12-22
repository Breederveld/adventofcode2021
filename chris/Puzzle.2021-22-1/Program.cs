using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Puzzle_2021_22_1
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

            var instructions = strings
                .Select(s =>
                {
                    var match = Regex.Match(s, @"(?<state>\w+) x=(?<x0>[\-0-9]+)..(?<x1>[\-0-9]+),y=(?<y0>[\-0-9]+)..(?<y1>[\-0-9]+),z=(?<z0>[\-0-9]+)..(?<z1>[\-0-9]+)");
                    return (
                        x0: int.Parse(match.Groups["x0"].Value),
                        x1: int.Parse(match.Groups["x1"].Value),
                        y0: int.Parse(match.Groups["y0"].Value),
                        y1: int.Parse(match.Groups["y1"].Value),
                        z0: int.Parse(match.Groups["z0"].Value),
                        z1: int.Parse(match.Groups["z1"].Value),
                        state: match.Groups["state"].Value == "on"
                        );
                })
                .ToArray();

            var cubes = new List<double>();
            var getPos = new Func<int, int, int, double>((x, y, z) => x * 100000000d + y * 10000d + z);

            foreach (var instruction in instructions)
            {
                if (instruction.state)
                {
                    var add = new List<double>();
                    for (int x = instruction.x0; x <= instruction.x1; x++)
                    {
                        if (x < -50 || x > 50)
                            continue;
                        for (int y = instruction.y0; y <= instruction.y1; y++)
                        {
                            if (y < -50 || y > 50)
                                continue;
                            for (int z = instruction.z0; z <= instruction.z1; z++)
                            {
                                if (z < -50 || z > 50)
                                    continue;
                                add.Add(getPos(x, y, z));
                            }
                        }
                    }
                    cubes.AddRange(add);
                    cubes = cubes.Distinct().ToList();
                }
                else
                {
                    var remove = new List<double>();
                    for (int x = instruction.x0; x <= instruction.x1; x++)
                    {
                        if (x < -50 || x > 50)
                            continue;
                        for (int y = instruction.y0; y <= instruction.y1; y++)
                        {
                            if (y < -50 || y > 50)
                                continue;
                            for (int z = instruction.z0; z <= instruction.z1; z++)
                            {
                                if (z < -50 || z > 50)
                                    continue;
                                cubes.Remove(getPos(x, y, z));
                            }
                        }
                    }
                }
            }

            var sum = cubes.Distinct().Count();
            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}
