using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_14_1
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

            var list = new LinkedList<char>(strings[0].Trim().ToArray());
            var rules = strings.Skip(2).Select(s => s.Split(" -> ")).ToDictionary(p => p[0], p => p[1][0]);
            var first = list.Find(list.First());

            for (int i = 0; i < 10; i++)
            {
                var inserts = new List<(LinkedListNode<char> item, char insert)>();
                var item = first;
                while (item.Next != null)
                {
                    var key = new string(new[] { item.Value, item.Next.Value });
                    if (rules.ContainsKey(key))
                    {
                        inserts.Add((item, insert: rules[key]));
                    }
                    item = item.Next;
                }
                foreach (var insert in inserts)
                {
                    list.AddAfter(insert.item, insert.insert);
                }
            }
            var result = new string(list.ToArray());
            var sums = result.GroupBy(chr => chr)
                .ToDictionary(grp => grp.Key, grp => grp.Count());
            var sum = sums.Max(kv => kv.Value) - sums.Min(kv => kv.Value);

            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}