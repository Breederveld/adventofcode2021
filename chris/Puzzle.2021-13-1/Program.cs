using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_13_1
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

	    var width = 2000;
	    var height = 2000;
	    var grid = new bool[width,height];
	    foreach (var str in strings.TakeWhile(s => s.Length != 0))
	    {
	    	var points = str.Split(',').Select(s => int.Parse(s)).ToArray();
		grid[points[0], points[1]] = true;
	    }

	    var folds = strings.SkipWhile(s => s.Length != 0).Skip(1).Select(s => s.Substring(11).Split('=')).Select(p => (axis: p[0][0], pos: int.Parse(p[1]))).ToArray();
	    var fold = new Action<char, int>((axis, pos) =>
	    {
		switch (axis)
		{
			case 'x':
				for (var y = 0; y < height; y++)
				{
				for (var x = 0; x < width; x++)
				{
					if (x >= pos)
					{
						grid[x, y] = false;
					}
					else
					{
						var target = pos * 2 - x;
						grid[x, y] |= grid[target, y];
					}
				}
				}
			break;
			case 'y':
				var yy = Math.Min(pos, width - pos);
				for (var y = 0; y < width; y++)
				{
						var target = pos * 2 - y;
				for (var x = 0; x < height; x++)
				{
					if (y >= pos)
					{
						grid[x, y] = false;
					}
					else
					{
						grid[x, y] |= grid[x, target];
					}
				}
				}
			break;
		}
	    });
	    fold(folds[0].axis, folds[0].pos);

	    var sum = Enumerable.Range(0, width).Sum(x => Enumerable.Range(0, height).Count(y => grid[x, y]));
            Console.WriteLine(sum);
            await Task.FromResult(0);
        }
    }
}
