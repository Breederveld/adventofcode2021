using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_11_2
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

	    var parts = strings.Select(s => s.Split('-')).ToArray();
	    var paths = parts
		    .Concat(parts.Select(p => new[] { p[1], p[0] }))
		    .GroupBy(p => p[0], p => p[1])
		    .ToDictionary(g => g.Key, g => g.ToArray());

	    var visited = paths.ToDictionary(kv => kv.Key, _ => 0);
	    var next = new Stack<string>();
	    next.Push("start");
	    var sum = 0;
	    while (next.Count > 0)
	    {
		    var curr = next.Pop();
		    if (curr.StartsWith("-"))
		    {
			    visited[curr.Substring(1)]--;
			    continue;
		    }

	 	    if (curr[0] >= 'a' && curr[0] <= 'z')
		    {
		    	visited[curr]++; 
		    }
		    next.Push("-" + curr);
		    foreach (var option in paths[curr])
		    {
			    if (option == "start")
			    {
				    continue;
			    }
			    if (option == "end")
			    {
				    sum++;
				    continue;
			    }
			    if (option[0] >= 'a' && option[0] <= 'z' && (visited[option] > 0 && visited.Any(kv => kv.Value > 1)))
			    {
				    continue;
			    }
			    next.Push(option);
		    }
	    }

	    // 24820
            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}
