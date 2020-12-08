using System;

namespace AdventOfCode._2020
{
    public class Problem8
    {
        public class CPU
        {
            private bool[] _instructionExecuted;
            private string[] _memory;
            private int PC = 0;
            public bool InfiniteLoopDetected { get; private set; } = false;

            public int ACC { get; set; } = 0;

            private int ParseParameter(string value)
            {
                int val = 1;
                value = value
                    .Replace("nop", string.Empty)
                    .Replace("acc", string.Empty)
                    .Replace("jmp", string.Empty)
                    .Trim();

                if (value.StartsWith("-"))
                    val = -1;

                return val * Convert.ToInt32(value.Substring(1));
            }

            public CPU(string[] memory)
            {
                _memory = memory;
                _instructionExecuted = new bool[memory.Length];
            }

            public void Execute()
            {
                while (PC < _instructionExecuted.Length && !_instructionExecuted[PC])
                {
                    _instructionExecuted[PC] = true;

                    if (_memory[PC].StartsWith("acc"))
                    {
                        ACC += ParseParameter(_memory[PC]);
                    }
                    else if (_memory[PC].StartsWith("jmp"))
                    {
                        PC += ParseParameter(_memory[PC]);
                        continue;
                    }
                    PC += 1;
                }
                InfiniteLoopDetected = PC < _instructionExecuted.Length;
            }
        }

        public static void Part1()
        {
            CPU c = new CPU(Helpers.GetInput());
            c.Execute();
            Console.WriteLine(c.ACC);
        }

        public static void Part2()
        {
            var arr = Helpers.GetInput();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].StartsWith("nop"))
                {
                    arr[i] = arr[i].Replace("nop", "jmp");

                    CPU c = new CPU(arr);
                    c.Execute();
                    if (!c.InfiniteLoopDetected)
                    {
                        Console.WriteLine(c.ACC);
                        return;
                    }

                    arr[i] = arr[i].Replace("jmp", "nop");
                }
                else if (arr[i].StartsWith("jmp"))
                {
                    arr[i] = arr[i].Replace("jmp", "nop");

                    CPU c = new CPU(arr);
                    c.Execute();
                    if (!c.InfiniteLoopDetected)
                    {
                        Console.WriteLine(c.ACC);
                        return;
                    }

                    arr[i] = arr[i].Replace("nop", "jmp");
                }
            }
        }
    }
}