using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class Problem7
    {
        public class CPU
        {
            public enum OpCode : int
            {
                Add = 1,
                Mul = 2,
                Input = 3,
                Output = 4,
                JmpT = 5,
                JmpF = 6,
                LT = 7,
                EQ = 8
            }

            public enum Mode
            {
                Position = 0,
                Immediate = 1
            }

            public class Operation
            {
                public OpCode Opcode { get; set; }

                public int Cycles { get; set; } = -1; // or really, how much we advance the program counter when completed.

                public Action<bool[]> Execute { get; set; }
            }

            public Dictionary<int, Operation> Operations = new Dictionary<int, Operation>();

            private int PC = 0;
            private int[] Memory = null;
            private bool Amplified = false;

            public int PhaseSetting { get; set; }
            public int CurrentInput { get; set; }
            public int CurrentOutput { get; private set; }

            private int GetMemory(int value, int idx, bool[] parameterModes)
            {
                return parameterModes[idx] ? value : Memory[value];
            }

            public CPU(int[] instructions)
            {
                Memory = instructions;

                Operations.Add(1, new Operation()
                {
                    Opcode = OpCode.Add,
                    Cycles = 4,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];
                        int operand3 = Memory[PC + 3];

                        Memory[operand3] = (GetMemory(operand1, 0, parameterModes)) +
                                           (GetMemory(operand2, 1, parameterModes));
                    }
                });

                Operations.Add(2, new Operation()
                {
                    Opcode = OpCode.Input,
                    Cycles = 4,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];
                        int operand3 = Memory[PC + 3];

                        Memory[operand3] = (GetMemory(operand1, 0, parameterModes)) *
                                           (GetMemory(operand2, 1, parameterModes));
                    }
                });

                Operations.Add(3, new Operation()
                {
                    Opcode = OpCode.Input,
                    Cycles = 2,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];                        
                        Memory[operand1] = !Amplified ? PhaseSetting : CurrentInput;
                        Amplified = true;
                    }
                });

                Operations.Add(4, new Operation()
                {
                    Opcode = OpCode.Output,
                    Cycles = 2,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        CurrentOutput = GetMemory(operand1, 0, parameterModes);
                    }
                });

                Operations.Add(5, new Operation()
                {
                    Opcode = OpCode.JmpT,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];

                        var value = GetMemory(operand1, 0, parameterModes);
                        var pos = GetMemory(operand2, 1, parameterModes);

                        if (value != 0)
                            PC = pos;
                        else
                            PC += 3;
                    }
                });

                Operations.Add(6, new Operation()
                {
                    Opcode = OpCode.JmpF,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];

                        var value = GetMemory(operand1, 0, parameterModes);
                        var pos = GetMemory(operand2, 1, parameterModes);

                        if (value == 0)
                            PC = pos;
                        else
                            PC += 3;
                    }
                });

                Operations.Add(7, new Operation()
                {
                    Opcode = OpCode.LT,
                    Cycles = 4,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];
                        int operand3 = Memory[PC + 3];

                        var value1 = GetMemory(operand1, 0, parameterModes);
                        var value2 = GetMemory(operand2, 1, parameterModes);

                        Memory[operand3] = value1 < value2 ? 1 : 0;
                    }
                });

                Operations.Add(8, new Operation()
                {
                    Opcode = OpCode.EQ,
                    Cycles = 4,
                    Execute =
                    (bool[] parameterModes) =>
                    {
                        int operand1 = Memory[PC + 1];
                        int operand2 = Memory[PC + 2];
                        int operand3 = Memory[PC + 3];

                        var value1 = GetMemory(operand1, 0, parameterModes);
                        var value2 = GetMemory(operand2, 1, parameterModes);

                        Memory[operand3] = value1 == value2 ? 1 : 0;
                    }
                });
            }

            public void Execute()
            {
                while (true)
                {
                    int operation = Memory[PC];
                    int instruction = -1;
                    bool[] parameterModes = new bool[4];

                    if (operation > 100)
                    {
                        instruction = operation % 100;
                        parameterModes[0] = Helpers.GetDigit(operation, 2) == 1;
                        parameterModes[1] = Helpers.GetDigit(operation, 3) == 1;
                        parameterModes[2] = Helpers.GetDigit(operation, 4) == 1;
                        parameterModes[3] = Helpers.GetDigit(operation, 5) == 1;
                    }
                    else
                        instruction = operation;

                    if (instruction == 99) // HALT
                        return;

                    var chosenInstruction = Operations[instruction];
                    chosenInstruction.Execute(parameterModes);
                    PC += chosenInstruction.Cycles < 0 ? 0 : chosenInstruction.Cycles;
                }
            }
        }

        public static void Part1()
        {
            var comp = Helpers.GetInput()[0].Split(",").Select(v => Convert.ToInt32(v)).ToArray();            
            int max = int.MinValue;
            
            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    if (a == b)
                        continue;

                    for (int c = 0; c < 5; c++)
                    {
                        if (c == a || c == b)
                            continue;

                        for (int d = 0; d < 5; d++)
                        {
                            if (d == a || d == b || d == c)
                                continue;

                            for (int e = 0; e < 5; e++)
                            {
                                if (e == a || e == b || e == c || e == d)
                                    continue;

                                int curInput = 0;
                                foreach (var ampSetting in new[] { a, b, c, d, e })
                                {
                                    var CPU = new CPU(comp);
                                    CPU.CurrentInput = curInput;
                                    CPU.PhaseSetting = ampSetting;

                                    CPU.Execute();
                                    curInput = CPU.CurrentOutput;
                                    if (curInput > max)
                                        max = curInput;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(max);            
        }

        public static void Part2()
        {
            var comp = Helpers.GetInput()[0].Split(",").Select(v => Convert.ToInt32(v)).ToArray();
            var CPU = new CPU(comp);
            CPU.Execute();
        }
    }
}
