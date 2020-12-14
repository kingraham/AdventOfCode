using System;
using System.Collections.Generic;

namespace AdventOfCode._2015
{
    public class Problem8
    {
        public static void Part1()
        {
            int diff = 0;
            foreach (var h in Helpers.GetInput())
            {
                var s = h.Substring(1, h.Length - 2);
                int ch = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '\\' && (s[i + 1] == '\"' || s[i + 1] == '\\'))
                        i++;
                    else if (s[i] == '\\' && s[i + 1] == 'x')
                        i += 3;

                    ch++;
                }
                diff += h.Length - ch;
            }
            Console.WriteLine(diff);
        }

        public static void Part2()
        {
            int diff = 0;
            foreach (var h in Helpers.GetInput())
            {
                int cur = 2;
                foreach (var c in h)
                {
                    if (c == '\\')
                        cur += 1;
                    if (c == '\"')
                        cur += 1;
                }
                diff += cur;
            }
            Console.WriteLine(diff);
        }
    }
}