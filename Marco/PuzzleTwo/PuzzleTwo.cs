namespace AOC2021
{
    public class PuzzleTwo
    {
        public static void PartOne(string filePath)
        {
            string[] positions = System.IO.File.ReadAllLines(filePath);
            int sumForward = 0;
            int sumDown = 0;
            foreach(var position in positions)
            {
                var splitUpPosition = position.Split(" ");
                var action = splitUpPosition[0].ToLower();
                var iterator = splitUpPosition[1];

                switch (action)
                {
                    case "forward":
                        sumForward = sumForward + int.Parse(iterator);
                        break;
                    case "down":
                        sumDown = sumDown + int.Parse(iterator);
                        break;
                    case "up":
                        sumDown = sumDown - int.Parse(iterator);
                        break;
                }

            }
            var totalSum = sumForward * sumDown;
        }

        public static void PartTwo(string filePath)
        {
            string[] positions = System.IO.File.ReadAllLines(filePath);
            int sumAim = 0;
            int sumForward = 0;
            int sumDown = 0;

            foreach (var position in positions)
            {
                var splitUpPosition = position.Split(" ");
                var action = splitUpPosition[0].ToLower();
                var iterator = splitUpPosition[1];

                switch (action)
                {
                    case "forward":
                        sumForward = sumForward + int.Parse(iterator);
                        sumDown = sumDown + (sumAim * int.Parse(iterator));
                        break;
                    case "down":
                        sumAim = sumAim + int.Parse(iterator);
                        break;
                    case "up":
                        sumAim = sumAim - int.Parse(iterator);
                        break;
                }

            }
            var totalSum = sumForward * sumDown;
        }

    }
}
