using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Problem7
    {
        public class Bag
        {
            public string Key { get; set; }

            public int ContainsShinyGold = int.MinValue;
            public int ContainsBags = int.MinValue;

            public List<(int Quantity, Bag bagType)> Contains { get; set; } = new List<(int Quantity, Bag bagType)>();
        }

        private static void ParseRule(string rule, Dictionary<string, Bag> bags)
        {
            Bag TryAdd(string parsedRule)
            {
                if (!bags.ContainsKey(parsedRule))
                    bags.Add(parsedRule, new Bag { Key = parsedRule });
                return bags[parsedRule];
            }

            if (rule.EndsWith("contain no other bags."))
            {
                var bag = TryAdd(rule.Replace("bags contain no other bags.", string.Empty).Trim());
                bag.ContainsShinyGold = 0;
                bag.ContainsBags = 0;
            }
            else
            {
                var matches = Regex.Match(rule, "([a-z ]+ bag[s]?) contain (?:([1-9] [a-z ]+ bag[s]?)[,. ]+)+");

                var source = TryAdd(matches.Groups[1].Captures[0].Value.Replace("bags", string.Empty).Trim());

                foreach (var match in matches.Groups[2].Captures.Select(v => v.Value.Replace("bags", string.Empty).Replace("bag", string.Empty).Trim()))
                {
                    int qty = Convert.ToInt32(match[0].ToString());
                    var parsedRule = match.Substring(2);

                    var bag = TryAdd(parsedRule.Trim());

                    source.Contains.Add((qty, bag));
                }
            }
        }

        public static void Part1()
        {
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
           
            void DetermineShinyGold(Bag bag)
            {
                if (bag.ContainsShinyGold < 0) // already visited
                {
                    bag.ContainsShinyGold = 0; // visited now.
                    foreach (var b in bag.Contains)
                    {
                        if (b.bagType.Key == "shiny gold")
                        {
                            bag.ContainsShinyGold += b.Quantity;
                        }
                        else
                        {
                            DetermineShinyGold(b.bagType);
                            bag.ContainsShinyGold += b.bagType.ContainsShinyGold;
                        }
                    }
                }                
            }

            foreach (var rule in Helpers.GetInput())
                ParseRule(rule, bags);

            foreach (var bag in bags)
                DetermineShinyGold(bag.Value);

            Console.WriteLine(bags.Values.Sum(a => a.ContainsShinyGold <= 0 ? 0 : 1));
        }

        public static void Part2()
        {
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
            
            void DetermineContains(Bag bag)
            {
                if (bag.ContainsBags < 0) // already visited
                {
                    bag.ContainsBags = 0;
                    foreach (var b in bag.Contains)
                    {
                        DetermineContains(b.bagType);
                        bag.ContainsBags += b.Quantity + b.Quantity * b.bagType.ContainsBags;
                    }
                }
            }

            foreach (var rule in Helpers.GetInput())
            {
                ParseRule(rule, bags);
            }

            foreach (var bag in bags)
                DetermineContains(bag.Value);
            Console.WriteLine(bags["shiny gold"].ContainsBags);
        }
    }
}