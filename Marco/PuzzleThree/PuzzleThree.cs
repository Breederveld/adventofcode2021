using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    class PuzzleThree
    {
        public static void PartOne(string filePath)
        {
            var numbers = System.IO.File.ReadAllLines(filePath).ToArray();
            var binaryCommon = String.Empty;
            var binaryLeastCommon = string.Empty;
            var zeroCounter = 0;
            var oneCounter = 0;

            for (int i = 0; i < 12; i++)
            {
                foreach (var number in numbers)
                {

                    var checker = number.Substring(i, 1);
                    if (int.Parse(checker).Equals(0))
                    {
                        zeroCounter++;
                        continue;
                    }
                    else
                    {
                        oneCounter++;
                        continue;
                    }
                }

                if (zeroCounter > oneCounter)
                {
                    binaryCommon = binaryCommon + "0";
                    binaryLeastCommon = binaryLeastCommon + "1";
                    zeroCounter = 0;
                    oneCounter = 0;
                }
                else
                {
                    binaryCommon = binaryCommon + "1";
                    binaryLeastCommon = binaryLeastCommon + "0";
                    oneCounter = 0;
                    zeroCounter = 0;
                }
            }
            var gammaResult = Convert.ToInt32(binaryCommon, 2);
            var epsilonResult = Convert.ToInt32(binaryLeastCommon, 2);

            var power = gammaResult * epsilonResult;
        }

        public static void PartTwo(string filePath)
        {
            var numbers = System.IO.File.ReadAllLines(filePath).ToList();

            int oxygenSum = RecursiveListCommon(numbers, 0);
            int scrubberSum = RecursiveListLeastCommon(numbers, 0);

            var lifeSupportRating = oxygenSum * scrubberSum;

        }

        public static int RecursiveListCommon(List<string> numbersList, int counter)
        {
            int oxygenSum = 0;
            var loop = counter;
            List<string> returnList = new List<string>();

            if (numbersList.Count == 1)
            {
                oxygenSum = Convert.ToInt32(numbersList[0], 2);
                return oxygenSum;
            }

            var zeroList = numbersList.Where(e => e.Substring(counter, 1).Equals("0")).ToList();
            var oneList = numbersList.Where(e => e.Substring(counter, 1).Equals("1")).ToList();

            if (zeroList.Count > oneList.Count)
            {
                returnList = zeroList;
                loop++;
                return RecursiveListCommon(returnList, loop);
            }
            else if (oneList.Count >= zeroList.Count)
            {
                returnList = oneList;
                loop++;
                return RecursiveListCommon(returnList, loop);
            }

            return oxygenSum;
        }

        public static int RecursiveListLeastCommon(List<string> numbersList, int counter)
        {
            int scrubberSum = 0;
            int loop = counter;
            List<string> returnList = new List<string>();

            if (numbersList.Count == 1)
            {
                return scrubberSum = Convert.ToInt32(numbersList[0], 2);
            }

            var zeroList = numbersList.Where(e => e.Substring(counter, 1).Equals("0")).ToList();
            var oneList = numbersList.Where(e => e.Substring(counter, 1).Equals("1")).ToList();

            if (oneList.Count < zeroList.Count)
            {
                returnList = oneList;
                loop++;
                return RecursiveListLeastCommon(returnList, loop);
            }
            else if (zeroList.Count <= oneList.Count)
            {
                returnList = zeroList;
                loop++;
                return RecursiveListLeastCommon(returnList, loop);
            }

            return scrubberSum;
        }
    }
}
