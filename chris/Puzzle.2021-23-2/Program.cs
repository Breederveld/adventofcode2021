using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_2021_23_1
{
    class Program
    {
        //async Task Main(string[] args)
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var strings = input.Trim().Split("\n").Select(s => s.TrimEnd()).ToArray();
            //var groups = input.Trim().Split("\n\n").Select(grp => grp.Split("\n").ToArray()).ToArray();
            //var ints = strings.Where(st => !string.IsNullOrWhiteSpace(st)).Select(st => int.Parse(st)).ToArray();

            var rooms = strings.Skip(2).Take(4)
                .SelectMany(s => new[] { s[3], s[5], s[7], s[9] })
                .Select((c, i) => (c, i))
                .GroupBy(t => t.c, t => t.i / 4 + (t.i % 4 + 1) * 20)
                .ToDictionary(grp => grp.Key, grp => grp.ToArray());
            var initialState = new GameState(4,
                Enumerable.Range(0, 7).Select(_ => '.')
                .Concat(strings[2..6].Select(s => s[3]))
                .Concat(strings[2..6].Select(s => s[5]))
                .Concat(strings[2..6].Select(s => s[7]))
                .Concat(strings[2..6].Select(s => s[9]))
                .ToArray());
            var states = new PriorityQueue<GameState, double>();
            states.Enqueue(initialState, initialState.Distance);
            var bestWin = double.MaxValue;
            var visited = new HashSet<GameState>();

            var loops = 0d;
            while (states.TryDequeue(out var state, out var _))
            {
                loops++;
                if (!visited.Add(state))
                {
                    continue;
                }
                if (state.IsDone())
                {
                    if (state.Distance < bestWin)
                    {
                        var steps = new List<GameState>();
                        var parent = state;
                        while (parent != null)
                        {
                            steps.Add(parent);
                            parent = parent.Parent;
                        }
                        steps.Reverse();
                        foreach (var step in steps)
                        {
                            Console.WriteLine(step.ToString());
                        }

                        bestWin = state.Distance;
                    }
                    break;
                }
                foreach (var s in state.Next())
                {
                    states.Enqueue(s, s.Distance);
                }
            }

            Console.WriteLine(bestWin.ToString());
            await Task.FromResult(0);
        }
    }

    // Positions: 0-6, 7-10, 11-14, 15-18, 19-22
    public class GameState
    {
        private static Dictionary<char, int> _powers = new Dictionary<char, int> { ['A'] = 1, ['B'] = 10, ['C'] = 100, ['D'] = 1000 };
        private int _roomSize;
        private PathInfo _pathInfo;

        public GameState(int roomSize, char[] positions)
        {
            _roomSize = roomSize;
            Distance = 0;
            var p = new PositionsList();
            for (var i = 0; i < positions.Length; i++)
            {
                p[i] = positions[i];
            }
            Positions = p;
            _pathInfo = new PathInfo(roomSize);
        }

        public GameState(double distance, PositionsList positions, PathInfo pathInfo)
        {
            Distance = distance;
            Positions = positions;
            _pathInfo = pathInfo;
        }

        public double Distance { get; }
        public PositionsList Positions { get; set; }
        public GameState Parent { get; set; }

        public IEnumerable<GameState> Next()
        {
            bool returned = false;
            // Move to a room.
            for (var pos = 0; pos < 7; pos++)
            {
                foreach (var state in Next(pos, _ => true))
                {
                    returned = true;
                    yield return state;
                }
            }
            if (returned)
                yield break;
            for (var pos = 7; pos < PositionsList.Length; pos++)
            {
                foreach (var state in Next(pos, _ => true))
                {
                    yield return state;
                }
            }
        }

        private IEnumerable<GameState> Next(int pos, Func<char, bool> filter)
        {
            var room = Positions[pos];
            if (room == '.' || !filter(room))
                yield break;
            var power = _powers[room];
            foreach (var p in NextPositions(pos, room))
            {
                var newPositions = Positions;
                newPositions[pos] = '.';
                newPositions[p.position] = room;
                yield return new GameState(Distance + power * p.distance, newPositions, _pathInfo) { Parent = this };
            }
        }

        public bool IsDone() => Enumerable.Range(7, PositionsList.Length - 7).All(pos => _pathInfo.Allowed(pos, Positions[pos]));

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(GetHashCode().ToString());
            builder.AppendLine($"###{Distance,7}###");
            builder.AppendLine($"#{Positions[0]}{Positions[1]}.{Positions[2]}.{Positions[3]}.{Positions[4]}.{Positions[5]}{Positions[6]}#");
            builder.AppendLine($"###{Positions[7]}#{Positions[11]}#{Positions[15]}#{Positions[19]}###");
            builder.AppendLine($"  #{Positions[8]}#{Positions[12]}#{Positions[16]}#{Positions[20]}#  ");
            builder.AppendLine($"  #{Positions[9]}#{Positions[13]}#{Positions[17]}#{Positions[21]}#  ");
            builder.AppendLine($"  #{Positions[10]}#{Positions[14]}#{Positions[18]}#{Positions[22]}#  ");
            return builder.ToString();
        }

        public override int GetHashCode() => Positions.GetHashCode();

        public override bool Equals(object obj)
        {
            return Enumerable.Range(0, PositionsList.Length).All(i => ((GameState)obj).Positions[i] == Positions[i]);
        }

        private IEnumerable<(int position, int distance)> NextPositions(int pos, char room)
        {
            var moves = _pathInfo.AllPaths[pos]
                // Filter om room.
                .Where(t => _pathInfo.Allowed(t.path.Last(), room))
                // Filter on valid room.
                .Where(t => t.path.Last() < 7 || IsValidRoom(room))
                // Filter taken places.
                .Where(t => !t.path.Any(pos => PosIsOccupied(pos)));
            foreach ((var path, var distance) in moves)
            {
                var position = path.Last();
                if (position > 6)
                {
                    if (!IsValidRoom(room) || !_pathInfo.RoomPositions(room).TakeWhile(p => p != position).All(p => PosIsOccupied(p)))
                    {
                        continue;
                    }
                }
                yield return (position, distance);
            }
        }

        private bool IsValidRoom(char room)
        {
            return _pathInfo.RoomPositions(room)
                .All(pos => Positions[pos] == room || Positions[pos] == '.');
        }

        private bool PosIsOccupied(int pos)
        {
            return Positions[pos] != '.';
        }
    }

    public struct PositionsList
    {
        public static int Length = 23;
        private char _0;
        private char _1;
        private char _2;
        private char _3;
        private char _4;
        private char _5;
        private char _6;
        private char _7;
        private char _8;
        private char _9;
        private char _10;
        private char _11;
        private char _12;
        private char _13;
        private char _14;
        private char _15;
        private char _16;
        private char _17;
        private char _18;
        private char _19;
        private char _20;
        private char _21;
        private char _22;

        public char this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return _0;
                    case 1: return _1;
                    case 2: return _2;
                    case 3: return _3;
                    case 4: return _4;
                    case 5: return _5;
                    case 6: return _6;
                    case 7: return _7;
                    case 8: return _8;
                    case 9: return _9;
                    case 10: return _10;
                    case 11: return _11;
                    case 12: return _12;
                    case 13: return _13;
                    case 14: return _14;
                    case 15: return _15;
                    case 16: return _16;
                    case 17: return _17;
                    case 18: return _18;
                    case 19: return _19;
                    case 20: return _20;
                    case 21: return _21;
                    case 22: return _22;
                    default:
                        throw new Exception();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: _0 = value; break;
                    case 1: _1 = value; break;
                    case 2: _2 = value; break;
                    case 3: _3 = value; break;
                    case 4: _4 = value; break;
                    case 5: _5 = value; break;
                    case 6: _6 = value; break;
                    case 7: _7 = value; break;
                    case 8: _8 = value; break;
                    case 9: _9 = value; break;
                    case 10: _10 = value; break;
                    case 11: _11 = value; break;
                    case 12: _12 = value; break;
                    case 13: _13 = value; break;
                    case 14: _14 = value; break;
                    case 15: _15 = value; break;
                    case 16: _16 = value; break;
                    case 17: _17 = value; break;
                    case 18: _18 = value; break;
                    case 19: _19 = value; break;
                    case 20: _20 = value; break;
                    case 21: _21 = value; break;
                    case 22: _22 = value; break;
                    default:
                        throw new Exception();
                }
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashcode = 1430287;
                hashcode = hashcode * 7302013 ^ _0;
                hashcode = hashcode * 7302013 ^ _1;
                hashcode = hashcode * 7302013 ^ _2;
                hashcode = hashcode * 7302013 ^ _3;
                hashcode = hashcode * 7302013 ^ _4;
                hashcode = hashcode * 7302013 ^ _5;
                hashcode = hashcode * 7302013 ^ _6;
                hashcode = hashcode * 7302013 ^ _7;
                hashcode = hashcode * 7302013 ^ _8;
                hashcode = hashcode * 7302013 ^ _9;
                hashcode = hashcode * 7302013 ^ _10;
                hashcode = hashcode * 7302013 ^ _11;
                hashcode = hashcode * 7302013 ^ _12;
                hashcode = hashcode * 7302013 ^ _13;
                hashcode = hashcode * 7302013 ^ _14;
                hashcode = hashcode * 7302013 ^ _15;
                hashcode = hashcode * 7302013 ^ _16;
                hashcode = hashcode * 7302013 ^ _17;
                hashcode = hashcode * 7302013 ^ _18;
                hashcode = hashcode * 7302013 ^ _19;
                hashcode = hashcode * 7302013 ^ _20;
                hashcode = hashcode * 7302013 ^ _21;
                hashcode = hashcode * 7302013 ^ _22;
                return hashcode;
            }
        }
    }

    // Positions: 0-6
    // 1-2: 7-10
    // 2-3: 11-14
    // 3-4: 15-18
    // 4-5: 19-22
    public class PathInfo
    {
        private readonly int _roomSize;
        private readonly Dictionary<int, int> _roomStarts;

        public PathInfo(int roomSize)
        {
            _roomSize = roomSize;
            _roomStarts = Enumerable.Range(0, 4)
                .ToDictionary(i => i + 1, i => i * roomSize + 7);
            AllPaths = Generate();
        }

        public Dictionary<int, (int[] path, int distance)[]> AllPaths { get; }

        public bool Allowed(int pos, char c) => pos < 7 || (pos - 7) / _roomSize == c - 'A';

        public IEnumerable<int> RoomPositions(char room) => Enumerable.Range(0, _roomSize).Reverse().Select(i => _roomStarts[room - 'A' + 1] + i);

        private Dictionary<int, (int[] path, int distance)[]> Generate()
        {
            return Enumerable.Range(0, 7 + 4 * _roomSize)
                .ToDictionary(i => i, i => GetPaths(i).ToArray());
        }

        private IEnumerable<(int[] path, int distance)> GetPaths(int pos)
        {
            // Hall to room paths.
            if (pos < 7)
            {
                var path = new List<int>();
                var distance = 0;
                for (var newPos = pos; newPos < 7; newPos++)
                {
                    if (newPos != pos)
                    {
                        distance++;
                        path.Add(newPos);
                    }
                    if (_roomStarts.ContainsKey(newPos))
                    {
                        distance++;
                        var roomStart = _roomStarts[newPos];
                        for (int i = 0; i < _roomSize; i++)
                        {
                            distance++;
                            path.Add(roomStart + i);
                            yield return (path.ToArray(), distance);
                        }
                        distance -= _roomSize;
                        path.RemoveRange(path.Count - _roomSize, _roomSize);
                    }
                }
                path.Clear();
                distance = 0;
                for (var newPos = pos; newPos >= 0; newPos--)
                {
                    if (newPos != pos)
                    {
                        distance++;
                        path.Add(newPos);
                    }
                    if (_roomStarts.ContainsKey(newPos - 1))
                    {
                        distance++;
                        var roomStart = _roomStarts[newPos - 1];
                        for (int i = 0; i < _roomSize; i++)
                        {
                            distance++;
                            path.Add(roomStart + i);
                            yield return (path.ToArray(), distance);
                        }
                        distance -= _roomSize;
                        path.RemoveRange(path.Count - _roomSize, _roomSize);
                    }
                }
            }
            // Room to hall paths.
            else
            {
                var path = new List<int>();
                var distance = 1;
                var startPos = pos;
                while (!_roomStarts.ContainsValue(startPos))
                {
                    startPos--;
                    distance++;
                    path.Add(startPos);
                }

                var hallStart = _roomStarts.Single(kv => kv.Value == startPos).Key;
                var distStart = distance;
                var pathLength = path.Count;
                for (var newPos = hallStart; newPos >= 0; newPos--)
                {
                    distance++;
                    path.Add(newPos);
                    yield return (path.ToArray(), distance);
                    if (_roomStarts.ContainsKey(newPos - 1))
                    {
                        distance++;
                    }
                }
                path.RemoveRange(pathLength, path.Count - pathLength);
                distance = distStart;
                for (var newPos = hallStart + 1; newPos < 7; newPos++)
                {
                    distance++;
                    path.Add(newPos);
                    yield return (path.ToArray(), distance);
                    if (_roomStarts.ContainsKey(newPos))
                    {
                        distance++;
                    }
                }
            }
        }
    }
}