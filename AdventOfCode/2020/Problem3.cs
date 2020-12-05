using System;

namespace AdventOfCode._2020
{
    public class Problem3
    {
        public static void Part1()
        {
            int pos = 0, tree = 0;

            foreach (var line in Helpers.GetInput())
            {
                if (line[pos] == '#')
                    tree++;
                pos = (pos + 3) % line.Length;
            }
            Console.WriteLine(tree);
        }

        public static void Part2()
        {
            var lines = Helpers.GetInput();
            int pos = 0;
            long tot = 1;
            int tree = 0;

            var slopes = new[] {
                ( Right: 1, Down: 1 ),
                ( Right: 3, Down: 1 ),
                ( Right: 5, Down: 1 ),
                ( Right: 7, Down: 1 ),
                ( Right: 1, Down: 2 )
            };

            foreach (var slope in slopes)
            {
                for (int y = 0; y < lines.Length; y += slope.Down)
                {
                    if (lines[y][pos] == '#')
                        tree++;
                    pos = (pos + slope.Right) % lines[y].Length;
                }
                tot *= tree;
                tree = 0;
                pos = 0;
            }
            Console.WriteLine(tot);
        }
    }
}