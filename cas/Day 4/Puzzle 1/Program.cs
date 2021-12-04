using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> bingonumbers = Input.GetInputNumbers().ToList();
            List<BingoCart> bingoCarts = Input.GetBoards().ToList();
            foreach(int number in bingonumbers)
            {
                foreach(BingoCart cart in bingoCarts)
                {
                    if (cart.CheckNumber(number))
                    {
                        bool bingo = cart.CheckForBingo();
                        if (bingo)
                        {
                            int sumunusednumbers = cart.Numbers.Where(number => !cart.GuessedNumbers.Contains(number)).Sum(number => number);
                            int awnser = sumunusednumbers * number;
                        }
                    }
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
