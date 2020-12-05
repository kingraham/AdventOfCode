using System;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Problem2
    {
        public static void Part1()
        {
            var comp = Helpers.GetInput()[0].Split(",").Select(v => Convert.ToInt32(v)).ToArray();
            comp[1] = 12;
            comp[2] = 2;

            int pc = 0;
            int operand1 = 0;
            int operand2 = 0;
            int operand3 = 0;

            while (true)
            {
                int curOpCode = comp[pc];
                if (curOpCode != 99)
                {
                    operand1 = comp[pc + 1];
                    operand2 = comp[pc + 2];
                    operand3 = comp[pc + 3];

                    if (curOpCode == 1)
                    {
                        comp[operand3] = comp[operand1] + comp[operand2];
                    }
                    else if (curOpCode == 2)
                    {
                        comp[operand3] = comp[operand1] * comp[operand2];
                    }
                }
                else
                    break;

                pc += 4;
            }
            Console.WriteLine(comp[0]);
        }

        public static void Part2()
        {
            var source = Helpers.GetInput()[0].Split(",").Select(v => Convert.ToInt32(v)).ToArray();

            for (int noun = 0; noun <= 99; noun++)
            {
                if (noun + 1 % 4 == 0) // not on an instruction.
                    continue;

                for (int verb = 0; verb <= 99; verb++)
                {
                    if (verb + 1 % 4 == 0) // not on an instruction.
                        continue;

                    var comp = source.ToArray();
                    comp[1] = noun;
                    comp[2] = verb;

                    int pc = 0;
                    int operand1 = 0;
                    int operand2 = 0;
                    int operand3 = 0;

                    while (true)
                    {
                        int curOpCode = comp[pc];
                        if (curOpCode != 99)
                        {
                            operand1 = comp[pc + 1];
                            operand2 = comp[pc + 2];
                            operand3 = comp[pc + 3];

                            if (curOpCode == 1)
                            {
                                comp[operand3] = comp[operand1] + comp[operand2];
                            }
                            else if (curOpCode == 2)
                            {
                                comp[operand3] = comp[operand1] * comp[operand2];
                            }
                        }
                        else
                            break;

                        pc += 4;
                    }

                    if (comp[0] == 19690720)
                    {
                        Console.WriteLine($"noun: {noun} verb: {verb} answer: {100 * noun + verb}");
                    }
                }
            }
        }
    }
}