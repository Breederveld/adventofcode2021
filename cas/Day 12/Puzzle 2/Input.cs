using System.Collections.Generic;
using System.Linq;

namespace codeadvent12b
{
    public static class Input
    {
        public static List<Cave> Map()
        {
            List<Cave> caves = new List<Cave>();
            foreach (string row in data.Split("\r\n"))
            {
                var rowcaves = row.Split("-");
                var cave1 = caves.Find(item => item.Name.Equals(rowcaves[0]));
                if (cave1 == null)
                {
                    cave1 = new Cave(rowcaves[0], !rowcaves[0].All(item => char.IsUpper(item)));
                    caves.Add(cave1);

                }

                var cave2 = caves.Find(item => item.Name.Equals(rowcaves[1]));
                if (cave2 == null)
                {
                    cave2 = new Cave(rowcaves[1], !rowcaves[1].All(item => char.IsUpper(item)));
                    caves.Add(cave2);
                }

                cave1.Connections.Add(cave2);
                cave2.Connections.Add(cave1);

            }
            return caves;
        }
        public static string data => @"BC-gt
gt-zf
end-KH
end-BC
so-NL
so-ly
start-BC
NL-zf
end-LK
LK-so
ly-KH
NL-bt
gt-NL
start-zf
so-zf
ly-BC
BC-zf
zf-ly
ly-NL
ly-LK
IA-bt
bt-so
ui-KH
gt-start
KH-so";
    }
}
