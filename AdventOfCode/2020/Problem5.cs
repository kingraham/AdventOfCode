using System;
using System.Linq;

namespace AdventOfCode._2020
{
    /// <summary>
    /// This problem is basically a binary number problem.  Part 2 just orders an array and finds the missing number between indexes 1 and last - 1.
    /// </summary>
    public class Problem5
    {
        private static (int Row, int Column, int SeatId) ParseSeat(string val)
        {
            // more obvious option is to just calculate it in line or build bits and shift
            var row = val.Substring(0, 7);
            row = row.Replace('B', '1').Replace('F', '0');

            var seat = val.Substring(7);
            seat = seat.Replace('R', '1').Replace('L', '0');

            var iRow = Convert.ToInt32(row, 2);
            var iSeat = Convert.ToInt32(seat, 2);

            return (iRow, iSeat, iRow * 8 + iSeat);
        }

        public static void Part1()
        {
            Console.WriteLine(Helpers.GetInput().Max(a => ParseSeat(a).SeatId));
        }

        public static void Part2()
        {
            var set = Helpers.GetInput().Select(a => ParseSeat(a).SeatId).OrderBy(a => a).ToArray();
            for (int i = 1; i < set.Length - 1; i++)
            {
                if (set[i] != set[i + 1] - 1)
                {
                    Console.WriteLine(set[i] + 1);
                    break;
                }
            }
        }
    }
}