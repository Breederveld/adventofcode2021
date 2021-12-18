using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle
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

            var max = 0d;
            for (int i = 0; i < strings.Length; i++)
            {
                for (int j = 0; j < strings.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var snail0 = ReadSnail(strings[i]);
                    var snail1 = ReadSnail(strings[j]);
                    snail0.Simplify();
                    snail1.Simplify();
                    var snail2 = snail0.Add(snail1);
                    snail2.Simplify();
                    var mag = snail2.Magnitude;
                    if (mag > max)
                    {
                        max = mag;
                    }
                }
            }

            Console.WriteLine(max.ToString());
            await Task.FromResult(0);
        }

        private static Snail ReadSnail(string number)
        {
            (var snail, var pos) = ReadSnail(number, 0, 0);
            return snail;
        }

        private static (Snail, int) ReadSnail(string number, int start, int depth)
        {
            if (number[start] == '[')
            {
                (var left, var posl) = ReadSnail(number, start + 1, depth + 1);
                (var right, var posr) = ReadSnail(number, posl + 1, depth + 1);
                var snail = new Snail { Left = left, Right = right, Depth = depth };
                left.Parent = snail;
                right.Parent = snail;
                return (snail, posr + 1);
            }
            if (number[start] >= '0' || number[start] <= '9')
            {
                var num = 0d;
                var pos = start;
                while (number[pos] >= '0' && number[pos] <= '9')
                {
                    num = num * 10 + (number[pos] - '0');
                    pos++;
                }
                return (new Snail { Number = num, Depth = depth }, pos);
            }
            return (null, 0);
        }

        public class Snail : IEnumerable<Snail>
        {
            public double? Number { get; set; }
            public Snail Left { get; set; }
            public Snail Right { get; set; }
            public Snail Parent { get; set; }
            public int Depth { get; set; }

            public void Simplify()
            {
                while (true)
                {
                    var toExplode = this.FirstOrDefault(s => s.Depth == 4 && s.IsPair);
                    if (toExplode != null)
                    {
                        toExplode.Explode();
                        continue;
                    }
                    var toSplit = this.FirstOrDefault(s => s.Number > 9);
                    if (toSplit != null)
                    {
                        toSplit.Split();
                        continue;
                    }
                    break;
                }
            }
            public void Explode()
            {
                var previous = Top.Where(x => x.Number.HasValue).TakeWhile(x => x != this.Left).LastOrDefault();
                if (previous != null)
                {
                    previous.Number += Left.Number;
                }
                var next = Top.Where(x => x.Number.HasValue).SkipWhile(x => x != this.Right).Skip(1).FirstOrDefault();
                if (next != null)
                {
                    next.Number += Right.Number;
                }
                Left = null;
                Right = null;
                Number = 0;
            }

            public void Split()
            {
                Left = new Snail { Number = (double)Math.Floor(Number.Value / 2), Depth = Depth + 1, Parent = this };
                Right = new Snail { Number = (double)Math.Ceiling(Number.Value / 2), Depth = Depth + 1, Parent = this };
                Number = null;
            }

            public bool IsPair => !Number.HasValue && Left.Number.HasValue && Right.Number.HasValue;
            public double Magnitude => Number.HasValue ? Number.Value : (Left.Magnitude * 3 + Right.Magnitude * 2);
            public Snail Top => Parent == null ? this : Parent.Top;

            public Snail Add(Snail snail)
            {
                var depth = Depth;
                foreach (var s in this)
                {
                    s.Depth++;
                }
                foreach (var s in snail)
                {
                    s.Depth++;
                }
                var parent = new Snail
                {
                    Left = this,
                    Right = snail,
                    Depth = depth,
                };
                this.Parent = parent;
                snail.Parent = parent;
                return parent;
            }

            public override string ToString()
            {
                if (Number.HasValue)
                {
                    return Number.ToString();
                }
                return $"[{Left},{Right}]";
            }

            public IEnumerator<Snail> GetEnumerator()
            {
                yield return this;
                if (Number.HasValue)
                {
                    yield break;
                }
                foreach (var snail in Left)
                {
                    yield return snail;
                }
                foreach (var snail in Right)
                {
                    yield return snail;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}