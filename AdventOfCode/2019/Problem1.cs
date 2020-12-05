using System;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Problem1
    {
        public static void Part1()
        {
            Console.WriteLine(Helpers.GetInput().Select(a => Math.Floor(Convert.ToInt32(a) / 3m) - 2).Sum());
        }

        public static void Part2()
        {
            decimal Calculate(decimal num)
            {
                if (num <= 0)
                    return 0;

                var val = Math.Floor(num / 3m) - 2;                
                return Math.Max(0, val) + Calculate(val);
            }            
            Console.WriteLine(Helpers.GetInput().Select(a => Calculate(Convert.ToDecimal(a))).Sum());
        }
    }
}
