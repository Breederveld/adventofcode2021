using System;
using System.Linq;

namespace codeavent2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var strings = Input.data.Trim().Split("\r\n").ToArray();
            var depthonator = new Depthonator();
            foreach (string dataline in strings)
            {
                var action = dataline.Split(" ")[0];
                var amount = Convert.ToInt32(dataline.Split(" ")[1]);

                if (action.Equals("forward"))
                    depthonator.MoveForward(amount);

                if (action.Equals("up"))
                    depthonator.DecreaseDepth(amount);

                if (action.Equals("down"))
                    depthonator.IncreaseDepth(amount);
            }

            Console.WriteLine($"Depth: {depthonator.Depth}");
            Console.WriteLine($"Forward: {depthonator.Forward}");
            Console.WriteLine($"Multiplied: {depthonator.Depth * depthonator.Forward}");

        }
    }


    public class Depthonator
    {
        public int Depth { get; private set; }
        public int Forward { get; private set; }
        private int Aim;
        public Depthonator()
        {
            Aim = 0;
        }

        public void IncreaseDepth(int amount)
        {
            Aim += amount;
        }

        public void DecreaseDepth(int amount)
        {
            Aim -= amount;
        }

        public void MoveForward(int amount)
        {
            Forward += amount;
            Depth += (amount * Aim);
        }
    }
}
