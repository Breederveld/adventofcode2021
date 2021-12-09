using System;
using System.Collections.Generic;
using System.Linq;

namespace codeavent8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = Input.CodeStrings().ToList();
            int total = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                Dictionary<int, char> sections = new Dictionary<int, char>
                {
                    {0,' '},
                    {1,' '},
                    {2,' '},
                    {3,' '},
                    {4,' '},
                    {5,' '},
                    {6,' '},
                };
                Dictionary<int, string> digits = new Dictionary<int, string>
                {
                    {0,""},
                    {1,""},
                    {2,""},
                    {3,""},
                    {4,""},
                    {5,""},
                    {6,""},
                    {7,""},
                    {8,""},
                    {9,""},
                };
                var obviousdigits = line.Inputs.Where(item => item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7).ToList(); // 1 4 7 8
                var sixnumberdigits = line.Inputs.Where(item => item.Length == 6).ToList(); // 0 6 9
                var fivenumberdigits = line.Inputs.Where(item => item.Length == 5).ToList(); // 2 3 5

                obviousdigits.ForEach(item =>
                {
                    if (item.Length == 2)
                        digits[1] = item;
                    if (item.Length == 3)
                        digits[7] = item;
                    if (item.Length == 4)
                        digits[4] = item;
                    if (item.Length == 7)
                        digits[8] = item;
                });

                sections[0] = CharNotInNumber(digits[7], digits[1]);

                var sectionsnotin1butin4 = CharsNotInNumber(digits[4],digits[1]);

                digits[0] = FindDigitNotWithChars(sixnumberdigits, sectionsnotin1butin4);
                sections[3] = CharNotInNumber(digits[8], digits[0]);
                digits[6] = FindDigitNotWithChars(sixnumberdigits, digits[1].ToCharArray().ToList());
                sixnumberdigits.Remove(digits[0]);
                sixnumberdigits.Remove(digits[6]);
                digits[9] = sixnumberdigits[0];
                sections[2] = CharNotInNumber(digits[8], digits[6]);
                sections[4] = CharNotInNumber(digits[8], digits[9]);
                sections[5] = CharNotInNumber(digits[1], sections[2].ToString());

                digits[5] = FindDigitNotWithChars(fivenumberdigits, new List<char> { sections[2] });
                fivenumberdigits.Remove(digits[5]);

                digits[2] = FindDigitNotWithChars(fivenumberdigits, new List<char> { sections[5] });
                fivenumberdigits.Remove(digits[2]);

                digits[3] = fivenumberdigits[0];
                sections[1] = CharNotInNumber(digits[4], string.Join("", new List<char> { sections[2], sections[3], sections[5] }));
                sections[6] = CharNotInNumber(digits[8], string.Join("", new List<char> { sections[0], sections[1], sections[2], sections[3], sections[4], sections[5] }));
                var found = GetDuplicaties(sections);
                var found2 = GetDuplicaties(digits);

                if (!ValidateNumbers(digits,sections) || found || found2 || digits.Where(item => string.IsNullOrWhiteSpace(item.Value)).Count() > 0 || sections.Where(item => item.Value.Equals(' ')).Count() > 0)
                { 
                    throw new Exception("njoh");
                }
                var jan = OutputToNumber(digits, line.Outputs.ToList());
                total += jan;
            }

            Console.WriteLine(total);
        }

        public static char CharNotInNumber(string digit, string filterdigit)
        {
            List<char> characters = new List<char>();
            foreach(char character in digit)
            {
                if (!filterdigit.Contains(character))
                {
                    characters.Add(character);
                }
            }

            return characters[0];
        }

        public static List<char> CharsNotInNumber(string digit, string filterdigit)
        {
            List<char> foundcharacters = new List<char>();
            foreach (char character in digit)
            {
                if (!filterdigit.Contains(character))
                {
                    foundcharacters.Add(character);
                }
            }
            return foundcharacters;
        }

        public static string FindDigitNotWithChars(List<string> digits, List<char> characters)
        {
            List<string> founddigits = new List<string>();
            foreach(string digit in digits)
            {
                foreach(char character in characters)
                {
                    if (!digit.Contains(character))
                    {
                        founddigits.Add(digit);
                    }
                }
            }
            return founddigits[0];
        }

        public static int OutputToNumber(Dictionary<int, string> digits, List<string> outputs)
        {
            List<int> numbers = new List<int>();
            for(int i = 0; i < outputs.Count; i++)
            {
                var outputnumber = outputs[i].Select(item => Convert.ToInt32(item)).Sum();
                foreach (KeyValuePair<int, string> entry in digits)
                {
                    var value = entry.Value.Select(item => Convert.ToInt32(item)).Sum(); 
                    if (value == outputnumber)
                    {
                        numbers.Add(entry.Key);
                        break;
                    }
                }
            }
            int result = Convert.ToInt32($"{numbers[0]}{numbers[1]}{numbers[2]}{numbers[3]}");
            return result;
        }

        public static bool GetDuplicaties(Dictionary<int, string> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                for (int b = 0; b < dict.Count; b++)
                {
                    if (i != b && dict[i] == dict[b])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool GetDuplicaties(Dictionary<int, char> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                for (int b = 0; b < dict.Count; b++)
                {
                    if (i != b && dict[i] == dict[b])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ValidateNumbers(Dictionary<int, string> digits, Dictionary<int, char> sections)
        {
            bool linecorrect = true;
            foreach(var line in digits)
            {
                if(line.Key == 0)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 3).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 1)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key == 2 || item.Key == 5).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 2)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 1 && item.Key != 5).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 3)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 1 && item.Key != 4).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 4)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key == 1 || item.Key == 2 || item.Key == 3 || item.Key == 5).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 5)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 2 && item.Key != 4).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 6)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 2).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 7)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key == 0 || item.Key == 2 || item.Key == 5).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if (line.Key == 8)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
                if(line.Key == 9)
                {
                    linecorrect = line.Value.Select(item => Convert.ToInt32(item)).Sum() == sections.Where(item => item.Key != 4).Select(item => Convert.ToInt32(item.Value)).Sum();
                    continue;
                }
            }
            return true;
        }
    }
}
