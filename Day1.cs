using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Day1 : ISolver
    {
        public List<int> Entries { get; set; }

        public Day1(string inputFile)
        {
            Entries = Reader.ReadStrings(inputFile)
                .Select(input => Int32.Parse(input))
                .ToList();
        }

        public void Part1()
        {
            for (var outerIndex = 0; outerIndex < Entries.Count; outerIndex++)
            {
                for (var innerIndex = outerIndex + 1; innerIndex < Entries.Count; innerIndex++)
                {
                    var outerElement = Entries[outerIndex];
                    var innerElement = Entries[innerIndex];

                    if (outerElement + innerElement == 2020)
                    {
                        Console.WriteLine(outerElement);
                        Console.WriteLine(innerElement);
                        Console.WriteLine(outerElement * innerElement);
                    }
                }
            }
        }

        public void Part2()
        {
            for (var outerIndex = 0; outerIndex < Entries.Count; outerIndex++)
            {
                for (var innerIndex = outerIndex + 1; innerIndex < Entries.Count; innerIndex++)
                {
                    for (var innermostIndex = innerIndex + 1; innermostIndex < Entries.Count; innermostIndex++)
                    {
                        var outerElement = Entries[outerIndex];
                        var innerElement = Entries[innerIndex];
                        var innermostElement = Entries[innermostIndex];

                        if (outerElement + innerElement + innermostElement == 2020)
                        {
                            Console.WriteLine(outerElement);
                            Console.WriteLine(innerElement);
                            Console.WriteLine(innermostElement);
                            Console.WriteLine(outerElement * innerElement * innermostElement);
                        }
                    }
                }
            }
        }
    }
}