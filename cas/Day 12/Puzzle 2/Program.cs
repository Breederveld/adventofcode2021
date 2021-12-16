using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent12b
{
    class Program
    {
        public static List<string> solutions = new List<string>();
        public static int amountofconnections = 0;
        public static void Main(string[] args)
        {
            var map = Input.Map();
            //Filter out points that are not usefull
            map = map.Where(item => !(item.Connections.Count == 1 && item.Connections.All(a => a.IsSmall))).ToList();
            var start = map.Find(item => item.Name == "start");
            processConnections(start, "start");
            Console.WriteLine(amountofconnections);
            Console.WriteLine(solutions.Count);
        }

        static void processConnections(Cave cave, string line)
        {
            if (cave.Name.Equals("end"))
            {
                if (!solutions.Contains(line))
                    solutions.Add(line);
            }
            else
            {

                var children = cave.Connections.Where(item => !item.Name.Equals("start")).ToList();
                for (int i = 0; i < children.Count; i++)
                {
                    if ((children[i].IsSmall && CanUseSamllCave(line, children[i].Name) || !children[i].IsSmall))
                    {
                        var result = $"{line}-{children[i].Name}";
                        processConnections(children[i], result);
                        amountofconnections++;
                    }
                }
            }
        }

        public static bool CanUseSamllCave(string line, string name)
        {
            if (line.IndexOf(name) == -1)
                return true;

            var caves = line.Split("-");
            var smallcaves = caves.Where(item => item.All(character => char.IsLower(character))).ToList();
            for(int a = 0; a < smallcaves.Count; a++)
            {
                for(int b = 0; b < smallcaves.Count; b++)
                {
                    if (a != b && smallcaves[a].Equals(smallcaves[b]))
                        return false;
                }
            }
            return true;
        }
    }
}
