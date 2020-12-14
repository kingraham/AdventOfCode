using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Problem13
    {        
        public static void Part1()
        {
            var s = Helpers.GetInput();
            int e = Convert.ToInt32(s[0]);
            var list = s[1].Split(",")
                .Where(a => a != "x")
                .Select(a => Convert.ToInt32(a))
                .Select(a => (a, b: a * (1 + Math.Floor(d: e / (double)a))))
                .OrderBy(a => a.b).First();          
            Console.WriteLine((list.b - e) * list.a);            
        }


        public static void Part2()
        {
            // brute force will take forever, brought in the CRT to complete this one.        
            //https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
            long Solve(long[] n, long[] a)
            {
                long prod = n.Aggregate((long)1, (i, j) => i * j), p, sm = 0, i = 0;
                for (; i < n.Length; i++)
                {
                    p = prod / n[i];
                    sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
                }
                return sm % prod;
            }
            long ModularMultiplicativeInverse(long a, long mod)
            {
                long b = a % mod;
                for (long x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)                    
                        return x;                    
                }
                return 1;
            }

            var l = Helpers.GetInput()[1].Split(",")
                .Select(a => a == "x" ? -1 : Convert.ToInt64(a))
                .Select((a, i) => (a, b: i == 0 ? 0 : a - i))
                .Where(a => a.a > 0);                                                    
             Console.WriteLine(Solve(l.Select(a => a.a).ToArray(), l.Select(a => a.b).ToArray()));
        }
    }
}