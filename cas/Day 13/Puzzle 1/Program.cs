using System;

namespace codeadvent13a
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = Input.Map();
            var instructions = Input.Instructions();

            //foreach((string, int) instruction in instructions)
            //{
            //    map = Fold(instruction, map);
            //}
            int counter = 0;
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int b = 0; b < map.GetLength(1); b++)
                {
                    if (map[i, b] == 1)
                        counter++;
                }
            }
            Console.WriteLine(counter);
        }

        public static int[,] Fold((string,int) instruction, int[,] map)
        {
            if (instruction.Item1.Equals("x"))
            {
                int ylength = map.GetLength(1);
                int xlength = map.GetLength(0);
                int longestpart = xlength - instruction.Item2 > instruction.Item2 ? xlength - instruction.Item2 : instruction.Item2;
                int[,] newmap = new int[(longestpart - 1),ylength ];
                for (int y = 0; y < ylength; y++)
                {
                    for (int x = 0; x < xlength; x++)
                    {
                        if ((longestpart - 1) - x > 0)
                        {
                            newmap[x, y] = map[x, y];
                        }
                        else if ((longestpart - 1) == x)
                        {
                            // do nada
                        }
                        else
                        {
                            if (newmap[xlength - (x + 1),y ] == 0)
                                newmap[xlength - (x + 1), y] = map[x, y];
                        }
                    }
                }
                return newmap;

            }
            else
            {
                int ylength = map.GetLength(1);
                int xlength = map.GetLength(0);
                int longestpart = ylength - instruction.Item2 > instruction.Item2 ? ylength - instruction.Item2 : instruction.Item2;
                int[,] newmap = new int[xlength, (longestpart - 1)];
               
                for(int y = 0; y < ylength; y++)
                {
                    for (int x = 0; x < xlength; x++)
                    {
                        if ((longestpart - 1) - y > 0)
                        {
                            newmap[x, y] = map[x, y];
                        } else if ((longestpart - 1) == y)
                        {
                            // do nada
                        }else
                        {
                            if(newmap[x, ylength - (y + 1)] == 0)
                                newmap[x, ylength - (y + 1)] = map[x, y];
                        }
                    }
                }
                return newmap;

            }
        }
    }
}
