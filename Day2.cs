using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day2 : ISolver
    {
        public List<PasswordWithRule> Entries { get; set; }

        public Day2(string inputFile)
        {
            Entries = Reader.ReadStrings(inputFile)
                .Select(input => new PasswordWithRule(input))
                .ToList();
        }

        public void Part1()
        {
            var entries = Entries
                .Where(password => password.IsValidPart1())
                .ToList();

            Console.WriteLine(entries.Count);
        }

        public void Part2()
        {
            var entries = Entries
                .Where(password => password.IsValidPart2())
                .ToList();

            Console.WriteLine(entries.Count);
        }
    }

    public class PasswordWithRule
    {
        public char Letter { get; set; }
        public string Password { get; set; }
        public int MinLetterCountInclusive { get; set; }
        public int MaxLetterCountInclusive { get; set; }
        private Regex re = new Regex("^(?<min>[0-9]+)-(?<max>[0-9]+) (?<letter>[a-z]): (?<password>[a-z]*)$");

        public PasswordWithRule(string input)
        {
            var match = re.Match(input);
            MinLetterCountInclusive = Int32.Parse(match.Groups["min"].Value);
            MaxLetterCountInclusive = Int32.Parse(match.Groups["max"].Value);
            Letter = Char.Parse(match.Groups["letter"].Value);
            Password = match.Groups["password"].Value;
        }

        public bool IsValidPart1()
        {
            var frequency = Password.Count(letter => letter == Letter);

            return frequency >= MinLetterCountInclusive && frequency <= MaxLetterCountInclusive;
        }

        public bool IsValidPart2()
        {
            var firstPosition = Password[MinLetterCountInclusive - 1];
            var lastPosition = Password[MaxLetterCountInclusive - 1];

            return (firstPosition == Letter && lastPosition != Letter)
                || (firstPosition != Letter && lastPosition == Letter);
        }
    }
}