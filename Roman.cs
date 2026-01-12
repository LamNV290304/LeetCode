using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    internal class Roman
    {
        public int RomanToInt(string s)
        {

            Dictionary<char, int> map = new Dictionary<char, int>()
            {
                {'I', 1 },
                {'V', 5 },
                {'X', 10 },
                {'L', 50 },
                {'C', 100 },
                {'D', 500 },
                {'M', 1000 }
            };
            int total = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && map[s[i]] < map[s[i + 1]])
                {
                    total -= map[s[i]];
                }
                else
                {
                    total += map[s[i]];
                }
            }
            return total;

        }
        public static string IntToRomanMethod(int num)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<int, string> map = new Dictionary<int, string>()
            {
                {1000, "M"},
                {900, "CM"},
                {500, "D"},
                {400, "CD"},
                {100, "C"},
                {90, "XC"},
                {50, "L"},
                {40, "XL"},
                {10, "X"},
                {9, "IX"},
                {5, "V"},
                {4, "IV"},
                {1, "I"}
            };
            foreach (var item in map)
            {
                while (num >= item.Key)
                {
                    sb.Append(item.Value);
                    num -= item.Key;
                }
            }
            return sb.ToString();
        }

    }
}
