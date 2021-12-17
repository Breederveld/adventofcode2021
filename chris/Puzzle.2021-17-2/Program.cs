using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_17_2
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

            int x1 = 185, x2 = 221, y1 = -74, y2 = -122;

            var sum = 0;
            for (int xx = 0; xx < 1000; xx++)
            {
                for (int yy = -1000; yy < 1000; yy++)
                {
                    int vx = xx, vy = yy;
                    int x = 0, y = 0;
                    int maxy = 0;
                    while (x <= x2 && y >= y2)
                    {
                        if (x >= x1 && y <= y1)
                        {
                            sum++;
                            break;
                        }

                        x += vx;
                        y += vy;
                        if (vx > 0) vx--;
                        vy--;
                        if (y > maxy) maxy = y;
                    }
                }
            }

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}