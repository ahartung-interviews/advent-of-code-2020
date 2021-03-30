using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day4 : ISolver
    {
        public List<Passport> Passports { get; set; }

        public Day4(string inputFile)
        {
            Passports = new List<Passport>();

            using (var sr = new StreamReader(inputFile))
            {
                string newPassportFields = String.Empty;
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        Passports.Add(new Passport(newPassportFields));
                        newPassportFields = String.Empty;
                    } else {
                        newPassportFields += " " + line;
                    }
                }
                Passports.Add(new Passport(newPassportFields));
            }
        }

        public void Part1()
        {
            var count = Passports
                .Where(passport => passport.IsNaivelyValid())
                .Count();

            Console.WriteLine(count);
        }

        public void Part2()
        {
            var count = Passports
                .Where(passport => passport.IsStrictlyValid())
                .Count();

            Console.WriteLine(count);
        }
    }

    public class Passport
    {
        public List<PassportField> Fields { get; set; }

        private char[] delimiterChars = { ' ' };

        public Passport(string fieldsAsString)
        {
            Fields = fieldsAsString
                .Trim()
                .Split(delimiterChars)
                .Select(field => new PassportField(field))
                .ToList();
        }

        public bool IsNaivelyValid()
        {
            var distinct = Fields
                .Select(field => field.Label)
                .Distinct()
                .ToList();

            return distinct.Count() == 8
                || distinct.Count() == 7 && !distinct.Contains(PassportFieldLabels.CountryId);
        }

        public bool IsStrictlyValid()
        {
            var distinctCount = Fields
                .Select(field => field.Label)
                .Distinct()
                .Count();

            return distinctCount == Fields.Count
                && (
                    Fields.Count == 8
                    || Fields.Count == 7 && !Fields.Select(f => f.Label).Contains(PassportFieldLabels.CountryId)
                )
                && Fields.All(field => field.IsValid);
        }
    }

    public class PassportField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsValid { get; set; }

        public PassportField() { }

        public PassportField(string labelAndValue)
        {
            var tokens = labelAndValue.Trim().Split(':');

            Label = tokens[0];
            Value = tokens[1];

            switch(Label)
            {
                case PassportFieldLabels.BirthYear:
                    IsValid = IsValidInt(Value, 1920, 2002);
                    break;
                case PassportFieldLabels.IssueYear:
                    IsValid = IsValidInt(Value, 2010, 2020);
                    break;
                case PassportFieldLabels.ExpirationYear:
                    IsValid = IsValidInt(Value, 2020, 2030);
                    break;
                case PassportFieldLabels.Height:
                    if (Value.EndsWith("in"))
                    {
                        var valueSansUnits = Value.Substring(0, Value.LastIndexOf("in"));
                        IsValid = IsValidInt(valueSansUnits, 59, 76);
                    }
                    else if (Value.EndsWith("cm"))
                    {
                        var valueSansUnits = Value.Substring(0, Value.LastIndexOf("cm"));
                        IsValid = IsValidInt(valueSansUnits, 150, 193);
                    }
                    else
                    {
                        IsValid = false;
                    }
                    break;
                case PassportFieldLabels.HairColor:
                    Regex re = new Regex("^#[a-f0-9]{6}$");
                    var match = re.Match(Value);
                    IsValid = match.Success;
                    break;
                case PassportFieldLabels.EyeColor:
                    var validEyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    IsValid = validEyeColors.Contains(Value);
                    break;
                case PassportFieldLabels.PassportId:
                    var canParse = Int32.TryParse(Value, out int _);
                    IsValid = Value.Length == 9
                        && canParse;
                    break;
                case PassportFieldLabels.CountryId:
                    IsValid = true;
                    break;
                default:
                    IsValid = false;
                    break;
            }
        }

        private bool IsValidInt(string intAsString, int minimumInclusive, int maximumInclusive)
        {
            var canParse = Int32.TryParse(intAsString, out int value);
            return canParse
                && value >= minimumInclusive
                && value <= maximumInclusive;
        }
    }

    public static class PassportFieldLabels
    {
        public const string BirthYear = "byr";
        public const string IssueYear = "iyr";
        public const string ExpirationYear = "eyr";
        public const string Height = "hgt";
        public const string HairColor = "hcl";
        public const string EyeColor = "ecl";
        public const string PassportId = "pid";
        public const string CountryId = "cid";
    }
}