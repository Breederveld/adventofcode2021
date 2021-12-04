using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent4_2
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
                        if (bingo && !cart.HasBingo)
                        {
                            cart.HasBingo = true;
                            if(bingoCarts.Where(cart => !cart.HasBingo).Count() == 0)
                            {
                                int sumunusednumbers = cart.Numbers.Where(number => !cart.GuessedNumbers.Contains(number)).Sum(number => number);
                                int awnser = sumunusednumbers * number;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
