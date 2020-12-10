using System;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Problem9
    {
        public static long Part1()
        {
            int preamble = 25;

            var values = Helpers.GetInput().Select(a => Convert.ToInt64(a)).ToArray();

            for (int i = preamble; i < values.Length; i++) // numbers we're considering.
            {
                bool numGood = false;
                for (int window = i - preamble; window < i; window++)
                {
                    for (int window2 = window + 1; window2 < i; window2++)
                    {
                        var num1 = values[window];
                        var num2 = values[window2];

                        if (values[window] + values[window2] == values[i])
                        {
                            numGood = true;
                            break;
                        }
                    }
                    if (numGood)
                        break;
                }
                if (!numGood)
                {
                    Console.WriteLine(values[i]);
                    return values[i];
                }
            }
            return 0;
        }

        public static void Part2()
        {
            long target = Part1();

            var values = Helpers.GetInput().Select(a => Convert.ToInt64(a)).ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                long sum = 0;
                for (int j = i; j < values.Length; j++)
                {
                    sum += values[j];
                    if (sum == target)
                    {
                        var vals = values.Skip(i).Take(j - i + 1);
                        Console.WriteLine($"Smallest: {vals.Min()}, Largest: {vals.Max()}, Sum: {vals.Sum()}, Answer: {vals.Min() + vals.Max()}");
                        return;
                    }
                    else if (sum > target)
                        break;
                }
            }
        }
    }
}