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
            var windows = MapToWindow(numbers);
            for(int i = 1; i < windows.Count; i++)
            {
                if (windows[i - 1] < windows[i])
                    amountIncreased++;
                amountProcessed++;
            }
            Console.WriteLine(amountIncreased);
            Console.WriteLine(amountProcessed);
        }

        static List<int> MapToWindow(List<int> input)
        {
            var result = new List<int>();
            for(int i = 0; i < input.Count - 2; i++)
            {
                result.Add(input[i] + input[i + 1] + input[i + 2]);
            }
            return result;
        }
    }
}
