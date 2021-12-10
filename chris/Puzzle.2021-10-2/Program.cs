using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_10_2
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

            var scores = new List<double>();
            foreach (var str in strings)
            {
                var state = new Stack<char>();
                var halt = false;
                foreach (var chr in str)
                {
                    halt = false;
                    switch (chr)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            state.Push(chr);
                            break;

                        case ')':
                            if (state.Count != 0 && state.Pop() != '(')
                            {
                                halt = true;
                            }
                            break;
                        case ']':
                            if (state.Count != 0 && state.Pop() != '[')
                            {
                                halt = true;
                            }
                            break;
                        case '}':
                            if (state.Count != 0 && state.Pop() != '{')
                            {
                                halt = true;
                            }
                            break;
                        case '>':
                            if (state.Count != 0 && state.Pop() != '<')
                            {
                                halt = true;
                            }
                            break;
                    }
                    if (halt)
                    {
                        break;
                    }
                }
                if (halt)
                {
                    continue;
                }
                var sum = 0d;
                while (state.Count > 0)
                {
                    sum *= 5;
                    switch (state.Pop())
                    {
                        case '(':
                            sum += 1;
                            break;
                        case '[':
                            sum += 2;
                            break;
                        case '{':
                            sum += 3;
                            break;
                        case '<':
                            sum += 4;
                            break;
                    }
                }
                scores.Add(sum);
            }
            var score = scores.OrderBy(i => i).Skip(scores.Count / 2).First();

            Console.WriteLine(score);
            await Task.FromResult(0);
        }
    }
}