using System;
using System.Collections.Generic;
using System.Linq;

namespace codeavent9_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = Input.Map();
            List<int> lowestpoints = new List<int>();
            for(int i = 0; i < map.Length; i++)
            {
                for(int b = 0; b < map[i].Length; b++)
                {
                    bool lowestpoint = true;
                    if(lowestpoint && b != 0)
                    {
                        lowestpoint = map[i][b] < map[i][(b - 1)];
                    }
                    if(lowestpoint && b != map[i].Length - 1)
                    {
                        lowestpoint = map[i][b] < map[i][(b + 1)];
                    }

                    if(lowestpoint && i != 0)
                    {
                        lowestpoint = map[i][b] < map[(i - 1)][b];
                    }

                    if(lowestpoint && i != map.Length - 1)
                    {
                        lowestpoint = map[i][b] < map[(i + 1)][b];
                    }
                    if (lowestpoint)
                    {
                        lowestpoints.Add((map[i][b] + 1));
                    }
                }
            }

            Console.WriteLine(lowestpoints.Sum());
        }
    }
}
