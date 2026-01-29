using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    internal class Combinations
    {
        public IList<string> LetterCombinations(string digits)
        {
            Dictionary<char, string> phoneMap = new Dictionary<char, string>()
            {
                {'2', "abc"},
                {'3', "def"},
                {'4', "ghi"},
                {'5', "jkl"},
                {'6', "mno"},
                {'7', "pqrs"},
                {'8', "tuv"},
                {'9', "wxyz"}
            };

            List<string> result = new List<string>();

            foreach (char digit in digits)
            {
                if (!phoneMap.ContainsKey(digit))
                    continue;
                string letters = phoneMap[digit];
                if (result.Count == 0)
                {
                    foreach (char letter in letters)
                    {
                        result.Add(letter.ToString());
                    }
                }
                else
                {
                    List<string> tempList = new List<string>();
                    foreach (string combination in result)
                    {
                        foreach (char letter in letters)
                        {
                            tempList.Add(combination + letter);
                        }
                    }
                    result = tempList;
                }
            }

            return result;
        }
    }
}
