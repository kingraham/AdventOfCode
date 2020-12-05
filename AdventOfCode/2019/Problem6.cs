using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Problem6
    {
        public class OrbitalBody
        {
            public string Marker;
            public int Hops;
            public List<OrbitalBody> Orbits = new List<OrbitalBody>();
        }

        // basically build a graph, figure how many hops the node is from COM, sum all values
        public static void Part1()
        {
            void CalcOrbits(int curHop, OrbitalBody current)
            {
                current.Hops = curHop;
                foreach (var b in current.Orbits)
                {
                    CalcOrbits(curHop + 1, b);
                }                
            }

            Dictionary<string, OrbitalBody> Bodies = new Dictionary<string, OrbitalBody>();
            foreach (var orbit in Helpers.GetInput().Select(a => a.Split(')'))) // build graph
            {
                if (!Bodies.ContainsKey(orbit[0]))
                    Bodies.Add(orbit[0], new OrbitalBody() { Marker = orbit[0] });
                if (!Bodies.ContainsKey(orbit[1]))
                    Bodies.Add(orbit[1], new OrbitalBody() { Marker = orbit[1] });

                Bodies[orbit[0]].Orbits.Add(Bodies[orbit[1]]);
            }
            CalcOrbits(0, Bodies["COM"]);

            Console.WriteLine(Bodies.Values.Sum(a => a.Hops));
        }

        public static void Part2()
        {
            void Navigate(OrbitalBody current)
            {
                foreach (OrbitalBody b in current.Orbits)
                {
                    int proposedHops = current.Hops + 1;
                    if (proposedHops < b.Hops)
                    {
                        b.Hops = proposedHops;
                        Navigate(b);
                    }
                }
            }

            OrbitalBody YouOrbits = null;
            OrbitalBody SantaOrbits = null;

            Dictionary<string, OrbitalBody> Bodies = new Dictionary<string, OrbitalBody>();
            foreach (var orbit in Helpers.GetInput().Select(a => a.Split(')'))) // build graph
            {
                if (!Bodies.ContainsKey(orbit[0]))
                    Bodies.Add(orbit[0], new OrbitalBody() { Marker = orbit[0], Hops = int.MaxValue });
                if (!Bodies.ContainsKey(orbit[1]))
                    Bodies.Add(orbit[1], new OrbitalBody() { Marker = orbit[1], Hops = int.MaxValue });

                if (orbit[1] == "YOU")
                    YouOrbits = Bodies[orbit[0]];
                if (orbit[1] == "SAN")
                    SantaOrbits = Bodies[orbit[0]];

                Bodies[orbit[0]].Orbits.Add(Bodies[orbit[1]]);
                Bodies[orbit[1]].Orbits.Add(Bodies[orbit[0]]); // dual link for traversal
            }

            YouOrbits.Hops = 0;
            Navigate(YouOrbits); // literally the whole damn graph, there's better ways, of course, but brute forcing is going to be right.

            Console.WriteLine(SantaOrbits.Hops);
        }
    }
}
