using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class Problem6
    {
        public static void Part1()
        {
            var lights = new bool[1000, 1000];

            void TurnOn(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] = true;
                    }
                }
            }

            void TurnOff(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] = false;
                    }
                }
            }

            void Toggle(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] = !lights[i, j];
                    }
                }
            }
            
            foreach (var instruction in Helpers.GetInput())
            {
                bool turnOn = false;
                bool toggle = false;
                bool turnOff = false;

                var copyInstruction = instruction;
                if (copyInstruction.StartsWith("turn on"))
                    turnOn = true;
                else if (copyInstruction.StartsWith("toggle"))
                    toggle = true;
                else if (copyInstruction.StartsWith("turn off"))
                    turnOff = true;

                var match = Regex.Match(copyInstruction, @"([\d]+,[\d]+) through ([\d]+,[\d]+)");
                var coords1 = match.Groups[1].Value.Split(',').Select(a => Convert.ToInt32(a)).ToArray();
                var coords2 = match.Groups[2].Value.Split(',').Select(a => Convert.ToInt32(a)).ToArray();

                if (turnOn)
                    TurnOn(coords1[0], coords1[1], coords2[0], coords2[1]);
                if (toggle)
                    Toggle(coords1[0], coords1[1], coords2[0], coords2[1]);
                if (turnOff)
                    TurnOff(coords1[0], coords1[1], coords2[0], coords2[1]);
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    count += lights[i, j] ? 1 : 0;
                }
            }
            Console.WriteLine(count);
        }

        public static void Part2()
        {
            var lights = new int[1000, 1000];

            void TurnOn(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] += 1;
                    }
                }
            }

            void TurnOff(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] -= 1;
                        if (lights[i, j] < 0)
                            lights[i, j] = 0;
                    }
                }
            }

            void Toggle(int x, int y, int x2, int y2)
            {
                for (int i = x; i <= x2; i++)
                {
                    for (int j = y; j <= y2; j++)
                    {
                        lights[i, j] += 2;
                    }
                }
            }

            foreach (var instruction in Helpers.GetInput())
            {
                bool turnOn = false;
                bool toggle = false;
                bool turnOff = false;

                var copyInstruction = instruction;
                if (copyInstruction.StartsWith("turn on"))
                    turnOn = true;
                else if (copyInstruction.StartsWith("toggle"))
                    toggle = true;
                else if (copyInstruction.StartsWith("turn off"))
                    turnOff = true;

                var match = Regex.Match(copyInstruction, @"([\d]+,[\d]+) through ([\d]+,[\d]+)");
                var coords1 = match.Groups[1].Value.Split(',').Select(a => Convert.ToInt32(a)).ToArray();
                var coords2 = match.Groups[2].Value.Split(',').Select(a => Convert.ToInt32(a)).ToArray();

                if (turnOn)
                    TurnOn(coords1[0], coords1[1], coords2[0], coords2[1]);
                if (toggle)
                    Toggle(coords1[0], coords1[1], coords2[0], coords2[1]);
                if (turnOff)
                    TurnOff(coords1[0], coords1[1], coords2[0], coords2[1]);
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    count += lights[i, j];
                }
            }
            Console.WriteLine(count);
        }
    }
}