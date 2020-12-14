using System;
using System.Linq;

namespace AdventOfCode._2015
{
    public class Problem2
    {
        public static void Part1()
        {
            var dimensionsList = Helpers.GetInput();
            long total = 0;

            foreach (var dimensions in dimensionsList)
            {
                var split = dimensions.Split('x').Select(a => Convert.ToInt32(a)).ToArray();
                int L = split[0];
                int W = split[1];
                int H = split[2];

                int area1 = L * W;
                int area2 = L * H;
                int area3 = W * H;

                int area = 2 * L * W + 2 * W * H + 2 * H * L;
                total += area + Math.Min(area3, Math.Min(area1, area2));
            }

            Console.WriteLine(total);
        }

        public static void Part2()
        {
            var dimensionsList = Helpers.GetInput();
            long total = 0;

            foreach (var dimensions in dimensionsList)
            {
                var split = dimensions.Split('x').Select(a => Convert.ToInt32(a)).ToArray();
                int L = split[0];
                int W = split[1];
                int H = split[2];

                var s = new[] { L, W, H }.OrderBy(a => a).Take(2).ToArray();

                int length = s[0] + s[0] + s[1] + s[1] + L * W * H;

                total += length;
            }

            Console.WriteLine(total);
        }
    }
}