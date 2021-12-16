namespace codeavent11a
{
    public static class Input
    {
        public static int[,] Map()
        {
            int[,] map = new int[10, 10];
            var lines = data.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                
                for (int b = 0; b < lines[i].Length; b++)
                {
                    map[i, b] = int.Parse(lines[i][b].ToString());
                }
            }
            return map;
        }
        private static string data => @"8271653836
7567626775
2315713316
6542655315
2453637333
1247264328
2325146614
2115843171
6182376282
2384738675";
    }
}
