using System;
using System.Collections.Generic;
using System.Linq;

namespace codeavent3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<string> input = Input.data.Split("\r\n").ToList();
            foreach(string bit in input)
            {
                var bits = bit.ToCharArray();
                for(int i = 0; i < bits.Length; i++)
                {
                    if (bits[i].Equals('0'))
                    {
                        numbers[i] -= 1;
                    }

                    if (bits[i].Equals('1'))
                    {
                        numbers[i] += 1;
                    }
                }
            }

            int gamma = Convert.ToInt32(string.Join("", numbers.Select((number) => number > 0 ? "1" : "0")), 2);
            int epsilon = Convert.ToInt32(string.Join("", numbers.Select((number) => number < 0 ? "1" : "0")),2);
            Console.WriteLine($"Gamma: {gamma}, Epsilon: {epsilon}, Consumption: {gamma * epsilon}");
        }
    }
}
