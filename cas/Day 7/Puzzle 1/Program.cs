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
                int result = crabPosition.Sum(item => i < item ? item - i : i - item);
                if(result < lowestFuelConsumption)
                {
                    lowestFuelConsumption = result;
                }
            }

            Console.WriteLine(lowestFuelConsumption);
        }
    }
}
