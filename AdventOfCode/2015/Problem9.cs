using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    // TSP!
    public class Problem9
    {
        // naive, let's get all permutations and calculate the routes.
        public static void Part1()
        {
            var s = Helpers.GetInput()
                .Select(a => Regex.Match(a, "([A-z]+) to ([A-z]+) = ([0-9]+)").Groups)
                .Select(a => (Source: a[1].Value, Target: a[2].Value, Dist: Convert.ToInt32(a[3].Value)))
                .ToArray();

            Dictionary<(int from, int to), int> Distances = new Dictionary<(int from, int to), int>();

            Dictionary<string, int> citiesToIdx = new Dictionary<string, int>();

            int idx = 1;
            foreach (var d in s)
            {
                if (!citiesToIdx.ContainsKey(d.Source))
                    citiesToIdx.Add(d.Source, idx++);
                if (!citiesToIdx.ContainsKey(d.Target))
                    citiesToIdx.Add(d.Target, idx++);

                Distances.Add((citiesToIdx[d.Source], citiesToIdx[d.Target]), d.Dist);
                Distances.Add((citiesToIdx[d.Target], citiesToIdx[d.Source]), d.Dist);
            }

            var cities = citiesToIdx.Values.ToArray();

            int minTrip = int.MaxValue;

            foreach (var permutation in Helpers.GetPermutations(cities.Length, cities))
            {
                int curTrip = 0;
                for (int i = 0; i < permutation.Length - 1; i++)
                {
                    var key = (permutation[i], permutation[i + 1]);
                    if (!Distances.ContainsKey(key))
                        break;
                    curTrip += Distances[key];
                }

                if (curTrip < minTrip)
                    minTrip = curTrip;
            }

            Console.WriteLine(minTrip);
        }

        public static void Part2()
        {
            var s = Helpers.GetInput()
               .Select(a => Regex.Match(a, "([A-z]+) to ([A-z]+) = ([0-9]+)").Groups)
               .Select(a => (Source: a[1].Value, Target: a[2].Value, Dist: Convert.ToInt32(a[3].Value)))
               .ToArray();

            Dictionary<(int from, int to), int> Distances = new Dictionary<(int from, int to), int>();

            Dictionary<string, int> citiesToIdx = new Dictionary<string, int>();

            int idx = 1;
            foreach (var d in s)
            {
                if (!citiesToIdx.ContainsKey(d.Source))
                    citiesToIdx.Add(d.Source, idx++);
                if (!citiesToIdx.ContainsKey(d.Target))
                    citiesToIdx.Add(d.Target, idx++);

                Distances.Add((citiesToIdx[d.Source], citiesToIdx[d.Target]), d.Dist);
                Distances.Add((citiesToIdx[d.Target], citiesToIdx[d.Source]), d.Dist);
            }

            var cities = citiesToIdx.Values.ToArray();

            int maxTrip = int.MinValue;

            foreach (var permutation in Helpers.GetPermutations(cities.Length, cities))
            {
                int curTrip = 0;
                for (int i = 0; i < permutation.Length - 1; i++)
                {
                    var key = (permutation[i], permutation[i + 1]);
                    if (!Distances.ContainsKey(key))
                        break;
                    curTrip += Distances[key];
                }

                if (curTrip > maxTrip)
                    maxTrip = curTrip;
            }

            Console.WriteLine(maxTrip);
        }
    }
}