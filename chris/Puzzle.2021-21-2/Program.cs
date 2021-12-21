using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_21_2
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

            var wins1 = new double[21 * 21 * 10 * 10];
            for (var score0 = 20; score0 >= 0; score0--)
            {
                for (var score1 = 20; score1 >= 0; score1--)
                {
                    for (var pos0 = 0; pos0 < 10; pos0++)
                    {
                        for (var pos1 = 0; pos1 < 10; pos1++)
                        {
                            var wins = 0d;
                            for (int roll0 = 0; roll0 < 27; roll0++)
                            {
                                var pos00 = (pos0 + (roll0 / 9) + ((roll0 % 9) / 3) + roll0 % 3 + 3) % 10;
                                if (score0 + pos00 + 1 >= 21)
                                {
                                    wins++;
                                    continue;
                                }
                                for (int roll1 = 0; roll1 < 27; roll1++)
                                {
                                    var pos11 = (pos1 + (roll1 / 9) + ((roll1 % 9) / 3) + roll1 % 3 + 3) % 10;
                                    if (score1 + pos11 + 1 >= 21)
                                    {
                                        continue;
                                    }
                                    var nextIdx = (score0 + pos00 + 1) * 2000 + (score1 + pos11 + 1) * 100 + pos00 * 10 + pos11;
                                    wins += wins1[nextIdx];
                                }
                            }
                            var idx = score0 * 2000 + score1 * 100 + pos0 * 10 + pos1;
                            wins1[idx] = wins;
                        }
                    }
                }
            }

            Console.WriteLine(wins1[17].ToString());
            await Task.FromResult(0);
        }
    }
}