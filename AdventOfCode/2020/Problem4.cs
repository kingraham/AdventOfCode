using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Problem4
    {
        public static void Part1()
        {
            var requiredFields = new Dictionary<string, int> { { "byr", 0 }, { "iyr", 1 }, { "eyr", 2 }, { "hgt", 3 }, { "hcl", 4 }, { "ecl", 5 }, { "pid", 6 } }; // cid is NOT required.
            int validCount = 0;
            var requiredFound = new bool[requiredFields.Count];

            void updateIfValid()
            {
                bool valid = !requiredFound.Any(a => !a);
                if (valid)
                    validCount++;
                Array.Clear(requiredFound, 0, requiredFound.Length);
            }

            foreach (var line in Helpers.GetInput())
            {
                if (string.IsNullOrEmpty(line))
                {
                    updateIfValid();
                }
                else
                {
                    foreach (var pairs in line.Split(' '))
                    {
                        var pair = pairs.Split(':');
                        if (pair.Length == 2 && pair[0] != "cid")
                        {
                            requiredFound[requiredFields[pair[0]]] = true;
                        }
                    }
                }
            }
            updateIfValid();
            Console.WriteLine(validCount);
        }

        public class Passport
        {
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hcl { get; set; }
            public string ecl { get; set; }
            public string pid { get; set; }
            public string cid { get; set; }
        }

        // a lot of complexity here, but was running into logic issues with it inline, not really happy with this solution because of the allocations.
        public static void Part2()
        {
            HashSet<string> validEyeColors = new HashSet<string>()
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };

            var requiredFields = new Dictionary<string, Func<string, bool>> {
                { "byr", (input) => {
                    var i = Convert.ToInt32(input);
                    return i >= 1920 && i <= 2002;
                } },
                { "iyr", (input) => {
                    var i = Convert.ToInt32(input);
                    return i >= 2010 && i <= 2020;
                } },
                { "eyr", (input) => {
                    var i = Convert.ToInt32(input);
                    return i >= 2020 && i <= 2030;
                } },
                { "hgt", (input) => {
                    if (input.EndsWith("cm"))
                    {
                        input = input.Replace("cm", string.Empty);
                        var i = Convert.ToInt32(input);
                        return i >= 150 && i <= 193;
                    }
                    else if (input.EndsWith("in"))
                    {
                        input = input.Replace("in", string.Empty);
                        var i = Convert.ToInt32(input);
                        return i >= 59 && i <= 76;
                    }
                    return false;
                } },
                { "hcl", (input) => Regex.Match(input, "^#[0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f]$").Success },
                { "ecl", (input) => validEyeColors.Contains(input)},
                { "pid", (input) => Regex.Match(input, "^[0-9]{9}$").Success }
            }; // cid is NOT required.

            int validCount = 0;
            Passport cur = new Passport();

            void updateIfValid()
            {
                if (string.IsNullOrEmpty(cur.byr) || string.IsNullOrEmpty(cur.iyr) || string.IsNullOrEmpty(cur.eyr) || string.IsNullOrEmpty(cur.hgt) || string.IsNullOrEmpty(cur.hcl) || string.IsNullOrEmpty(cur.ecl) || string.IsNullOrEmpty(cur.pid))
                {
                }
                else
                {
                    if (requiredFields["byr"](cur.byr) &&
                        requiredFields["iyr"](cur.iyr) &&
                        requiredFields["eyr"](cur.eyr) &&
                        requiredFields["hgt"](cur.hgt) &&
                        requiredFields["hcl"](cur.hcl) &&
                        requiredFields["ecl"](cur.ecl) &&
                        requiredFields["pid"](cur.pid))
                    {
                        validCount++;
                    }
                }
                cur = new Passport();
            }

            foreach (var line in Helpers.GetInput())
            {
                if (string.IsNullOrEmpty(line))
                {
                    updateIfValid();
                }
                else
                {
                    foreach (var pairs in line.Split(' '))
                    {
                        var pair = pairs.Split(':');
                        switch (pair[0])
                        {
                            case "byr":
                                cur.byr = pair[1];
                                break;

                            case "iyr":
                                cur.iyr = pair[1];
                                break;

                            case "eyr":
                                cur.eyr = pair[1];
                                break;

                            case "hgt":
                                cur.hgt = pair[1];
                                break;

                            case "hcl":
                                cur.hcl = pair[1];
                                break;

                            case "ecl":
                                cur.ecl = pair[1];
                                break;

                            case "pid":
                                cur.pid = pair[1];
                                break;

                            case "cid":
                                cur.cid = pair[1];
                                break;
                        }
                    }
                }
            }

            updateIfValid();
            Console.WriteLine(validCount);
        }
    }
}