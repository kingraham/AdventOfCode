using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode._2019
{
    public class Problem3
    {
        public static void Part1()
        {
            Point orig = new Point(0, 0);
            HashSet<Point> line1Path = new HashSet<Point>();
            int minManhattanDistance = int.MaxValue;

            void Navigate(string dir, bool isLine1 = true)
            {
                int dx = 0;
                int dy = 0;

                if (dir[0] == 'R')
                    dx = 1;
                if (dir[0] == 'L')
                    dx = -1;
                if (dir[0] == 'U')
                    dy = -1;
                if (dir[0] == 'D')
                    dy = 1;

                dir = dir.Substring(1);

                for (int steps = 1; steps <= Convert.ToInt32(dir); steps++)
                {
                    orig.X += dx;
                    orig.Y += dy;

                    if (isLine1 && !line1Path.Contains(orig))
                        line1Path.Add(new Point(orig.X, orig.Y));
                    else if (!isLine1 && line1Path.Contains(orig))
                    {
                        int curManhattanDistance = Math.Abs(orig.X) + Math.Abs(orig.Y);

                        if (curManhattanDistance < minManhattanDistance)
                            minManhattanDistance = curManhattanDistance;
                    }
                }
            }


            var input = Helpers.GetInput();
            var line1 = input[0];
            var line2 = input[1];

            foreach (var directions in line1.Split(","))
                Navigate(directions);

            orig = new Point(0, 0);
            foreach (var directions in line2.Split(","))
                Navigate(directions, false);

            Console.WriteLine(minManhattanDistance);
        }

        public static void Part2()
        {
            Point orig = new Point(0, 0);
            Dictionary<Point, int> line1Path = new Dictionary<Point, int>();
            int minSteps = int.MaxValue;
            int curStep = 0;

            void Navigate(string dir, bool isLine1 = true)
            {
                int dx = 0;
                int dy = 0;

                if (dir[0] == 'R')
                    dx = 1;
                if (dir[0] == 'L')
                    dx = -1;
                if (dir[0] == 'U')
                    dy = -1;
                if (dir[0] == 'D')
                    dy = 1;

                dir = dir.Substring(1);

                for (int steps = 1; steps <= Convert.ToInt32(dir); steps++)
                {
                    orig.X += dx;
                    orig.Y += dy;
                    curStep++;

                    if (isLine1 && !line1Path.ContainsKey(orig))
                        line1Path.Add(new Point(orig.X, orig.Y), curStep);
                    else if (!isLine1 && line1Path.ContainsKey(orig))
                    {
                        var curSteps = curStep + line1Path[orig];
                        if (curSteps < minSteps)
                            minSteps = curSteps;
                    }
                }                
            }

            var input = Helpers.GetInput();
            var line1 = input[0];
            var line2 = input[1];

            foreach (var directions in line1.Split(","))
                Navigate(directions);

            curStep = 0;
            orig = new Point(0, 0);
            foreach (var directions in line2.Split(","))
                Navigate(directions, false);

            Console.WriteLine(minSteps);
        }
    }
}