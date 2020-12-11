using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Problem11
    {
        public const byte EMPTY = 2;
        public const byte OCCUPIED = 1;

        public static void Part1()
        {            
            var input = Helpers.GetInput();

            int maxX = input[0].Length;
            int maxY = input.Length;

            var layout = new byte[maxY, maxX];

            bool change = false;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (input[y][x] == '#')
                        layout[y,x] = OCCUPIED;
                    else if (input[y][x] == 'L')
                        layout[y, x] = EMPTY;
                }
            }

            int occupied = 0;
            do
            {
                change = false;
                occupied = 0;
                
                var newlayout = new byte[maxY, maxX];

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        if (layout[y, x] != 0)
                        {
                            int adj = 0;
                            var curState = layout[y, x];

                            // top row
                            if (y > 0 && x > 0 && layout[y - 1, x - 1] == 1)
                                adj++;
                            if (y > 0 && layout[y - 1, x] == 1)
                                adj++;
                            if (y > 0 && x < maxX - 1 && layout[y - 1, x + 1] == 1)
                                adj++;
                            if (x > 0 && layout[y, x - 1] == 1)
                                adj++;
                            if (x < maxX - 1 && layout[y, x + 1] == 1)
                                adj++;
                            if (y < maxY - 1 && x > 0 && layout[y + 1, x - 1] == 1)
                                adj++;
                            if (y < maxY - 1 && layout[y + 1, x] == 1)
                                adj++;
                            if (y < maxY - 1 && x < maxX - 1 && layout[y + 1, x + 1] == 1)
                                adj++;

                            newlayout[y, x] = layout[y, x];

                            if (adj >= 4)
                                newlayout[y, x] = EMPTY;
                            else if (adj == 0)                            
                                newlayout[y, x] = OCCUPIED;                                                            

                            if (newlayout[y, x] == OCCUPIED)
                                occupied++;

                            var propState = newlayout[y, x];

                            change = change || curState != propState;
                        }
                    }
                }

                layout = newlayout;

            } while (change == true);

            Console.WriteLine(occupied);
        }
        
        public static void Part2()
        {
            var input = Helpers.GetInput();

            int maxX = input[0].Length;
            int maxY = input.Length;

            var layout = new byte[maxY, maxX];

            bool change = false;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (input[y][x] == '#')
                        layout[y, x] = OCCUPIED;
                    else if (input[y][x] == 'L')
                        layout[y, x] = EMPTY;
                }
            }

            int occupied = 0;
            do
            {
                change = false;
                occupied = 0;

                var newlayout = new byte[maxY, maxX];

                bool SeesOccupied(int y, int x, int dy, int dx)
                {
                    while (true)
                    {
                        y += dy;
                        x += dx;

                        if (x < 0 || y < 0 || x >= maxX || y >= maxY)
                            return false;

                        if (layout[y, x] == OCCUPIED)
                            return true;
                        else if (layout[y, x] == EMPTY)
                            return false;
                    }
                }

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                         if (layout[y, x] != 0)
                        {
                            int adj = 0;
                            var curState = layout[y, x];

                            // top row
                            if (SeesOccupied(y, x, -1, -1))
                                adj++;
                            if (SeesOccupied(y, x, -1, 0))
                                adj++;
                            if (SeesOccupied(y, x, -1, 1))
                                adj++;
                            if (SeesOccupied(y, x, 0, -1))
                                adj++;
                            if (SeesOccupied(y, x, 0, 1))
                                adj++;
                            if (SeesOccupied(y, x, 1, -1))
                                adj++;
                            if (SeesOccupied(y, x, 1, 0))
                                adj++;
                            if (SeesOccupied(y, x, 1, 1))
                                adj++;

                            newlayout[y, x] = layout[y, x];

                            if (adj >= 5)
                                newlayout[y, x] = EMPTY;
                            else if (adj == 0)
                                newlayout[y, x] = OCCUPIED;

                            if (newlayout[y, x] == OCCUPIED)
                                occupied++;

                            var propState = newlayout[y, x];

                            change = change || curState != propState;
                        }
                    }
                }

                layout = newlayout;

            } while (change == true);

            Console.WriteLine(occupied);
        }
    }
}