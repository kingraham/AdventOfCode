using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Problem1
    {
        public static void Part1()
        {            
            HashSet<int> numbers = new HashSet<int>();

            Helpers.GetInput().Select(line => Convert.ToInt32(line))
                .Aggregate(new HashSet<int>(), (set, a) =>
                {
                    if (set.Contains(2020 - a))
                        Console.WriteLine(a * (2020 - a));
                    set.Add(a);
                    return set;
                });                      
        }

        // 162292410
        public static void Part2()
        {
            var a = Helpers.GetInput().Select(a => Convert.ToInt32(a)).OrderBy(a => a).ToArray(); // naive solution here.
            for (int i = 0; i < a.Length; i++)
            {                
                for (int j = i + 1; j < a.Length; j++)
                {                    
                    for (int k = j + 1; k < a.Length; k++)
                    {                        
                        if (a[i] + a[j] + a[k] == 2020)
                        {                            
                            Console.WriteLine(a[i] * a[j] * a[k]);
                            return;
                        }
                        else if (a[i] + a[j] + a[k] > 2020)
                            break;
                    }
                }
            }
        }
    }
}