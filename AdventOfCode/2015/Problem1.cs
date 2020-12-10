using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class Problem1
    {
        public static void Part1()
        {
            var instructions = Helpers.GetInput()[0];
            Console.WriteLine(instructions.Count(a => a == '(') - instructions.Count(a => a == ')'));
        }

        public static void Part2()
        {
            var instructions = Helpers.GetInput()[0];
            int floor = 0;

            for (int i = 0; i < instructions.Length; i++)
            {
                var ins = instructions[i];
                if (ins == '(')
                    floor++;
                else
                    floor--;

                if (floor < 0)
                {
                    Console.WriteLine(i + 1);
                    return;
                }
            }
        }
    }
}
