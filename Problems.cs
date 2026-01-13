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

        public bool IsPalindrome(int num)
        {
            if (num < 0) return false;

            int original = num; int reversed = 0;
            if (num < 0 || (num % 10 == 0 && num != 0))
                return false;

            while (num > reversed)
            {
                int digit = num % 10;
                reversed = reversed * 10 + digit;
                num /= 10;
            }
            return (num == reversed || num == reversed / 10);
        }

        public bool IsMatch(string s, string p)
        {
            bool[,] dp = new bool[s.Length + 1, p.Length + 1];
            dp[0, 0] = true;

            for (int j = 1; j <= p.Length; j++)
            {
                if (p[j - 1] == '*')
                {
                    dp[0, j] = dp[0, j - 2];
                }
            }

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= p.Length; j++)
                {
                    if (p[j - 1] == '.' || p[j - 1] == s[i - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j - 1] == '*')
                    {
                        dp[i, j] = dp[i, j - 2] || ((p[j - 2] == '.' || p[j - 2] == s[i - 1]) && dp[i - 1, j]);
                    }
                }
            }

            return dp[s.Length, p.Length];
        }

        public int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            int maxArea = 0;

            while (left < right)
            {
                int currentArea = Math.Min(height[left], height[right]) * (right - left);
                maxArea = Math.Max(maxArea, currentArea);
                if (height[left] < height[right])
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return maxArea;
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0) return "";
            System.String ss = strs[0];
            for (int i = 1; i < strs.Length; i++)
            {
                while (!strs[i].StartsWith(ss))
                {
                    ss = ss.Substring(0, ss.Length - 1);
                    if (ss == "") return "";
                }
            }
            return ss;
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                int left = i + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });
                        while (left < right && nums[left] == nums[left + 1]) left++;
                        while (left < right && nums[right] == nums[right - 1]) right--;
                        left++;
                        right--;
                    }
                    else if (sum < 0)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }
            return result;
        }

        public IList<IList<int>> FourSum(int[] nums, int target)
        {

            Array.Sort(nums);
            IList<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                for (int j = i + 1; j < nums.Length - 2; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1]) continue;
                    int left = j + 1;
                    int right = nums.Length - 1;
                    while (left < right)
                    {
                        long sum = (long)nums[i] + nums[j] + nums[left] + nums[right];
                        if (sum == target)
                        {
                            result.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                            while (left < right && nums[left] == nums[left + 1]) left++;
                            while (left < right && nums[right] == nums[right - 1]) right--;
                            left++;
                            right--;
                        }
                        else if (sum < target)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }
                }
            }
            return result;
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode(0);
            dummy.next = head;

            ListNode fast = dummy;
            ListNode slow = dummy;

            for (int i = 0; i < n; i++)
            {
                fast = fast.next;
            }

            while (fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
            }

            slow.next = slow.next.next;

            return dummy.next;
        }

        public bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            Dictionary<char, char> map = new Dictionary<char, char>
            {
                { ')', '(' },
                { '}', '{' },
                { ']', '[' }
            };
            foreach (char c in s)
            {
                if (map.ContainsKey(c))
                {
                    char topElement = stack.Count == 0 ? '#' : stack.Pop();
                    if (topElement != map[c])
                    {
                        return false;
                    }
                }
                else
                {
                    stack.Push(c);
                }
            }
            return stack.Count == 0;
        }

        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            ListNode dummy = new ListNode(0);
            ListNode tail = dummy;

            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    tail.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    tail.next = list2;
                    list2 = list2.next;
                }
                tail = tail.next;
            }
            if (list1 != null) tail.next = list1;
            if (list2 != null) tail.next = list2;

            return dummy.next;
        }

        public IList<string> GenerateParenthesis(int n)
        {
            IList<string> result = new List<string>();
            void Backtrack(string current, int open, int close)
            {
                if (current.Length == n * 2)
                {
                    result.Add(current);
                    return;
                }
                if (open < n)
                {
                    Backtrack(current + "(", open + 1, close);
                }
                if (close < open)
                {
                    Backtrack(current + ")", open, close + 1);
                }
            }
            Backtrack("", 0, 0);
            return result;
        }
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0) return null;

            Dictionary<int, int> dic = new Dictionary<int, int>();

            foreach (ListNode l in lists) {
                ListNode current = l;
                while (current != null)
                {
                    if (!dic.ContainsKey(current.val))
                    {
                        dic[current.val] = 1;
                    }
                    else
                    {
                        dic[current.val]++;
                    }
                    current = current.next;
                }
            }
            var sortedKeys = dic.Keys.ToList();
            sortedKeys.Sort();

            ListNode dummy = new ListNode(0);
            ListNode tail = dummy;
            foreach (ListNode l in lists) {
                tail = l;

        }


    #endregion

    //Tip 0ms runtime display =)))))))

    //AppDomain.CurrentDomain.ProcessExit += (s, e) =>
    //    File.WriteAllText("display_runtime.txt", "00000");
    #region Private Function

    #endregion
}

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}


