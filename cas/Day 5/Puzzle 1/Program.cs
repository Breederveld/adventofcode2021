using System;
using System.Collections.Generic;
using System.Linq;

namespace codeavent5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Line> lines = Input.GetCoordinates().ToList();
            int[,] map = new int[1000, 1000];
            lines = lines.Where(line => line.X1 == line.X2 || line.Y1 == line.Y2).ToList();
            foreach(Line line in lines)
            {
                if(line.X1 != line.X2)
                {
                    int lowerx = line.X1 < line.X2 ? line.X1 : line.X2;
                    int higherx = line.X1 < line.X2 ? line.X2 : line.X1;
                    for(int i = lowerx; i < (higherx + 1); i++)
                    {
                        map[i, line.Y1] += 1;
                    }
                } else
                {
                    int lowery = line.Y1 < line.Y2 ? line.Y1 : line.Y2;
                    int highery = line.Y1 < line.Y2 ? line.Y2 : line.Y1;
                    for (int i = lowery; i < (highery + 1); i++)
                    {
                        map[line.X1, i] += 1;
                    }
                }
            }
            int counter = 0;
            for(int x = 0; x < 1000; x++)
            {
                for(int y = 0; y < 1000; y++)
                {
                    if(map[x,y] > 1)
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine(counter);
        }
    }
}
