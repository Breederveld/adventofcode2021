using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Puzzle_2021_22_2
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
                        cube: new Cube(
                            int.Parse(match.Groups["x0"].Value),
                            int.Parse(match.Groups["x1"].Value),
                            int.Parse(match.Groups["y0"].Value),
                            int.Parse(match.Groups["y1"].Value),
                            int.Parse(match.Groups["z0"].Value),
                            int.Parse(match.Groups["z1"].Value)),
                        state: match.Groups["state"].Value == "on"
                    );
                })
                .ToArray();

            for (int i = instructions.Length - 1; i >= 0; i--)
            {
                instructions[i].cube.Apply(instructions.Skip(i + 1));
            }
            var sum = instructions.Where(i => i.state).Sum(i => i.cube.Count());

            Console.WriteLine(sum.ToString());
            await Task.FromResult(0);
        }

        public class Cube
        {
            public Cube(int x0, int x1, int y0, int y1, int z0, int z1)
            {
                X0 = x0;
                X1 = x1;
                Y0 = y0;
                Y1 = y1;
                Z0 = z0;
                Z1 = z1;
            }
            public int X0 { get; set; }
            public int X1 { get; set; }
            public int Y0 { get; set; }
            public int Y1 { get; set; }
            public int Z0 { get; set; }
            public int Z1 { get; set; }

            public Cube GetOverlap(Cube other)
            {
                var x0 = Math.Max(X0, other.X0);
                var x1 = Math.Min(X1, other.X1);
                var y0 = Math.Max(Y0, other.Y0);
                var y1 = Math.Min(Y1, other.Y1);
                var z0 = Math.Max(Z0, other.Z0);
                var z1 = Math.Min(Z1, other.Z1);
                if (x1 < x0 || y1 < y0 || z1 < z0)
                {
                    return null;
                }
                var cube = new Cube(x0, x1, y0, y1, z0, z1);
                cube.Apply(_instructions);
                return cube;
            }

            private (Cube cube, bool state)[] _instructions = new (Cube, bool)[0];
            public void Apply(IEnumerable<(Cube cube, bool state)> instructions)
            {
                // Constrain.
                _instructions = instructions
                    .Select(i => (cube: i.cube.GetOverlap(this), i.state))
                    .Where(i => i.cube != null)
                    .ToArray();
                // Simplify.
                _instructions = _instructions
                    .Select((i, idx) => (i, idx))
                    .Where(t => _instructions.Skip(t.idx + 1).All(ii => ii.cube.GetOverlap(t.i.cube)?.Count() != t.i.cube.Count()))
                    .Select(t => t.i)
                    .ToArray();
            }

            public double Count()
            {
                var count = 1d * (X1 - X0 + 1) * (Y1 - Y0 + 1) * (Z1 - Z0 + 1);
                foreach (var instruction in _instructions)
                {
                    count -= instruction.cube.Count();
                }
                return count;
            }
        }
    }
}