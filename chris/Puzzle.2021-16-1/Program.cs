using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeTech.Core.Mathematics;

namespace Puzzle_2021_16_1
{
    class Program
    {
        private static int pos = 0;
        private static bool[] binary;

        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();
            binary = strings[0].SelectMany(c => BinarySequences.GetBinaryForInt(int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber), 4)).ToArray();

            var sum = ReadPacket();

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
        public static double ReadPacket()
        {
            var version = BinarySequences.GetIntFromBinary(binary.Skip(pos).Take(3));
            pos += 3;
            var sum = version;
            var type = BinarySequences.GetIntFromBinary(binary.Skip(pos).Take(3));
            pos += 3;
            switch (type)
            {
                case 4:
                    var literal = new List<bool>();
                    var last = false;
                    while (!last)
                    {
                        last = !binary[pos];
                        literal.AddRange(binary.Skip(pos + 1).Take(4));
                        pos += 5;
                    }
                    break;
                default:
                    if (binary[pos] == false)
                    {
                        pos += 1;
                        var length = BinarySequences.GetIntFromBinary(binary.Skip(pos).Take(15));
                        pos += 15;
                        var stop = pos + length;
                        while (pos < stop)
                        {
                            sum += ReadPacket();
                        }
                    }
                    else
                    {
                        pos += 1;
                        var packets = BinarySequences.GetIntFromBinary(binary.Skip(pos).Take(11));
                        pos += 11;
                        for (int i = 0; i < packets; i++)
                        {
                            sum += ReadPacket();
                        }
                    }
                    break;
            }

            return sum;
        }
    }
}