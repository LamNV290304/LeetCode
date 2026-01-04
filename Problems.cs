using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeetCode
{
    public class Problems
    {
        #region Solution
        public int[] TwoSum(int[] nums, int target)
        {
            //int[] ints = new int[2];
            //int n = nums.Length;
            //int j = 0;
            //for (int i = j + 1; i < n; i++)
            //{

            //    if ((nums[j] + nums[i]) == target)
            //    {
            //        ints[0] = j;
            //        ints[1] = i;
            //        return ints;
            //    }


            //    if (i == n - 1 && j != n)
            //    {
            //        j++;
            //        i = j;
            //    }
            //}
            //return ints;

            var numMap = new Dictionary<int, int>
            {
                [nums[0]] = 0
            };

            for (int i = 1; i < nums.Length; i++)
            {
                if (numMap.TryGetValue(target - nums[i], out int value))
                {
                    return [value, i];
                }
                numMap[nums[i]] = i;
            }
            return [];
        }

        public string LongestPalindrome(string s)
        {
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = s.Length - 1; j > i; j--)
                {
                    if (s[i] == s[j])
                    {
                        int left = i;
                        int right = j;
                        bool isPalindrome = true;
                        while (left < right)
                        {
                            if (s[left] != s[right])
                            {
                                isPalindrome = false;
                                break;
                            }
                            left++;
                            right--;
                        }
                        if (isPalindrome)
                        {
                            string palindrome = s.Substring(i, j - i + 1);
                            if (palindrome.Length > result.Length)
                            {
                                result = palindrome;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public string Convert(string s, int numRows)
        {
            if (s.Length < numRows) return s;

            string result = "";
            Dictionary<int, string> dic = new Dictionary<int, string>();
            bool isUp = true;
            int row = 1;

            for (int i = 0; i < s.Length; i++)
            {

                if (!dic.ContainsKey(row))
                {
                    dic[row] = "";
                }

                dic[row] += s[i];

                if (numRows != 1)
                {
                    if (row == 1)
                    {
                        isUp = true;
                    }
                    else if (row == numRows)
                    {
                        isUp = false;
                    }

                    if (isUp)
                    {
                        row++;
                    }
                    else
                    {
                        row--;
                    }
                }
            }

            for (int i = 1; i <= numRows; i++)
            {
                result += dic[i];
            }

            return result;
        }

        public int Reverse(int x)
        {
            try
            {
                string str = x.ToString();
                Span<char> result = stackalloc char[str.Length];

                if (x < 0)
                {
                    result[0] = '-'; // giữ dấu âm
                    int len = str.Length - 1;
                    for (int i = 1; i < str.Length; i++)
                    {
                        result[i] = str[len - i + 1]; // đảo ngược phần số
                    }
                }
                else
                {
                    int len = str.Length - 1;
                    for (int i = 0; i < str.Length; i++)
                    {
                        result[i] = str[len - i]; // đảo ngược toàn bộ
                    }
                }

                // parse lại, dùng constructor string từ span
                int r = int.Parse(result);
                return r;
            }
            catch
            {
                return 0;
            }
        }

        public int MyAtoi(string s)
        {
            long result = 0;
            int i = 0, sign = 1;
            s = s.Trim();
            if (string.IsNullOrEmpty(s)) return 0;

            if (i < s.Length && (s[i] == '+' || s[i] == '-'))
            {
                sign = (s[i] == '-') ? -1 : 1;
                i++;
            }

            while (i < s.Length && char.IsDigit(s[i]))
            {
                int digit = s[i] - '0';
                result = result * 10 + digit;

                if (result * sign < int.MinValue) return int.MinValue;
                if (result * sign > int.MaxValue) return int.MaxValue;

                i++;
            }

            return (int)result * sign;
        }

        public string SpilitString(string s)
        {
            int len = s.Length;
            for (int i = 0; i < s.Length; i++)
            {
                if ((i + 1) < len - 1)
                {
                    if (s[i] > s[i + 1])
                    {
                        s = s.Remove(i, 1);
                        return s;
                    }
                }
            }

            if (len == s.Length)
            {
                s = s.Remove(s.Length - 1, 1);
            }
            return s;
        }

        public int MatrixRook(int[][] A)
        {
            int n = A.Length;
            int m = A[0].Length;

            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();

            int maxSum = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int currentValue = A[i][j];

                    foreach (var kv in visited)
                    {
                        var (r, c) = kv.Key;
                        int prevValue = kv.Value;

                        if (r != i && c != j)
                        {
                            int sum = currentValue + prevValue;
                            maxSum = Math.Max(maxSum, sum);
                        }
                    }

                    visited[(i, j)] = currentValue;
                }
            }

            return maxSum;
        }

        public int ArrayMove(int[] A)
        {
            if (A.Length > int.MaxValue || A.Length < int.MinValue) return -1;

            int result = 0;

            var duplicate = new SortedSet<int>();
            var miss = new SortedSet<int>();

            var dic = new Dictionary<int, int>();

            for (int i = 0; i < A.Length; i++)
            {
                if (!dic.ContainsKey(A[i]))
                {
                    dic.Add(A[i], 1);
                }
                else
                {
                    duplicate.Add(A[i]);
                }
            }

            for (int i = 1; i <= A.Length; i++)
            {
                if (!dic.ContainsKey(i))
                {
                    miss.Add(i);
                }
            }

            for (int i = 0; i < miss.Count; i++)
            {
                int tmpValue = miss.ElementAt(i) - duplicate.ElementAt(i);
                if (tmpValue > 0)
                {
                    result += tmpValue;
                }
                else
                {
                    result -= tmpValue;
                }
            }

            return result;
        }
        public int LengthOfLongestSubstring(string s)
        {
            Dictionary<char, int> myDictionary = new Dictionary<char, int>();
            int count = 0;
            int count2 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char s2 = s[i];
                if (!myDictionary.ContainsKey(s2))
                {
                    count++;
                    myDictionary.Add(s2, i);
                    if (count2 < count)
                    {
                        count2 = count;
                    }
                }
                else
                {
                    i = myDictionary[s2];
                    myDictionary.Clear();
                    count = 0;
                }
            }

            return count2;
        }

        
        #endregion

        //Tip 0ms runtime display =)))))))

        //AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        //    File.WriteAllText("display_runtime.txt", "00000");
        #region Private Function

        #endregion
    }
}

