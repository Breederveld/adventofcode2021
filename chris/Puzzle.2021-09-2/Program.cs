using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_09_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => st.Trim().Select(c => c - '0').ToArray()).ToArray();
            var width = ints.Length;
            var height = ints[0].Length;

            var basins = new List<int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var val = ints[x][y];
                    if (x > 0 && ints[x - 1][y] <= val)
                    {
                        continue;
                    }
                    if (y > 0 && ints[x][y - 1] <= val)
                    {
                        continue;
                    }
                    if (x < width - 1 && ints[x + 1][y] <= val)
                    {
                        continue;
                    }
                    if (y < height - 1 && ints[x][y + 1] <= val)
                    {
                        continue;
                    }
                    basins.Add(FindBasin(ints, x, y));
                }
            }
            var sum = basins.OrderByDescending(x => x).Take(3).Aggregate(1, (acc, val) => acc * val);

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }

        public static int FindBasin(int[][] ints, int xx, int yy)
        {
            var width = ints.Length;
            var height = ints[0].Length;
            ints = ints.Select(a => a.ToArray()).ToArray();
            ints[xx][yy] = 10;
            var cnt = 1;
            var lastCnt = 0;
            while (cnt != lastCnt)
            {
                lastCnt = cnt;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (ints[x][y] == 10 || ints[x][y] == 9)
                        {
                            continue;
                        }
                        if (x > 0 && ints[x - 1][y] == 10)
                        {
                            ints[x][y] = 10;
                            cnt++;
                            continue;
                        }
                        if (y > 0 && ints[x][y - 1] == 10)
                        {
                            ints[x][y] = 10;
                            cnt++;
                            continue;
                        }
                        if (x < width - 1 && ints[x + 1][y] == 10)
                        {
                            ints[x][y] = 10;
                            cnt++;
                            continue;
                        }
                        if (y < height - 1 && ints[x][y + 1] == 10)
                        {
                            ints[x][y] = 10;
                            cnt++;
                            continue;
                        }
                    }
                }
            }
            return cnt;
        }
    }
}