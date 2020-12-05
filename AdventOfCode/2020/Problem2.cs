using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Problem2
    {
        public static void Part1()
        {
            int matches = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var m = Regex.Match(line, @"(\d+)-(\d+) (.):(.+)");

                int min = Convert.ToInt32(m.Groups[1].Value);
                int max = Convert.ToInt32(m.Groups[2].Value);
                char c = m.Groups[3].Value[0];
                string password = m.Groups[4].Value;
                var count = password.Where(a => a == c).Count();
                if (count >= min && count <= max)
                    matches++;          
            }
            Console.WriteLine(matches);
        }

        public static void Part2()
        {
            int matches = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var m = Regex.Match(line, @"(\d+)-(\d+) (.):(.+)");

                int pos1 = Convert.ToInt32(m.Groups[1].Value);
                int pos2 = Convert.ToInt32(m.Groups[2].Value);
                char c = m.Groups[3].Value[0];
                string password = m.Groups[4].Value;

                if (password[pos1] == c ^ password[pos2] == c)
                    matches++;
               
            }
            Console.WriteLine(matches);
        }
    }
}