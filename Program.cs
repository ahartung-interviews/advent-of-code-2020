using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (InvalidArgs(args))
            {
                return;
            }

            var day = Int32.Parse(args[0]);
            var part = Int32.Parse(args[1]);

            var solvers = new List<ISolver>
            {
                new Day1("Inputs/Day1.txt"),
                new Day2("Inputs/Day2.txt"),
                new Day3("Inputs/Day3.txt"),
                new Day4("Inputs/Day4.txt"),
                new Day5("Inputs/Day5.txt"),
                new Day6("Inputs/Day6.txt"),
                new Day7("Inputs/Day7.txt"),
                new Day8("Inputs/Day8.txt")
            };

            if (day > solvers.Count)
            {
                Help();
                Console.WriteLine($"No support for day {day} yet.");
                return;
            }

            if (part == 1)
            {
                solvers[day - 1].Part1();
            }
            else if (part == 2)
            {
                solvers[day - 1].Part2();
            }
        }

        static bool InvalidArgs(string[] args)
        {
            if (args.Length != 2)
            {
                Help();
                return true;
            }

            string unparsedDay = args[0];
            var canParseDay = Int32.TryParse(unparsedDay, out int day);

            if (day < 1 || day > 25)
            {
                Help();
                return true;
            }

            string unparsedPart = args[1];
            var canParsePart = Int32.TryParse(unparsedPart, out int part);
            if (part < 1 || part > 2)
            {
                Help();
                return true;
            }

            return false;
        }

        static void Help()
        {
            Console.WriteLine("Usage: dotnet run -- <day 1-25> <part 1|2>");
        }
    }
}
