using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_10_1
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

            var sum = 0;
            foreach (var str in strings)
            {
                var state = new Stack<char>();
                foreach (var chr in str)
                {
                    var halt = false;
                    switch (chr)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            state.Push(chr);
                            break;

                        case ')':
                            if (state.Count == 0 || state.Pop() != '(')
                            {
                                sum += 3;
                                halt = true;
                            }
                            break;
                        case ']':
                            if (state.Count == 0 || state.Pop() != '[')
                            {
                                sum += 57;
                                halt = true;
                            }
                            break;
                        case '}':
                            if (state.Count == 0 || state.Pop() != '{')
                            {
                                sum += 1197;
                                halt = true;
                            }
                            break;
                        case '>':
                            if (state.Count == 0 || state.Pop() != '<')
                            {
                                sum += 25137;
                                halt = true;
                            }
                            break;
                    }
                    if (halt)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}