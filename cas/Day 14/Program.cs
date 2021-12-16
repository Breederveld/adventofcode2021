using System;
using System.Collections.Generic;
using System.Linq;

namespace codeadvent14a
{
    class Program
    {
        static void Main(string[] args)
        {
            string startpoint = Input.StartPoint;
            List<Converter> converters = Input.Converters();

            string firstcharacter = startpoint[0].ToString();
            string lastcharacter = startpoint[startpoint.Length - 1].ToString();

            for(int i = 1; i < startpoint.Length; i++)
            {
                Converter converter = converters.Find(item => item.Symbol.Equals($"{startpoint[i - 1]}{startpoint[i]}"));
                converter.Amount++;
            }
            List<string> elements = new List<string>();
            foreach (Converter converter in converters)
            {
                string symbol = converter.Symbol;
                if (!elements.Contains(symbol[0].ToString()))
                    elements.Add(symbol[0].ToString());

                if (!elements.Contains(symbol[1].ToString()))
                    elements.Add(symbol[1].ToString());

            }


            for (int i = 0; i < 40; i++)
            {
                List<Converter> newlist = new List<Converter>();
                foreach(Converter converter in converters)
                {
                    List<Converter> relatives = converters.FindAll(item => converter.Relatives.Contains(item.Symbol));
                    foreach(Converter relative in relatives)
                    {
                        var clone = newlist.Find(item => item.Symbol.Equals(relative.Symbol));
                        if(clone == null)
                        {
                            clone = new Converter(relative.Symbol, relative.MiddleCharacter);
                            clone.Amount = converter.Amount;
                            newlist.Add(clone);
                        } else
                        {
                            clone.Amount += converter.Amount;
                        }

                    }
                }
                converters = newlist;            
            }
            foreach (string element in elements)
            {
                var foundconverters = converters.Where(item => item.Symbol.Contains(element)).ToList();
                decimal amount = 0;
                foreach (Converter converter in foundconverters)
                {
                    if (converter.Symbol.Any(character => !character.ToString().Equals(element)))
                    {
                        amount += converter.Amount;
                    }
                    else
                    {
                        amount += converter.Amount * 2;
                    }
                }
                if (element.Equals(firstcharacter) || element.Equals(lastcharacter))
                    amount = ((amount - 1) / 2) + 1;
                else
                    amount /= 2;
                Console.WriteLine($"{element}: {amount}");
            }
        }
    }
}
