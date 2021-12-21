using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_21_1
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

            var pos0 = 2;
            var pos1 = 8;
            var score0 = 0d;
            var score1 = 0d;
            var rolls = 0;
            while (score0 < 1000 && score1 < 1000)
            {
                var roll = Throw();
                rolls += 3;

                pos0 = (pos0 + roll - 1) % 10 + 1;
                score0 += pos0;
                if (score0 >= 1000)
                {
                    break;
                }

                roll = Throw();
                rolls += 3;
                pos1 = (pos1 + roll) % 10;
                score1 += pos1;
            }

            var sum = score0 < 1000
                ? rolls * score0
                : rolls * score1;
            Console.WriteLine(sum);
            await Task.FromResult(0);
        }

        public static int _die = 1;
        public static int Throw()
        {
            var roll = 0;
            for (int i = 0; i < 3; i++)
            {
                roll += _die;
                _die = _die % 100 + 1;
            }
            return roll;
        }
    }
}