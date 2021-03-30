using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6 : ISolver
    {
        public List<Group> Groups { get; set; }

        public Day6(string inputFile)
        {
            Groups = new List<Group>();

            using (var sr = new StreamReader(inputFile))
            {
                IEnumerable<string> answers = Enumerable.Empty<string>();
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        Groups.Add(new Group(answers));
                        answers = Enumerable.Empty<string>();
                    } else {
                        answers = answers.Append(line);
                    }
                }
                Groups.Add(new Group(answers));
            }
        }

        public void Part1()
        {
            var sum = Groups.Select(g => g.DistinctAnswers).Sum();

            Console.WriteLine(sum);
        }

        public void Part2()
        {
            var sum = Groups.Select(g => g.UniformAnswers).Sum();

            Console.WriteLine(sum);
        }
    }

    public class Group
    {
        private List<string> IndividualAnswers { get; set; }

        private Dictionary<char, int> AnswerFrequencies { get; set; }

        public int DistinctAnswers
        {
            get => string
                .Join("", IndividualAnswers)
                .ToList()
                .Distinct()
                .Count();
        }

        public int UniformAnswers
        {
            get => AnswerFrequencies
                .Where(af => af.Value == IndividualAnswers.Count)
                .Count();
        }

        public Group() { }

        public Group(IEnumerable<string> individualAnswers)
        {
            IndividualAnswers = individualAnswers.ToList();

            AnswerFrequencies = new Dictionary<char, int>();

            IndividualAnswers
                .ForEach(answer => {
                    answer
                        .ToList()
                        .ForEach(c => AnswerFrequencies[c] = AnswerFrequencies.GetValueOrDefault(c, 0) + 1);
                });
        }
    }
}