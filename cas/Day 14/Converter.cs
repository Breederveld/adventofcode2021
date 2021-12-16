using System.Collections.Generic;

namespace codeadvent14a
{
    public class Converter
    {
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public string MiddleCharacter { get; set; }
        public List<string> Relatives { get; set; } = new List<string>();

        public Converter(string symbol, string middlecharacter)
        {
            Symbol = symbol;
            MiddleCharacter = middlecharacter;
            Amount = 0;
            Relatives.Add($"{Symbol[0]}{MiddleCharacter}");
            Relatives.Add($"{MiddleCharacter}{Symbol[1]}");
        }
    }
}
