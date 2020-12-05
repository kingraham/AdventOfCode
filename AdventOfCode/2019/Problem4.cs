using System;

namespace AdventOfCode._2019
{
    public class Problem4
    {
        private static int GetDigit(int num, int n)
        {
            var temp = num / (int)Math.Pow(10, n);
            return temp % 10;
        }

        public static void Part1()
        {
            int min = 171309;
            int max = 643603;
            int found = 0;

            for (int cur = min; cur <= max; cur++) // there are much more elegant ways to do this, I know.
            {
                int dig1 = GetDigit(cur, 0);
                int dig2 = GetDigit(cur, 1);
                int dig3 = GetDigit(cur, 2);
                int dig4 = GetDigit(cur, 3);
                int dig5 = GetDigit(cur, 4);
                int dig6 = GetDigit(cur, 5);

                if (dig6 > dig5 || dig5 > dig4 || dig4 > dig3 || dig3 > dig2 || dig2 > dig1)
                    continue;

                if (dig1 == dig2 || dig2 == dig3 || dig3 == dig4 || dig4 == dig5 || dig5 == dig6)
                    found++;
            }
            Console.WriteLine(found);
        }

        public static void Part2()
        {
            int min = 171309;
            int max = 643603;
            int found = 0;

            for (int cur = min; cur <= max; cur++) // there are much more elegant ways to do this, I know.
            {
                byte[] vals = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


                int dig1 = GetDigit(cur, 0);
                int dig2 = GetDigit(cur, 1);
                int dig3 = GetDigit(cur, 2);
                int dig4 = GetDigit(cur, 3);
                int dig5 = GetDigit(cur, 4);
                int dig6 = GetDigit(cur, 5);

                if (dig6 > dig5 || dig5 > dig4 || dig4 > dig3 || dig3 > dig2 || dig2 > dig1)
                    continue;

                vals[dig1]++;
                vals[dig2]++;
                vals[dig3]++;
                vals[dig4]++;
                vals[dig5]++;
                vals[dig6]++;

                bool valid = false;
                foreach (var val in vals)
                {
                    if (val == 2)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                    found++;

                Array.Clear(vals, 0, vals.Length);
                
            }
            Console.WriteLine(found);
        }
    }
}