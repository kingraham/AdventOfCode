using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public static class Helpers
    {
        public static string[] GetInput() => File.ReadAllLines("input.txt");

        public static int GetDigit(int num, int n)
        {
            var temp = num / (int)Math.Pow(10, n);
            return temp % 10;
        }
    }
}