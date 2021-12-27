using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2021_23_1
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

            //var initialState = new GameState(0, 21, 81, 20, 60, 40, 61, 41, 80);
            var initialState = new GameState(0, 20, 61, 60, 80, 40, 81, 21, 41);
            var states = new Stack<GameState[]>();
            states.Push(new[] { initialState });
            GameState state = null;
            var bestWin = double.MaxValue;
            while (states.Count > 0)
            {
                var next = states.Pop();
                state = next[0];
                if (state.Distance + state.BestToGo() >= bestWin)
                {
                    continue;
                }
                if (state.IsDone())
                {
                    if (state.Distance < bestWin)
                    {
                        bestWin = state.Distance;
                    }
                }
                foreach (var s in state.Next())
                {
                    states.Push(new[] { s }.Concat(next).ToArray());
                }
            }

            Console.WriteLine(bestWin);
            await Task.FromResult(0);
        }

    }

    // Positions: 0-10, 20+21, 40+41, 60+61, 80+81
    public class GameState
    {
        public GameState(double distance, params int[] positions)
        {
            Distance = distance;
            Positions = positions;
            LastMoved = -1;
        }

        public double Distance { get; }
        public int[] Positions { get; set; }
        public int LastMoved { get; set; }

        public IEnumerable<GameState> Next()
        {
            for (var idx = 0; idx < Positions.Length; idx++)
            {
                var pos = Positions[idx];
                var power = GetPower(idx);
                foreach (var p in NextPositions(pos, idx))
                {
                    yield return new GameState(Distance + power * p.distance, Positions.Select((position, i) => i == idx ? p.position : position).ToArray())
                    {
                        LastMoved = idx,
                    };
                }
            }
        }

        public double BestToGo()
        {
            return
            (IsInCorrectRoom(0) ? 0 : DistanceBetween(Positions[0], 20) * GetPower(0))
            + (IsInCorrectRoom(1) ? 0 : DistanceBetween(Positions[1], 21) * GetPower(1))
            + (IsInCorrectRoom(2) ? 0 : DistanceBetween(Positions[2], 40) * GetPower(2))
            + (IsInCorrectRoom(3) ? 0 : DistanceBetween(Positions[3], 41) * GetPower(3))
            + (IsInCorrectRoom(4) ? 0 : DistanceBetween(Positions[4], 60) * GetPower(4))
            + (IsInCorrectRoom(5) ? 0 : DistanceBetween(Positions[5], 61) * GetPower(5))
            + (IsInCorrectRoom(6) ? 0 : DistanceBetween(Positions[6], 80) * GetPower(6))
            + (IsInCorrectRoom(7) ? 0 : DistanceBetween(Positions[7], 81) * GetPower(7));
        }

        public int DistanceBetween(int posA, int posB)
        {
            if (posA <= 11 && posA >= 0)
            {
                if (posB <= 11 && posB >= 0)
                {
                    return Math.Abs(posB - posA);
                }
                if (posB % 10 == 1)
                {
                    return Math.Abs((posB - 1) / 10 - posA) + 2;
                }
                return Math.Abs(posB / 10 - posA) + 1;
            }
            if (posA % 10 == 1)
            {
                return DistanceBetween((posA - 1) / 10, posB) + 2;
            }
            return DistanceBetween(posA / 10, posB) + 1;
        }

        public bool IsDone() => Enumerable.Range(0, Positions.Length).All(IsInCorrectRoom);

        public override string ToString()
        {
            return $"{Distance,6}: {string.Join(' ', Positions.Select(p => p.ToString().PadLeft(3)))}";
        }

        private IEnumerable<(int position, int distance)> NextPositions(int pos, int idx)
        {
            if (pos >= 0 && pos <= 10)
            {
                var room = GetCorrectRoom(idx);
                if (IsValidRoom(room))
                {
                    var hall = room / 10;
                    if (pos > hall && Enumerable.Range(hall, pos - hall - 1).Any(PosIsOccupied))
                    {
                        yield break;
                    }
                    if (pos < hall && Enumerable.Range(pos - 1, hall - pos - 1).Any(PosIsOccupied))
                    {
                        yield break;
                    }
                    if (!Positions.Any(p => p == room + 1))
                    {
                        room++;
                    }
                    yield return (room, DistanceBetween(pos, room));
                }
            }
            else
            {
                if (pos % 10 == 1 && PosIsOccupied(pos - 1))
                {
                    yield break;
                }
                var hall = pos / 10;
                if (IsValidRoom(hall * 10))
                {
                    yield break;
                }
                for (int i = hall - 1; i >= 0 && !PosIsOccupied(i); i--)
                {
                    if (i == 2 || i == 4 || i == 6 | i == 8)
                    {
                        continue;
                    }
                    yield return (i, DistanceBetween(pos, i));
                }
                for (int i = hall + 1; i <= 10 && !PosIsOccupied(i); i++)
                {
                    if (i == 2 || i == 4 || i == 6 | i == 8)
                    {
                        continue;
                    }
                    yield return (i, DistanceBetween(pos, i));
                }
            }
        }

        private bool IsInCorrectRoom(int idx)
        {
            var room = GetCorrectRoom(idx);
            return Positions[idx] == room || Positions[idx] == room + 1;
        }

        private int GetCorrectRoom(int idx)
        {
            return (idx / 2 + 1) * 20;
        }

        private bool IsValidRoom(int room)
        {
            return Enumerable.Range(0, Positions.Length)
                .Where(idx => Positions[idx] == room || Positions[idx] == room + 1)
                .All(idx => GetCorrectRoom(idx) == room);
        }

        private bool PosIsOccupied(int pos)
        {
            return Positions.Contains(pos);
        }

        private int GetPower(int idx) =>
            idx == 0 ? 1
            : idx == 1 ? 1
            : idx == 2 ? 10
            : idx == 3 ? 10
            : idx == 4 ? 100
            : idx == 5 ? 100
            : idx == 6 ? 1000
            : idx == 7 ? 1000
            : 0;
    }
}