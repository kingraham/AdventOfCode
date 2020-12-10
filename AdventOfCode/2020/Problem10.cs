using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Problem10
    {
        public static void Part1()
        {
            var values = Helpers.GetInput().Select(a => Convert.ToInt32(a)).OrderBy(a => a).ToArray();

            int oneDiff = 0;
            int threeDiff = 0;
            int lastValue = 0;

            for (int i = 0; i < values.Length; i++)
            {
                int curValue = values[i];
                oneDiff = oneDiff + (curValue - lastValue == 1 ? 1 : 0);
                threeDiff = threeDiff + (curValue - lastValue == 3 ? 1 : 0);
                lastValue = curValue;
            }

            Console.WriteLine($"$Differences of One: {oneDiff} Three: {threeDiff+1} Answer: {oneDiff * (threeDiff+1)}"); // built in adapter always adjusts threeDiff by one
        }

        public class Adapter
        {
            public Adapter(int val)
            {
                Value = val;
            }

            public int Value { get; private set; }
            public long Paths { get; set; } // how many paths go through this point
            public bool Queued { get; set; } // has this adapter been considered?
            public List<Adapter> Compatible { get; set; } = new List<Adapter>(); // what can plug into this.
        }

        public static void Part2()
        {
            var values = Helpers.GetInput().Select(a => Convert.ToInt32(a)).ToList();

            var max = values.Max() + 3;
            values.AddRange(new[] { 0, max });

            var valuesOrdered = values.OrderBy(a => a).ToArray();
            var adapterSet = valuesOrdered.ToDictionary(a => a, a => new Adapter(a));
                        
            for (int i = 0; i < valuesOrdered.Length; i++)
            {
                var currentAdapter = adapterSet[valuesOrdered[i]];
                var value = currentAdapter.Value;

                if (adapterSet.ContainsKey(value + 1))
                    currentAdapter.Compatible.Add(adapterSet[value + 1]);
                if (adapterSet.ContainsKey(value + 2))
                    currentAdapter.Compatible.Add(adapterSet[value + 2]);
                if (adapterSet.ContainsKey(value + 3))
                    currentAdapter.Compatible.Add(adapterSet[value + 3]);
            }

            Queue<Adapter> adapterQueue = new Queue<Adapter>();
            adapterQueue.Enqueue(adapterSet[0]);
            adapterSet[0].Queued = true;
            adapterSet[0].Paths = 1;

            while (adapterQueue.Count > 0) // use a queue to figure how many paths go through each node
            {
                var cur = adapterQueue.Dequeue();

                foreach (var comp in cur.Compatible)
                {
                    if (!comp.Queued)
                    {
                        adapterQueue.Enqueue(comp);
                        comp.Queued = true;
                        comp.Paths = cur.Paths;
                    } else
                    {
                        comp.Paths += cur.Paths;
                    }                    
                }
            }

            Console.WriteLine(adapterSet[max].Paths); // then see what the last node returns
        }
    }
}