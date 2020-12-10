using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015
{
    public class Problem5
    {       
        public static void Part1()
        {
            bool IsNice(string v)
            {
                int vowelCount = 0;
                int last = ' ';
                bool foundDouble = false;

                foreach (var c in v)
                {
                    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                        vowelCount++;
                    if (c == last)
                        foundDouble = true;
                    if ((last == 'a' && c == 'b') ||
                        (last == 'c' && c == 'd') ||
                        (last == 'p' && c == 'q') ||
                        (last == 'x' && c == 'y'))
                    {
                        return false;
                    }
                    last = c;
                }

                return vowelCount >= 3 && foundDouble;
            }

            int isNice = 0;
            foreach (var c in Helpers.GetInput())
            {
                if (IsNice(c))
                    isNice++;
            }
            Console.WriteLine(isNice);
        }

        public static void Part2()
        {
            bool IsNiceRule1(string v)
            {
                for (int i = 0; i < v.Length - 1; i++)
                {
                    for (int j = i + 2; j < v.Length - 1; j++)
                    {
                        if (v[i] == v[j] && v[i + 1] == v[j + 1])
                            return true;
                    }
                }
                return false;
            }

            bool IsNiceRule2(string v)
            {
                for (int i = 0; i < v.Length - 2; i++)
                {
                    if (v[i] == v[i + 2])
                        return true;
                }
                return false;
            }

            int isNice = 0;
            foreach (var c in Helpers.GetInput())
            {
                if (IsNiceRule1(c) && IsNiceRule2(c))
                    isNice++;
            }
            Console.WriteLine(isNice);
        }
    }
}