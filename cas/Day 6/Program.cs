using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventofCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, decimal> daysandamountoffish = Input.DaysLaternFishMap();
            Dictionary<int, decimal> startofday = new Dictionary<int, decimal>(daysandamountoffish);
            for (int i = 0; i < 256; i++)
            {
                Dictionary<int, decimal> endofday = new Dictionary<int, decimal>();
                foreach(KeyValuePair<int,decimal> fish in startofday)
                {
                    if(fish.Key > 0)
                    {
                        if (endofday.ContainsKey(fish.Key - 1))
                        {
                            endofday[(fish.Key - 1)] += fish.Value;
                        }
                        else
                        {
                            endofday.Add((fish.Key - 1), fish.Value);
                        }
                    }
                    if(fish.Key == 0)
                    {
                        if (endofday.ContainsKey(6))
                        {
                            endofday[6] += fish.Value;
                        }
                        else
                        {
                            endofday.Add(6, fish.Value);
                        }
                        if (endofday.ContainsKey(8))
                        {
                            endofday[8] += fish.Value;
                        }
                        else
                        {
                            endofday.Add(8, fish.Value);
                        }
                    }
                }
                startofday = new Dictionary<int, decimal>(endofday);
            }
            Console.WriteLine(startofday.Select(item => item.Value).Sum(item => item));
        }
    }
}
