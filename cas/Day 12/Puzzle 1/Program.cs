using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent12a
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
                    if ((children[i].IsSmall && line.IndexOf(children[i].Name) == -1) || !children[i].IsSmall)
                    {
                        var result = $"{line}-{children[i].Name}";
                        processConnections(children[i], result);
                        amountofconnections++;
                    }
                }
            }
        }
    }
}
