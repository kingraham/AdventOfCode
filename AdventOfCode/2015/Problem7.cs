using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class Problem7
    {
        public enum Operation
        {
            AND,
            OR,
            LSHIFT,
            RSHIFT,
            NOT,
            ASSIGN
        }

        public class Wire
        {
            public string Key { get; set; }
            public Operation Op { get; set; }
            public Wire Wire1 { get; set; }
            public Wire Wire2 { get; set; }
            public bool Resolved { get; set; }
            public ushort Literal1 { get; set; }
            public ushort Literal2 { get; set; }
            public ushort Value { get; set; }
        }

        public static void Part1()
        {
            Dictionary<string, Wire> wires = new Dictionary<string, Wire>();

            Wire GetOrCreate(string target)
            {
                if (!wires.ContainsKey(target))
                    wires.Add(target, new Wire() { Key = target });
                return wires[target];
            }

            void AssignSlot1(Wire wire, string value)
            {
                ushort val = 0;
                if (ushort.TryParse(value, out val))
                    wire.Literal1 = val;
                else
                    wire.Wire1 = GetOrCreate(value);
            }

            void AssignSlot2(Wire wire, string value)
            {
                ushort val = 0;
                if (ushort.TryParse(value, out val))
                    wire.Literal2 = val;
                else
                    wire.Wire2 = GetOrCreate(value);
            }

            ushort GetSlot1(Wire wire) => wire.Wire1 != null ? wire.Wire1.Value : wire.Literal1;
            ushort GetSlot2(Wire wire) => wire.Wire2 != null ? wire.Wire2.Value : wire.Literal2;

            // preprocess wires into pending commands.
            foreach (var instruction in Helpers.GetInput())
            {
                var initialParse = Regex.Match(instruction, "([A-z 0-9]+) -> ([a-z]+)");
                var command = initialParse.Groups[1].Value;
                var target = initialParse.Groups[2].Value;

                Wire w = GetOrCreate(target);

                if (command.Contains("AND"))
                {
                    w.Op = Operation.AND;
                    var split = command.Split(" AND ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("OR"))
                {
                    w.Op = Operation.OR;
                    var split = command.Split(" OR ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("LSHIFT"))
                {
                    w.Op = Operation.LSHIFT;
                    var split = command.Split(" LSHIFT ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("RSHIFT"))
                {
                    w.Op = Operation.RSHIFT;
                    var split = command.Split(" RSHIFT ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("NOT "))
                {
                    w.Op = Operation.NOT;
                    var from = command.Replace("NOT ", string.Empty);
                    AssignSlot1(w, from);
                }
                else
                {
                    w.Op = Operation.ASSIGN;

                    ushort val = 0;
                    if (ushort.TryParse(command, out val))
                    {
                        w.Value = Convert.ToUInt16(command);
                        w.Resolved = true;
                    }
                    else
                    {
                        w.Wire1 = GetOrCreate(command);
                    }
                }
            }

            bool ChildrenResolved(Wire src)
            {
                return (((src.Wire1 != null && src.Wire1.Resolved) || src.Wire1 == null) &&
                    ((src.Wire2 != null && src.Wire2.Resolved) || src.Wire2 == null));
            }

            // so there's a much better way to do this, this is somewhat lazy.
            bool resolved = true;
            do
            {
                foreach (var kvp in wires)
                {
                    if (kvp.Value.Resolved)
                        continue;
                    if (!ChildrenResolved(kvp.Value))
                    {
                        resolved = false;
                        continue;
                    }
                    var src = kvp.Value;

                    if (kvp.Value.Op == Operation.AND)
                        src.Value = (ushort)(GetSlot1(src) & GetSlot2(src));
                    else if (kvp.Value.Op == Operation.OR)
                        src.Value = (ushort)(GetSlot1(src) | GetSlot2(src));
                    else if (kvp.Value.Op == Operation.LSHIFT)
                        src.Value = (ushort)(GetSlot1(src) << GetSlot2(src));
                    else if (kvp.Value.Op == Operation.RSHIFT)
                        src.Value = (ushort)(GetSlot1(src) >> GetSlot2(src));
                    else if (kvp.Value.Op == Operation.NOT)
                        src.Value = (ushort)~GetSlot1(src);
                    else if (kvp.Value.Op == Operation.ASSIGN)
                        src.Value = GetSlot1(src);

                    src.Resolved = true;
                    if (src.Key == "a")
                    {
                        resolved = true;
                        break;
                    }
                }
            } while (!resolved);

            Console.WriteLine($"{wires["a"].Value}");
        }

        public static void Part2()
        {
            Dictionary<string, Wire> wires = new Dictionary<string, Wire>();
            ushort overrideB = 46065;

            Wire GetOrCreate(string target)
            {
                if (!wires.ContainsKey(target))
                    wires.Add(target, new Wire() { Key = target });
                return wires[target];
            }

            void AssignSlot1(Wire wire, string value)
            {
                ushort val = 0;
                if (ushort.TryParse(value, out val))
                    wire.Literal1 = val;
                else
                    wire.Wire1 = GetOrCreate(value);
            }

            void AssignSlot2(Wire wire, string value)
            {
                ushort val = 0;
                if (ushort.TryParse(value, out val))
                    wire.Literal2 = val;
                else
                    wire.Wire2 = GetOrCreate(value);
            }

            ushort GetSlot1(Wire wire) => wire.Wire1 != null ? wire.Wire1.Value : wire.Literal1;
            ushort GetSlot2(Wire wire) => wire.Wire2 != null ? wire.Wire2.Value : wire.Literal2;

            // preprocess wires into pending commands.
            foreach (var instruction in Helpers.GetInput())
            {
                var initialParse = Regex.Match(instruction, "([A-z 0-9]+) -> ([a-z]+)");
                var command = initialParse.Groups[1].Value;
                var target = initialParse.Groups[2].Value;

                Wire w = GetOrCreate(target);
                if (target == "b")
                {
                    w.Op = Operation.ASSIGN;
                    w.Value = overrideB;
                    w.Resolved = true;
                    continue;
                }
                
                if (command.Contains("AND"))
                {
                    w.Op = Operation.AND;
                    var split = command.Split(" AND ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("OR"))
                {
                    w.Op = Operation.OR;
                    var split = command.Split(" OR ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("LSHIFT"))
                {
                    w.Op = Operation.LSHIFT;
                    var split = command.Split(" LSHIFT ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("RSHIFT"))
                {
                    w.Op = Operation.RSHIFT;
                    var split = command.Split(" RSHIFT ");
                    AssignSlot1(w, split[0]);
                    AssignSlot2(w, split[1]);
                }
                else if (command.Contains("NOT "))
                {
                    w.Op = Operation.NOT;
                    var from = command.Replace("NOT ", string.Empty);
                    AssignSlot1(w, from);
                }
                else
                {
                    w.Op = Operation.ASSIGN;

                    ushort val = 0;
                    if (ushort.TryParse(command, out val))
                    {
                        w.Value = Convert.ToUInt16(command);
                        w.Resolved = true;
                    }
                    else
                    {
                        w.Wire1 = GetOrCreate(command);
                    }
                }
            }

            bool ChildrenResolved(Wire src)
            {
                return (((src.Wire1 != null && src.Wire1.Resolved) || src.Wire1 == null) &&
                    ((src.Wire2 != null && src.Wire2.Resolved) || src.Wire2 == null));
            }

            // so there's a much better way to do this, this is somewhat lazy.
            bool resolved = true;
            do
            {
                foreach (var kvp in wires)
                {
                    if (kvp.Value.Resolved)
                        continue;
                    if (!ChildrenResolved(kvp.Value))
                    {
                        resolved = false;
                        continue;
                    }
                    var src = kvp.Value;

                    if (kvp.Value.Op == Operation.AND)
                        src.Value = (ushort)(GetSlot1(src) & GetSlot2(src));
                    else if (kvp.Value.Op == Operation.OR)
                        src.Value = (ushort)(GetSlot1(src) | GetSlot2(src));
                    else if (kvp.Value.Op == Operation.LSHIFT)
                        src.Value = (ushort)(GetSlot1(src) << GetSlot2(src));
                    else if (kvp.Value.Op == Operation.RSHIFT)
                        src.Value = (ushort)(GetSlot1(src) >> GetSlot2(src));
                    else if (kvp.Value.Op == Operation.NOT)
                        src.Value = (ushort)~GetSlot1(src);
                    else if (kvp.Value.Op == Operation.ASSIGN)
                        src.Value = GetSlot1(src);

                    src.Resolved = true;
                    if (src.Key == "a")
                    {
                        resolved = true;
                        break;
                    }
                }
            } while (!resolved);

            Console.WriteLine($"{wires["a"].Value}");
        }
    }
}