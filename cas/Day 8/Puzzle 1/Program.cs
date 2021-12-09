using System;
using System.Linq;

namespace codeavent8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputs = Input.Outputs().ToList();
            Console.WriteLine(outputs.Where(item => item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7).Count());
        }
    }
}
