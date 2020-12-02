using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public static class Problem1
    {
        public static void Execute()
        {
            HashSet<int> numbers = new HashSet<int>();
            foreach (var line in File.ReadAllLines("input.txt").Select(line => Convert.ToInt32(line)))
            {
                if (numbers.Contains(2020 - line))
                {
                    Console.WriteLine(line * (2020 - line));
                    break;
                }
                numbers.Add(line);
            }
        }

        public static void ExecuteB()
        {
            var arr = File.ReadAllLines("input.txt").Select(line => Convert.ToInt32(line)).OrderBy(a => a).ToArray(); // naive solution here.
            for (int i = 0; i < arr.Length; i++)
            {
                var first = arr[i];
                for (int j = i + 1; j < arr.Length; j++)
                {
                    var second = arr[j];
                    for (int k = j + 1; k < arr.Length; k++)
                    {
                        var third = arr[k];
                        if (first + second + third == 2020)
                        {
                            Console.WriteLine($"{first} {second} {third}");
                            Console.WriteLine(first * second * third);
                            return;
                        }
                        else if (first + second + third > 2020)
                            break;
                    }
                }
            }
        }
    }
}