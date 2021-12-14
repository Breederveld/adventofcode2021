using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_14_2
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

            var list = strings[0].Trim();
            var rules = strings.Skip(2).Select(s => s.Split(" -> "))
                .ToDictionary(p => p[0], p => new[] { new string(new[] { p[0][0], p[1][0] }), new string(new[] { p[1][0], p[0][1] }) });

            var letters = rules.Keys.SelectMany(k => k.ToArray()).Distinct().ToArray();
            var combinations = Enumerable.Range(0, letters.Length)
                .SelectMany(i => Enumerable.Range(0, letters.Length).Select(j => new string(new[] { letters[i], letters[j] })))
                .ToArray();
            var dict = combinations
                .ToDictionary(key => key, key => (double)list.FindIndexes(c => c == key[0]).Where(idx => idx != list.Length - 1 && list[idx + 1] == key[1]).Count());
            for (int i = 0; i < 40; i++)
            {
                var updates = combinations.ToDictionary(key => key, _ => 0d);
                foreach (var rule in rules)
                {
                    if (dict.ContainsKey(rule.Key))
                    {
                        var amount = dict[rule.Key];
                        updates[rule.Value[0]] += amount;
                        updates[rule.Value[1]] += amount;
                        updates[rule.Key] -= amount;
                    }
                }
                foreach (var kv in updates)
                {
                    dict[kv.Key] += kv.Value;
                }
                Console.WriteLine(i);
            }
            var sums = dict
                .SelectMany(kv => kv.Key.Select(k => (k, kv.Value)))
                .GroupBy(t => t.k)
                .ToDictionary(grp => grp.Key, grp => grp.Sum(t => t.Value));
            sums[list[0]]++;
            sums[list.Last()]++;
            var sum = (sums.Max(kv => kv.Value) - sums.Min(kv => kv.Value)) / 2;

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}