using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class PuzzleOne
    {
        public static void CountIncreases(string filePath)
        {
            var counter = 0;
            int previousLine = 143;

            string[] numbers = System.IO.File.ReadAllLines(filePath);

            foreach (var number in numbers)
            {
                if (Int32.Parse(number) > previousLine)
                {
                    counter++;
                }
                previousLine = Int32.Parse(number);
            }
        }

        public static void PuzzlePartTwo(string filePath)
        {
            List<int> calculatedSums = new List<int>();
            string[] numbers = System.IO.File.ReadAllLines(filePath);
            var itterator = 0;
            var sum = 0;
            var increasedCounter = 0;
            int previousSum = 440;

            for (int i = 0; i< numbers.Length; i++)
            {                           
                if(itterator + 3 <= numbers.Length)
                {
                    var numbersForSum = numbers.Skip(itterator).Take(3).ToArray();
                    sum = Int32.Parse(numbersForSum[0]) + Int32.Parse(numbersForSum[1]) + Int32.Parse(numbersForSum[2]);
                }
                
                itterator++;

                calculatedSums.Add(sum);
            }

            foreach(var calcSum in calculatedSums)
            {
                if(calcSum > previousSum)
                {
                    increasedCounter++;
                }
                previousSum = calcSum;
            }
        }
    }
}
