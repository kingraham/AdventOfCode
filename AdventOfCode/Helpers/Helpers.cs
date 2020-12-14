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

        // heap's algorithm for permutations
        public static int[][] GetPermutations(int k, int[] a)
        {
            List<int[]> final = new List<int[]>();
            int n = a.Length;

            int[] d = new int[n];
            Array.Copy(a, d, n);
            final.Add(d);

            int[] c = new int[n];
            int i = 0;

            while (i < n)
            {
                if (c[i] < i)
                {
                    int temp = 0;
                    if (i % 2 == 0)
                    {
                        temp = a[0];
                        a[0] = a[i];
                        a[i] = temp;
                    }
                    else
                    {
                        temp = a[c[i]];
                        a[c[i]] = a[i];
                        a[i] = temp;
                    }

                    d = new int[n];
                    Array.Copy(a, d, n);
                    final.Add(d);

                    c[i] += 1;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i += 1;
                }
            }

            return final.ToArray();
        }
    }
}