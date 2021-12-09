using System;
using System.Collections.Generic;

namespace codeavent9_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = Input.Map();
            List<(int, int)> lowestpoints = new List<(int, int)>();
            for (int i = 0; i < map.Length; i++)
            {
                for (int b = 0; b < map[i].Length; b++)
                {
                    bool lowestpoint = true;
                    if (lowestpoint && b != 0)
                    {
                        lowestpoint = map[i][b] < map[i][(b - 1)];
                    }
                    if (lowestpoint && b != map[i].Length - 1)
                    {
                        lowestpoint = map[i][b] < map[i][(b + 1)];
                    }

                    if (lowestpoint && i != 0)
                    {
                        lowestpoint = map[i][b] < map[(i - 1)][b];
                    }

                    if (lowestpoint && i != map.Length - 1)
                    {
                        lowestpoint = map[i][b] < map[(i + 1)][b];
                    }
                    if (lowestpoint)
                    {
                        lowestpoints.Add((i , b));
                    }
                }
            }

            List<int> basins = new List<int>();
            foreach((int, int) point in lowestpoints)
            {
                basins.Add(CheckForNines(map, point.Item1, point.Item2));
            }

            basins.Sort((itema, itemb) =>
            {
                if(itema < itemb)
                {
                    return 1;
                } 

                if(itema > itemb)
                {
                    return -1;
                }
                return 0;
            });

            Console.WriteLine(basins[0] * basins[1] * basins[2]);
        }

        public static int CheckForNines(int[][] map, int y, int x)
        {
            int number = 1;
            map[y][x] = 9;
            if(x != 0)
            {
                if(map[y][x - 1] != 9 && map[y][x - 1] != 10)
                {
                    
                    number += CheckForNines(map, y, x - 1);
                }
            }
            if (x != map[x].Length - 1)
            {
                if (map[y][x + 1] != 9 && map[y][x + 1] != 10)
                {
                    number += CheckForNines(map, y, x + 1);
                }
            }
            if (y != 0)
            {
                if (map[y - 1][x] != 9 && map[y - 1][x] != 10)
                {
                    number += CheckForNines(map, y - 1, x);
                }
            }
            if (y != map.Length - 1)
            {
                if (map[y + 1][x] != 9 && map[y + 1][x] != 10)
                {
                    number += CheckForNines(map, y + 1, x);
                }
            }
            return number;
        }
    }
}
