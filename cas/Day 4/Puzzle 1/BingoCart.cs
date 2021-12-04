using System.Collections.Generic;
using System.Linq;

namespace codeadvent4_1
{
    public class BingoCart
    {
        public List<int> Numbers { get; set; }
        private int[][] board = new int[5][];
        public List<int> GuessedNumbers;

        public BingoCart(List<int> numbers)
        {
            Numbers = numbers;
            GuessedNumbers = new List<int>();
            for(int i = 0; i < 5; i++)
            {
                board[i] = numbers.GetRange((i*5),5).ToArray();
            }
        }

        public bool CheckNumber(int number)
        {
            if (Numbers.Contains(number))
            {
                GuessedNumbers.Add(number);
                return true;
            }
            return false;
        }

        public bool CheckForBingo()
        {
            return CheckRows() || CheckColumns();
        }

        public bool CheckRows()
        {
             return board.Any(row => row.All(item => GuessedNumbers.Contains(item)));
        }

        private bool CheckColumns()
        {
            for(int i = 0; i < board.Length; i++)
            {
                bool column = true;
                for(int i2 = 0; i2 < board[i].Length; i2++)
                {
                    if (!GuessedNumbers.Contains(board[i2][i])) {
                        column = false;
                        break;
                    }
                }
                if (column)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
