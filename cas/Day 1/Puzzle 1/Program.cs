using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string input = Input.input;
            List<int> numbers = input.Split("\r\n").Select(item => Convert.ToInt32(item)).ToList();
            int amountIncreased = 0;
            int amountProcessed = 0;
            for(int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i - 1] < numbers[i])
                    amountIncreased++;
                amountProcessed++;
            }
            Console.WriteLine(amountIncreased);
            Console.WriteLine(amountProcessed);
        }
    }
}
