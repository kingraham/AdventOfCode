using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Problem6
    {       
        public static void Part1()
        {
            HashSet<char> chars = new HashSet<char>();
            int tot = 0;
            foreach (var line in Helpers.GetInput())
            {
                if (string.IsNullOrWhiteSpace(line))
                {                    
                    tot += chars.Count;
                    chars.Clear();
                }
                else
                {
                    foreach (var c in line)                    
                        chars.Add(c);                    
                }
            }
            tot += chars.Count;
            Console.WriteLine(tot);
        }

        public static void Part2()
        {
            IEnumerable<char> set = null;
            int tot = 0;
            foreach (var line in Helpers.GetInput())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    tot += set.Count();
                    set = null;
                }
                else                
                    set = set == null ? line.ToCharArray() : line.ToCharArray().Intersect(set);
            }
            tot += set.Count();
            Console.WriteLine(tot);
        }
    }
}