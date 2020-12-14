using System;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
    public class Problem12
    {
        public static (int x, int y) EAST = (1, 0);
        public static (int x, int y) WEST = (-1, 0);
        public static (int x, int y) NORTH = (0, -1);
        public static (int x, int y) SOUTH = (0, 1);

        public static int EAST_DEG = 90;
        public static int WEST_DEG = 270;
        public static int NORTH_DEG = 0;
        public static int SOUTH_DEG = 180;

        public static Dictionary<int, (int x, int y)> DEGREES_TO_VELOCITY = new Dictionary<int, (int x, int y)>() { { EAST_DEG, EAST }, { WEST_DEG, WEST }, { NORTH_DEG, NORTH }, { SOUTH_DEG, SOUTH } };

        public static void Part1()
        {
            var direction = 90;
            var facing = 90;
            var x = 0;
            var y = 0;

            foreach (var inst in Helpers.GetInput())
            {
                var comm = inst[0];
                var moves = Convert.ToInt32(inst.Substring(1));
                bool move = false;

                if (comm == 'N')
                {
                    direction = NORTH_DEG;
                    move = true;
                }
                else if (comm == 'S')
                {
                    direction = SOUTH_DEG;
                    move = true;
                }
                else if (comm == 'E')
                {
                    direction = EAST_DEG;
                    move = true;
                }
                else if (comm == 'W')
                {
                    direction = WEST_DEG;
                    move = true;
                }
                else if (comm == 'L')
                    facing -= moves;
                else if (comm == 'R')
                    facing += moves;
                else if (comm == 'F')
                {
                    move = true;
                    direction = facing;
                }

                if (direction < 0)
                    direction += 360;
                else if (direction > 360)
                    direction -= 360;

                if (facing < 0)
                    facing += 360;
                else if (facing >= 360)
                    facing = facing % 360;

                if (move)
                {
                    var velocity = DEGREES_TO_VELOCITY[direction];
                    x += moves * velocity.x;
                    y += moves * velocity.y;
                }
            }
            Console.WriteLine($"x: {x}, y: {y}");
        }

        public static void Part2()
        {
            var direction = 90;            
            var x = 0;
            var y = 0;
            var waypointX = 10;
            var waypointY = -1;

            (int x, int y) rotateDegrees(int degrees, int x, int y, bool counterClockwise)
            {                
                for (;degrees > 0; degrees-=90)
                {
                    int tempX = x;
                    int tempY = y;

                    if (!counterClockwise)
                    {                        
                        x = tempY;
                        y = -tempX;
                    }
                    else
                    {
                        x = -tempY;
                        y = tempX;
                    }
                }
                return (x, y);
            }

            foreach (var inst in Helpers.GetInput())
            {                
                var comm = inst[0];
                var moves = Convert.ToInt32(inst.Substring(1));
                bool moveWaypoint = false;

                if (comm == 'N')
                {
                    direction = NORTH_DEG;
                    moveWaypoint = true;
                }
                else if (comm == 'S')
                {
                    direction = SOUTH_DEG;
                    moveWaypoint = true;
                }
                else if (comm == 'E')
                {
                    direction = EAST_DEG;
                    moveWaypoint = true;
                }
                else if (comm == 'W')
                {
                    direction = WEST_DEG;
                    moveWaypoint = true;
                }
                else if (comm == 'L')
                {
                    var res = rotateDegrees(moves, waypointX, waypointY, false);
                    waypointX = res.x;
                    waypointY = res.y;
                }
                else if (comm == 'R')
                {
                    var res = rotateDegrees(moves, waypointX, waypointY, true);
                    waypointX = res.x;
                    waypointY = res.y;
                }
                else if (comm == 'F')
                {                                        
                    for (int i = 1; i <= moves; i++)
                    {
                        x += waypointX;
                        y += waypointY;
                    }                    
                }

                if (direction < 0)
                    direction += 360;
                else if (direction > 360)
                    direction -= 360;
               
                if (moveWaypoint)
                {
                    var velocity = DEGREES_TO_VELOCITY[direction];
                    waypointX += moves * velocity.x;
                    waypointY += moves * velocity.y;
                }                
            }
            Console.WriteLine($"x: {x}, y: {y}, waypointX: {waypointX}, waypointY: {waypointY}, Answer: {Math.Abs(x) + Math.Abs(y)}");
        }
    }
}