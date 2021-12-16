using System.Collections.Generic;

namespace codeadvent12a
{
    public class Cave
    {
        public string Name { get; set; }
        public List<Cave> Connections { get; set; }
        public bool IsSmall { get; set; }

        public Cave (string name, bool issmall)
        {
            Name = name;
            IsSmall = issmall;
            Connections = new List<Cave>();
        }
    }
}
