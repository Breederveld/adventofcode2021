using System;
using System.Collections.Generic;
using System.Linq;

namespace codeavent3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<string> input = Input.data.Split("\r\n").ToList();
            List<string> copy = input.Select(item => $"{item}").ToList();
            for(int activebit = 0; activebit < 12; activebit++)
            {
                foreach(string bit in copy)
                {
                    var bits = bit.ToCharArray();
                    if (bits[activebit].Equals('0'))
                    {
                        numbers[activebit] -= 1;
                    }

                    if (bits[activebit].Equals('1'))
                    {
                        numbers[activebit] += 1;
                    }
                }
                if(numbers[activebit] >= 0)
                {
                    copy = copy.Where(item => item.ToCharArray()[activebit].Equals('1')).ToList();
                } else
                {
                    copy = copy.Where(item => item.ToCharArray()[activebit].Equals('0')).ToList();
                }
                if (copy.Count == 1)
                    break;
            }

            int oxygen = Convert.ToInt32(copy[0],2);
            numbers = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            copy = input.Select(item => $"{item}").ToList();
            for (int activebit = 0; activebit < 12; activebit++)
            {
                foreach (string bit in copy)
                {
                    var bits = bit.ToCharArray();
                    if (bits[activebit].Equals('0'))
                    {
                        numbers[activebit] -= 1;
                    }

                    if (bits[activebit].Equals('1'))
                    {
                        numbers[activebit] += 1;
                    }
                }
                if (numbers[activebit] < 0)
                {
                    copy = copy.Where(item => item.ToCharArray()[activebit].Equals('1')).ToList();
                }
                else
                {
                    copy = copy.Where(item => item.ToCharArray()[activebit].Equals('0')).ToList();
                }
                if (copy.Count == 1)
                    break;
            }
             
            int c02scrubber = Convert.ToInt32(copy[0], 2);
            Console.WriteLine($"Oxygen: {oxygen}, C02Scrubber rating: {c02scrubber}, Lifesupport rating: {oxygen * c02scrubber}");

        }
    }
}
