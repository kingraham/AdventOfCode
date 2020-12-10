using System;
using System.Collections.Generic;

namespace AdventOfCode._2015
{
    public class Problem3
    {
       

        public static void Part1()
        {
            var houses = new HashSet<(int x, int y)>();

            int x = 0;
            int y = 0;

            houses.Add((x, y));

            string instruction = Helpers.GetInput()[0];
           
            foreach (var c in instruction)
            {
                int nextX = x;
                int nextY = y;

                if (c == '^')
                    nextY += -1;
                else if (c == 'v')
                    nextY += 1;
                else if (c == '>')
                    nextX += 1;
                else if (c == '<')
                    nextX -= 1;
                
                houses.Add((nextX, nextY));
                x = nextX;
                y = nextY;
            }

            Console.WriteLine(houses.Count);
        }

        public static void Part2()
        {
            var houses = new HashSet<(int x, int y)>();

            int x = 0;
            int y = 0;

            int roboX = 0;
            int roboY = 0;

            houses.Add((x, y));

            bool santa = true;

            string instruction = Helpers.GetInput()[0];

            foreach (var c in instruction)
            {
                int nextX = santa ? x : roboX;
                int nextY = santa ? y : roboY;

                if (c == '^')
                    nextY += -1;
                else if (c == 'v')
                    nextY += 1;
                else if (c == '>')
                    nextX += 1;
                else if (c == '<')
                    nextX -= 1;

                houses.Add((nextX, nextY));

                if (santa)
                {
                    x = nextX;
                    y = nextY;
                }
                else
                {
                    roboX = nextX;
                    roboY = nextY;
                }
                santa = !santa;
            }

            Console.WriteLine(houses.Count);
        }
    }
}