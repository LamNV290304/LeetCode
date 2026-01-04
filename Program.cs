using System.Collections;
using System.Diagnostics;

namespace LeetCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Problems problems = new Problems();


            Stopwatch sw = Stopwatch.StartNew();
            bool s = problems.IsPalindrome(100);
            sw.Stop();
            Console.WriteLine("Result: " + s);
            Console.WriteLine($"Thời gian: {sw.ElapsedMilliseconds} ms");

        }
    }
}
