using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_19_1
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

            var scanners = new List<List<(int x, int y, int z)>>();
            var scannerIdx = -1;
            foreach (var s in strings)
            {
                if (s.Length == 0)
                {
                    continue;
                }
                if (s.StartsWith("---"))
                {
                    scannerIdx++;
                    scanners.Add(new List<(int x, int y, int z)>());
                    continue;
                }
                var parts = s.Split(',').Select(s => int.Parse(s)).ToArray();
                scanners[scannerIdx].Add((parts[0], parts[1], parts[2]));
            }

            var known = new List<(int x, int y, int z)>();
            var relative = new Dictionary<int, List<(int x, int y, int z)>>();
            relative[0] = scanners[0];
            var check = new List<(int, int)>();
            while (relative.Count != scanners.Count)
            {
                for (int scanner0 = 0; scanner0 < scanners.Count; scanner0++)
                {
                    if (!relative.ContainsKey(scanner0))
                    {
                        continue;
                    }

                    for (int scanner1 = 0; scanner1 < scanners.Count; scanner1++)
                    {
                        if (relative.ContainsKey(scanner1))
                        {
                            continue;
                        }
                        if (check.Contains((scanner0, scanner1)))
                        {
                            continue;
                        }
                        check.Add((scanner0, scanner1));

                        if (scanner0 != scanner1)
                        {
                            var rel = FindRelative(relative[scanner0], scanners[scanner1]);
                            if (rel == null)
                            {
                                continue;
                            }

                            relative[scanner1] = rel;
                        }
                    }
                }
            }

            var all = relative.SelectMany(kv => kv.Value).Distinct().ToArray();
            Console.WriteLine(all.Length);
            await Task.FromResult(0);
        }

        private static List<(int x, int y, int z)> FindRelative(List<(int x, int y, int z)> scanner0, List<(int x, int y, int z)> scanner1)
        {
            var scannerRot = scanner1.ToList();
            for (int rotX = 0; rotX < 4; rotX++)
            {
                for (int rotY = 0; rotY < 4; rotY++)
                {
                    for (int rotZ = 0; rotZ < 4; rotZ++)
                    {
                        for (int pos0 = 0; pos0 < scanner0.Count; pos0++)
                        {
                            for (int pos1 = 0; pos1 < scanner1.Count; pos1++)
                            {
                                var relX = scanner0[pos0].x - scannerRot[pos1].x;
                                var relY = scanner0[pos0].y - scannerRot[pos1].y;
                                var relZ = scanner0[pos0].z - scannerRot[pos1].z;
                                var scannerMove = scannerRot.Select(t => (x: t.x + relX, y: t.y + relY, z: t.z + relZ)).ToList();
                                var cnt = scannerMove.Count(scanner => scanner0.Any(t => scanner.x == t.x && scanner.y == t.y && scanner.z == t.z));
                                if (cnt >= 12)
                                {
                                    return scannerMove;
                                }
                            }
                        }

                        scannerRot = scannerRot.Select(t => (-t.y, t.x, t.z)).ToList();
                    }
                    scannerRot = scannerRot.Select(t => (t.z, t.y, -t.x)).ToList();
                }
                scannerRot = scannerRot.Select(t => (t.x, t.z, -t.y)).ToList();
            }
            return null;
        }
    }
}