using System;
using System.Linq;

namespace codeavent7_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var crabPosition = Input.CrabPositions();
            int highestCrabPosition = crabPosition.Max(item => item);
            int lowerCrabPosition = crabPosition.Min(item => item);

            int lowestFuelConsumption = 1000000000;
            for(int i = lowerCrabPosition; i < highestCrabPosition; i++)
            {
                int result = crabPosition.Select(item => i < item ? item - i : i - item).Sum(item => item * (item + 1) / 2);
                if (result < lowestFuelConsumption)
                {
                    lowestFuelConsumption = result;
                }
            }

            Console.WriteLine(lowestFuelConsumption);
        }
    }
}
